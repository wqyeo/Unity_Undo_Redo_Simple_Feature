using UnityEngine;

public class DeleteAction : UserAction {

    public DeleteAction(GameObject _targetObject) : base(_targetObject) {}

    internal override void Redo() {
        TargetObject.SetActive(false);
    }

    internal override void Undo() {
        TargetObject.SetActive(true);
    }
}
