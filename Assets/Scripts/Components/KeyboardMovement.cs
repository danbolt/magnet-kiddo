using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardMovement : MonoBehaviour
{

    //private Rigidbody body;

    private CharacterController controller;

    private CharacterPhysicsComp physComp;

    public float WalkSpeed = 1;

    //public float JumpHeight = 20.0f;

	// Use this for initialization
	void Start ()
    {
        //this.body = GetComponent<Rigidbody>();
        this.controller = GetComponent<CharacterController>();
        this.physComp = GetComponent<CharacterPhysicsComp>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 movementVelocity = new Vector3(0, 0, 0);

        // maybe another component for the actual movement?
        movementVelocity += Input.GetAxis("Horizontal") * new Vector3(1, 0, 0) * this.WalkSpeed * Time.deltaTime;
        movementVelocity +=   Input.GetAxis("Vertical") * new Vector3(0, 0, 1) * this.WalkSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            physComp.Jump();
        }

        this.controller.Move(movementVelocity);
    }
}
