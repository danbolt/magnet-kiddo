// handles movement by calling movement in CharacterController, while also handling physics and movement speed
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovementComp : MonoBehaviour {

    public float GravityScale = 1.0f;
    public float MoveSpeed = 10.0f;

    // how long to apply upwards velocity after jump is pressed
    public float JumpDuration = 0.2f;
    // how long after jumping must we wait until we can jump again
    public float JumpCooldownDuration = 0.0f;
    // upwards velocity when jump is pressed, applied for JumpDuration seconds (scales from *1.0 - *0.0 during this duration)
    public float JumpVelocity = 20.0f;

    private float lastJumpedTime;

    private CharacterController controller;

    private Vector3 moveVelocity = new Vector3(0,0,0);

    private Vector3 movementInputDir = new Vector3(0, 0, 0);

    private Quaternion LastRotation;

    // Use this for initialization
    void Start ()
    {
        lastJumpedTime = Time.time - 50.0f;
        this.controller = GetComponent<CharacterController>();
        LastRotation = transform.rotation;
    }
	
	// Update is called once per frame
	void Update ()
    {
        moveVelocity = new Vector3(0, 0, 0);

        // apply jump velocity
        if (Time.time < lastJumpedTime + JumpDuration)
        {
            float remainingJumpTime = Time.time - lastJumpedTime;
            float currentJumpVelocity = (JumpDuration / remainingJumpTime) * JumpVelocity;
            moveVelocity += new Vector3(0, currentJumpVelocity, 0) * Time.deltaTime;
        }

        // apply gravity if we are in the air - do this last to prevent conflicts
        if (!controller.isGrounded)
        {
            moveVelocity += GetCurrentGravity() * Time.deltaTime;
        }
        // add movement input
        moveVelocity += movementInputDir;

        RotateCharacterToMovement();

        // FINALLY, apply movement
        // ONLY CALL THIS ONCE PER FRAME!!! or it will mess up, since it uses absolute velocities....
        this.controller.Move(moveVelocity);
    }
    Vector3 GetCurrentGravity()
    {
        return Physics.gravity * GravityScale;
    }
    public void MoveCharacter(Vector3 motion)
    {
        // clamped to prevent moving faster than movement speed, multiplied by delta time to keep us framerate independent
        movementInputDir = motion * MoveSpeed * Time.deltaTime;

    }
    public bool Jump()
    {
        // don't allow jumping if already in the air
        // TODO: replace with raycast to ground to check if we are grounded. this grounded check sucks ass, doesn't even work half the time...
        if (!controller.isGrounded || lastJumpedTime + JumpCooldownDuration > Time.time)
        {
            return false;
        }

        lastJumpedTime = Time.time;
        return true;
    }

    void RotateCharacterToMovement()
    {
        if (movementInputDir.magnitude > 0.0f)
        {
            Quaternion LerpedRotation;
            // lerping smoothes out movement, but its somewhat unrealistic (we interpolate at the same rate, no matter how big/small the change in angle is)
            LerpedRotation = Quaternion.Lerp(LastRotation, Quaternion.LookRotation(movementInputDir, new Vector3(0, 1, 0)), 0.3f);
            this.transform.rotation = LerpedRotation;
        }
        else
        {
            this.transform.rotation = LastRotation;
        }
        LastRotation = this.transform.rotation;

    }
}
