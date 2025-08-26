using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerJump : PlayerAction
{
    

    [SerializeField] private float jumpingPower = 12f;
    [SerializeField] private float coyoteTime = 0.15f;
    [SerializeField] private float jumpBufferTime = 0.1f;

    [HideInInspector] public float coyoteTimeCounter;
    [HideInInspector] public float jumpBufferCounter;
    [HideInInspector] public float jumpReleaseCounter;


    public override void Action(InputAction.CallbackContext context)
    {

        if (context.action.name != "Jump" || Time.timeScale == 0f) return;


        if (!IsWalled() && !controller.IsWallJumping)
        {
            jumpBufferCounter = context.performed ? jumpBufferTime : jumpBufferCounter;
            jumpReleaseCounter = context.canceled ? jumpBufferTime : jumpReleaseCounter;
        }

        
 
        

    }


    public void Update()
    {
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            controller.animator.ResetTrigger(AnimationStrings.jump);
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        jumpBufferCounter -= Time.deltaTime;
        jumpReleaseCounter -= Time.deltaTime;


        Jump();


    }


    private void Jump()
    {
        //if player pressed jump
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            jumpBufferCounter = 0f;

            controller.animator.SetTrigger(AnimationStrings.jump);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingPower);

        }
        //jump height increases/decreases based on how long space was pressed
        if (jumpReleaseCounter > 0f && rigidBody.velocity.y > 0f)
        {
            jumpReleaseCounter = 0f;

            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * 0.5f);
            controller.animator.ResetTrigger(AnimationStrings.jump);

            coyoteTimeCounter = 0f;
        }
    }

}
