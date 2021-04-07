using System;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (lineRenderer)
        {
            bool mouseDown = Input.GetMouseButton(0);
            lineRenderer.enabled = mouseDown;
            
            if (mouseDown)
            {
                Ray ray = _mainCamera.ScreenPointToRay (Input.mousePosition);
                
                lineRenderer.SetPosition(0, transform.position);

                if (Physics.Raycast (ray, out RaycastHit hit, Mathf.Infinity)) 
                {
                    lineRenderer.SetPosition(1, hit.point);
                }
                else
                {
                    lineRenderer.SetPosition(1, ray.origin + ray.direction * 50f);
                }
            }
        }
    }
}
