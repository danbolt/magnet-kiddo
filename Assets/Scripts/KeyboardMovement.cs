using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardMovement : MonoBehaviour {

    private Rigidbody body;

    public float WalkSpeed = 10;

	// Use this for initialization
	void Start () {
        this.body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 xInput = Input.GetAxis("Horizontal") * new Vector3(1, 0, 0) * this.WalkSpeed;
        Vector3 yInput =   Input.GetAxis("Vertical") * new Vector3(0, 0, 1) * this.WalkSpeed;

        this.body.velocity = new Vector3(0, this.body.velocity.y, 0) + xInput + yInput;
	}
}
