using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public LayerMask movementMask;

    Camera cam;
    PlayerMotor motor;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            //shoot ray from mouse pos
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                Debug.Log("Object hit: " + hit.collider.name + " " + hit.point);

                // Move player to raycast hit position

                motor.MoveToPoint(hit.point);

                //Stop focusing objects

            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            //shoot ray from mouse pos
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log("Object hit: " + hit.collider.name + " " + hit.point);

                //Check if hit object is interactable
                //if interactable set it as focus

            }
        }
    }
}
