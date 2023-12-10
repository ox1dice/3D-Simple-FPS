using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    
    bool isGrounded;
    bool lerpCrouch = false;
    float crouchTimer = 0f;
    bool crouching = false;
    bool sprinting = false;
    
    public float speed = 8f;
    public float gravity = -9.81f * 3;
    public float jumpHeight = 2f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if(lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if(crouching){
                controller.height = Mathf.Lerp(controller.height, 1, p);
            } else {
                controller.height = Mathf.Lerp(controller.height, 2, p);
            }

            if(p > 1){
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;

        moveDirection.x = input.x;
        moveDirection.z = input.y;

        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;

        if(isGrounded && playerVelocity.y < 0){
            playerVelocity.y = -2f;
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;

        if (crouching)
        {
            speed = 4;
        }
        else
        {
            speed = 8;
        }
    }

    public void Sprint()
    {
        sprinting = !sprinting;

        if(sprinting)
        {
            speed = 12;
        } 
        else 
        {
            speed = 8;
        }
    }
}
