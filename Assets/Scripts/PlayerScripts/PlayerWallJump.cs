using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerWallJump : PlayerAction
{
    private bool isWallSliding;
    [SerializeField] private float wallSlidingSpeed = 1.5f;


    public bool isWallJumping;
    private float wallJumpingDirection;
    [SerializeField] private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    [SerializeField] private float wallJumpingDuration = 0.2f;
    [SerializeField] private Vector2 wallJumpingPower = new Vector2(8f, 10f); 

    public void Update()
    {
        WallSlide();
        WallJump();
    }



    public override void Action(InputAction.CallbackContext context)
    {

        if (context.action.name != "Jump" || Time.timeScale == 0f) return;


        if(context.performed && !IsGrounded() && wallJumpingCounter > 0f)
        {

            if (TryGetComponent(out PlayerMovement movement))
            {
                if (movement.IsFacingRight == (transform.rotation.y == 0))
                {
                    movement.Flip();
                }

            }


            isWallSliding = false;
            isWallJumping = true;
            rigidBody.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            controller.animator.SetTrigger(AnimationStrings.jump);



            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
        

    }

    private void StopWallJumping()
    {
        isWallJumping = false;
        controller.animator.ResetTrigger(AnimationStrings.jump);
    }


    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = transform.rotation.y >= 0 ? -1 : 1;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        



    }


    private void WallSlide()
    {
        if(IsWalled() &&  !IsGrounded())
        {
            isWallSliding = true;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Clamp(rigidBody.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }



}
