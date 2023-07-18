 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTurn : MonoBehaviour
{
    public float sensitivity = 100f;
    public Transform Capsule;
    public Transform orientation;
    float xRotation = 0f;
    float yRotation = 0f;
    // Start is called before the first frame update
    private void Start()
    {
        GameObject orientationObject = GameObject.Find("orientation");
        orientation = orientationObject.GetComponent<Transform>();

        GameObject player = GameObject.Find("Capsule");
        Capsule = player.GetComponent<Transform>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}