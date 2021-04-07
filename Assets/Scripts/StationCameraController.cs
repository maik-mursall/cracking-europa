using UnityEngine;

[RequireComponent(typeof(Camera))]
public class StationCameraController : MonoBehaviour
{
    [SerializeField] private float minFOV = 30f;
    [SerializeField] private float maxFOV = 60f;
    [SerializeField] private float scrollMultiplier = 2f;

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView - (scrollMultiplier * Input.mouseScrollDelta.y), minFOV, maxFOV);
    }
}
