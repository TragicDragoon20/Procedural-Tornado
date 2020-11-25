using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 2.0f;
    [SerializeField]
    private float minTurnAngle = -90.0f;
    [SerializeField]
    private float maxTurnAngle = 90.0f;

    private float headRotation;

    [SerializeField]
    private Camera cam;

    private float rotX;
    private float rotY;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        rotY = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        rotX = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime * -1;

        this.transform.Rotate(0, rotY, 0);

        headRotation += rotX;
        headRotation = Mathf.Clamp(headRotation, minTurnAngle, maxTurnAngle);
        cam.transform.localEulerAngles = new Vector3(headRotation, 0, 0);
    }
}
