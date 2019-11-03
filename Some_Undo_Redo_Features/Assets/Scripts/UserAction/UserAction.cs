using UnityEngine;

public abstract class UserAction {

    public GameObject TargetObject { get; private set; }

    public UserAction(GameObject _targetObject) {
        TargetObject = _targetObject;
    }

    internal abstract void Undo();

    internal abstract void Redo();
}
