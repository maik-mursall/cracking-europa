using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVelocity = Vector3.zero;

    private void Update()
    {
        if (Gameplay.GameManager.instance)
        {
            if (Gameplay.GameManager.instance.orbitsAreMoving)
                transform.Rotate(rotationVelocity * Time.deltaTime);
        }
        else
        {
            transform.Rotate(rotationVelocity * Time.deltaTime);
        }

    }
}
