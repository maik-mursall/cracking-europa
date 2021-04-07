using UnityEngine;
using Cursor = UnityEngine.Cursor;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Camera _camera;
    private Vector3 _rotation = Vector3.zero;

    [SerializeField] private float distance = 100f;

    [SerializeField] private bool followRotation = false;

    private void Start()
    {
        SetMouseVisible(false);
    }

    private void SetMouseVisible(bool visible)
    {
        Cursor.visible = visible;
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void Update()
    {
        _rotation.x += Input.GetAxis("Mouse Y");
        _rotation.y -= Input.GetAxis("Mouse X");

        Quaternion lookRotation = Quaternion.Euler(_rotation + (followRotation ? target.rotation.eulerAngles : Vector3.zero));

        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = target.position - (lookDirection * distance);
        
        transform.SetPositionAndRotation(lookPosition, lookRotation);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetMouseVisible(true);
        }
    }
}
