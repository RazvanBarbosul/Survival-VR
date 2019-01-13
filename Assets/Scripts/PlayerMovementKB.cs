using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementKB : MonoBehaviour {


    public float speed = 6f;
    public float jumpSpeed = 8f;
    public float gravity = 2f;
    private Vector3 moveDirection = Vector3.zero;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Controller();

    }

    private void Controller()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);


        float mouseinputX = Input.GetAxis("Mouse X");
        float mouseinputY = Input.GetAxis("Mouse Y");
        Vector3 lookhere = new Vector3(-mouseinputY, mouseinputX, 0);
        transform.Rotate(lookhere);
    }
}
