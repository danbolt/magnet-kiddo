// adds gravity to character movement comp

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPhysicsComp : MonoBehaviour
{
    private CharacterController controller;

    public float GravityScale = 1.0f;

    // TODO: apply jump velocity for JumpDuration when jump is applied

    public float JumpHeight = 10.0f;

    private float LastJumpTime;

    // how long we apply upwards velocity after a jump is pressed
    public float JumpDuration;

    // Use this for initialization
    void Start ()
    {
        this.controller = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // apply gravity if we are in the air
        if (!controller.isGrounded)
        {
            controller.Move(GetCurrentGravity() * Time.deltaTime);
        }

    }

    Vector3 GetCurrentGravity()
    {
        return Physics.gravity * GravityScale;
    }

    public bool Jump ()
    {
        // don't allow jumping if we are in the air
        if (!controller.isGrounded)
        {
            print("we are not grounded!");
            return false;
        }
        print("we are grounded!");

        Vector3 JumpVelocity = new Vector3(0, JumpHeight, 0);
        controller.Move(JumpVelocity);

        return true;
    }
}
