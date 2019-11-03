using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserActionCache : MonoBehaviour {

    #region Singleton
    private static UserActionCache instance;

    public static UserActionCache Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<UserActionCache>();
            }

            return instance;
        }
    }
    #endregion

    Stack<UserAction> actionsPerformed;

    Stack<UserAction> actionsUndoned;

    private void Awake() {
        instance = this;
        actionsPerformed = new Stack<UserAction>();
        actionsUndoned = new Stack<UserAction>();
    }

    internal void UndoLastAction() {
        if (actionsPerformed.Count > 0) {
            UserAction lastActionDone = actionsPerformed.Pop();

            lastActionDone.Undo();

            actionsUndoned.Push(lastActionDone);
        }
    }

    internal void RedoLastUndoneAction() {
        if (actionsUndoned.Count > 0) {
            UserAction lastUndoneAction = actionsUndoned.Pop();

            lastUndoneAction.Redo();

            actionsPerformed.Push(lastUndoneAction);
        }
    }

    internal void AddActionPerformed(UserAction performedAction) {
        actionsPerformed.Push(performedAction);

        // NOTE: Possible optimization for the actions below?

        // Before we actually clear the 'actionsUndoned' stack,
        // we will destroy any possible gameobjects that is not referenced anymore.
        foreach (var actionUndone in actionsUndoned) {
            GameObject currentTargetObject = actionUndone.TargetObject;

            // Already destroyed, moving on.
            if (currentTargetObject == null) {
                continue;
            }

            bool destroyTargetObject = true;

            foreach (var actionPerformed in actionsPerformed) {
                if (actionPerformed.TargetObject.gameObject == currentTargetObject) {
                    // Still referenced even after the stack is cleared.
                    // Dont destroy.
                    destroyTargetObject = false;
                    break;
                }
            }

            // Not referenced after the stack is cleared; Destroy
            if (destroyTargetObject) {
                Destroy(currentTargetObject);
            }
        }

        actionsUndoned = new Stack<UserAction>();
    }
}
