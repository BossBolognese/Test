using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public Transform target;
    public Vector3 offset;
    private float currentZoom = 10f;
    public float pitch = 2f;
    public float zoomSpeed = 4f, minZoom = 5f, maxZoom = 15f;
    public float yawSpeed = 100f;
    private float currentYaw = 0f;

    private Vector3 lastMousePosition;
    private bool enableRotation = false;

    public Image cursor;

    public bool inventoryOpen = false;

    public float mouseSensitivityX = 3f;
    public float mouseSensitivityY = 3f;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        //currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;

        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = cursor.transform.position;
        }

        if (Input.GetMouseButton(1))
        {

            if (lastMousePosition != null)
            {
                Debug.Log("Last pos: " + lastMousePosition + " current pos: " + Input.mousePosition + " Distance: " + Vector3.Distance(cursor.transform.position, lastMousePosition));


                if (Vector3.Distance(cursor.transform.position, lastMousePosition) >= 15)
                {
                    //Cursor.lockState = CursorLockMode.Locked;
                    //.visible = false;
                    enableRotation = true;
                    currentYaw -= Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;
                }
            }

        }

        if (Input.GetButtonDown("Inventory"))
        {
            inventoryOpen = !inventoryOpen;
        }

        if (Input.GetMouseButtonUp(1))
        {
            enableRotation = false;
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        }
    }
    

    void LateUpdate () {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.up, currentYaw);
	}

    private void OnGUI()
    {
        if (!enableRotation)
        {
            if (!inventoryOpen)
            {
                cursor.enabled = true;
            }
            else
            {
                cursor.enabled = false;
            }
            //cursor.transform.position += new Vector3(Input.GetAxis("Mouse X") * mouseSensitivityX, Input.GetAxis("Mouse Y") * mouseSensitivityY, 0f);
            cursor.transform.position = Input.mousePosition;
        }
        else
        {
            cursor.enabled = false;
        }
    }
}
