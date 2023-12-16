using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;

    float xRotation = 0f;

    public float xSensitivity = 5f;
    public float ySensitivity = 5f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ProcessLook(Vector2 input)
    {
        // Mouse inputs
        float mouseX = input.x * xSensitivity * Time.deltaTime;
        float mouseY = input.y * xSensitivity * Time.deltaTime;

        //Calculate Rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Apply Camera Transform
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //Rotate Left and Right
        transform.Rotate(Vector3.up * mouseX);
    }
}
