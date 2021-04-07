using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVelocity = Vector3.zero;

    private void Update()
    {
        transform.Rotate(rotationVelocity * Time.deltaTime);
    }
}
