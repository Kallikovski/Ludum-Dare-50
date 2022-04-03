using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSensetivity;
    private float xRotation = 0f;
    private new GameObject camera;

    private void Start()
    {
        camera = gameObject;
    }

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensetivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
