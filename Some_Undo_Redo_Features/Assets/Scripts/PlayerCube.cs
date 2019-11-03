using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PlayerCube : MonoBehaviour {
    [SerializeField, Tooltip("Material to show when the cube is selected")]
    private Material selectedMaterial;

    [SerializeField, Tooltip("Default material")]
    private Material defaultMaterial;

    private Renderer cubeRenderer;

    private void Awake() {
        cubeRenderer = GetComponent<Renderer>();
    }

    internal void SelectCube() {
        cubeRenderer.material = selectedMaterial;
    }

    internal void DeselectCube() {
        cubeRenderer.material = defaultMaterial;
    }
}
