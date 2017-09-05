using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public LayerMask movementMask;

    Camera cam;
    PlayerMotor motor;

    public Image cursor;

    public Interactable focus;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //shoot ray from mouse pos
            Ray ray = cam.ScreenPointToRay(cursor.transform.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                Debug.Log("Object hit: " + hit.collider.name + " " + hit.point);

                // Move player to raycast hit position

                motor.MoveToPoint(hit.point);

                //Stop focusing objects
                RemoveFocus();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            //shoot ray from mouse pos
            Ray ray = cam.ScreenPointToRay(cursor.transform.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log("Object hit: " + hit.collider.name + " " + hit.point);

                //Check if hit object is interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    // set it as focus
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }
            
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }

        focus = null;
        motor.StopFollowingTarget();
    }
}
