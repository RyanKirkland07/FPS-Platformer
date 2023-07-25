 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTurn : MonoBehaviour
{
    //Sensitivity
    public float sensitivity = 100f;
    public Transform Capsule;
    public Transform orientation;
    float xRotation = 0f;
    float yRotation = 0f;
    // Start is called before the first frame update
    private void Start()
    {
        //Finds orientation object and transform
        GameObject orientationObject = GameObject.Find("orientation");
        orientation = orientationObject.GetComponent<Transform>();

        //Finds player object and transform
        GameObject player = GameObject.Find("Capsule");
        Capsule = player.GetComponent<Transform>();

        //locks cursor to screen and hides it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        //Gets mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        //Sets the values to what is required and clamps the xRotation so you can only look up and down at a 180 degree angle
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotates camera and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
    //turns off mouse lock and invisibility when pause menu is opened
    public void TurnOff()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    //turns back on mouse lock and invisibility when closing pause menu
    public void BackOn()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    //changes sensitivity when changed in pause menu
    public void ChangeSens(float value)
    {
        sensitivity = value;
    }
}