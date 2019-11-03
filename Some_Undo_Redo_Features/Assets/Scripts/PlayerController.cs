using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    private const KeyCode DELETE_KEY = KeyCode.Delete;

    /// <summary>
    /// Hold Left-Mouse to make the object follow your mouse.
    /// Lift it up to stop dragging.
    /// </summary>
    private const KeyCode DRAG_KEY = KeyCode.Mouse0;

    private const KeyCode UNDO_KEY = KeyCode.Z;
    private const KeyCode REDO_KEY = KeyCode.Y;


    [SerializeField, Tooltip("The name of the tag for the cubes")]
    private string cubeTag;

    [SerializeField, Tooltip("Prefab of the cube")]
    private PlayerCube playerCubePrefab;

    private PlayerCube currentlySelectedCube;

    private EventSystem currentEventSystem;

    private Camera mainCamera;

    private Vector3 startDragPosition;

    private void Awake() {
        currentEventSystem = EventSystem.current;
        mainCamera = Camera.main;
        currentlySelectedCube = null;
    }

    private void Update() {
        if (Input.GetKeyDown(DRAG_KEY)) {
            // Cant select another cube if one is currently selected.
            if (currentlySelectedCube == null) {
                HandlePlayerSelectingCube();
            }
        }

        if (Input.GetKeyUp(DRAG_KEY) && currentlySelectedCube != null) {
            MoveAction moveAction = new MoveAction(currentlySelectedCube.gameObject, startDragPosition, currentlySelectedCube.transform.position);
            UserActionCache.Instance.AddActionPerformed(moveAction);

            currentlySelectedCube.DeselectCube();
            currentlySelectedCube = null;
        }

        if (currentlySelectedCube != null) {
            MoveCurrentlySelectedCubeByCursorMovement();

            if (Input.GetKeyDown(DELETE_KEY)) {

                DeleteAction deleteAction = new DeleteAction(currentlySelectedCube.gameObject);
                UserActionCache.Instance.AddActionPerformed(deleteAction);

                currentlySelectedCube.DeselectCube();
                currentlySelectedCube.gameObject.SetActive(false);
                currentlySelectedCube = null;
            }
        }


        if (Input.GetKeyDown(UNDO_KEY)) {
            UserActionCache.Instance.UndoLastAction();
        }

        if (Input.GetKeyDown(REDO_KEY)) {
            UserActionCache.Instance.RedoLastUndoneAction();
        }
    }

    private void MoveCurrentlySelectedCubeByCursorMovement() {
        Vector3 newCubePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newCubePos.z = 0f;
        currentlySelectedCube.transform.position = newCubePos;
    }

    private void HandlePlayerSelectingCube() {
        if (PlayerNotClickingOnInterface()) {
            if (TryGetClickedCube(out PlayerCube selectedCube)) {
                selectedCube.SelectCube();
                currentlySelectedCube = selectedCube;

                startDragPosition = selectedCube.transform.position;
            }
        }

        #region Local_Function

        bool TryGetClickedCube(out PlayerCube playerCube) {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit)) {
                if (hit.transform.CompareTag(cubeTag)) {
                    playerCube = hit.transform.GetComponent<PlayerCube>();
                    return true;
                }
            }

            playerCube = null;
            return false;
        }

        bool PlayerNotClickingOnInterface() {
            return !currentEventSystem.IsPointerOverGameObject();
        }

        #endregion
    }

    /// <summary>
    /// Called by the unity's creation button in the game scene.
    /// </summary>
    public void CreateNewCubeAtOrigin() {
        GameObject newCube = Instantiate(playerCubePrefab).gameObject;
        newCube.transform.position = Vector3.zero;

        CreateAction createAction = new CreateAction(newCube);
        UserActionCache.Instance.AddActionPerformed(createAction);
    }
}
