using UnityEngine;

public class CreateAction : UserAction {
    public CreateAction(GameObject _targetObject) : base(_targetObject) { }

    internal override void Redo() {
        TargetObject.SetActive(true);
    }

    internal override void Undo() {
        TargetObject.SetActive(false);
    }
}
