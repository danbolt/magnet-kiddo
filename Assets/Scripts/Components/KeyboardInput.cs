using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardInput : MonoBehaviour
{

    private CharacterMovementComp charMoveComp;

	// Use this for initialization
	void Start ()
    {
        this.charMoveComp = GetComponent<CharacterMovementComp>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 movementDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (Vector3.Magnitude(movementDir) > 0.1f)
        {
            charMoveComp.MoveCharacter(movementDir);
        }
        else
        {
            // ignore negligible movement input
            charMoveComp.MoveCharacter(new Vector3(0,0,0));
        }

        if (Input.GetButtonDown("Jump"))
        {
            charMoveComp.Jump();
        }
    }
}
