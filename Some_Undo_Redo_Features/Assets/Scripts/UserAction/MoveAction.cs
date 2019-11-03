using UnityEngine;

public class MoveAction : UserAction {

    private Vector3 startPosition;
    private Vector3 endPosition;

    public MoveAction(GameObject _targetObject, Vector3 _startPosition, Vector3 _endPosition) : base(_targetObject) {
        startPosition = _startPosition;
        endPosition = _endPosition;
    }

    internal override void Redo() {
        TargetObject.transform.position = endPosition;
    }

    internal override void Undo() {
        TargetObject.transform.position = startPosition;
    }
}
