using System;
using UnityEngine;

public class CameraControlls : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float maxSpeed, maxSpeedAtZoom, acceleration, zoomInSpeed, zoomOutSpeed;

    private float speed, horizontal, vertical;

    private void Start()
    {
        speed = maxSpeed * (camera.orthographicSize / maxSpeedAtZoom);
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 && vertical == 0)
        {
            rigidbody.linearVelocity = new Vector2(horizontal * maxSpeed, 0);
        }
        else if (horizontal == 0 && vertical != 0)
        {
            rigidbody.linearVelocity = new Vector2(0, vertical * maxSpeed);
        }
        else if (horizontal != 0 && vertical != 0)
        {
            Vector3 vector = new Vector2(horizontal * maxSpeed, vertical * maxSpeed);
            rigidbody.linearVelocity = vector.normalized * maxSpeed;
        }
        else
        {
            rigidbody.linearVelocity = Vector2.zero;
        }
        
        
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            camera.orthographicSize *= zoomOutSpeed;
            speed = maxSpeed * (camera.orthographicSize / maxSpeedAtZoom);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            camera.orthographicSize *= zoomInSpeed;
            speed = maxSpeed * (camera.orthographicSize / maxSpeedAtZoom);
        }
    }
}
