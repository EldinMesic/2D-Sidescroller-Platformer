using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerDoubleJump : PlayerAction
{
    

    [SerializeField] private float jumpingPower = 10f;
    private bool hasDoubleJump = true;

    public override void Action(InputAction.CallbackContext context)
    {

        if (context.action.name != "Jump" || Time.timeScale == 0f) return;
        


        if (context.performed && hasDoubleJump && !IsGrounded() && !IsWalled() && !controller.IsWallJumping)
        {
            hasDoubleJump = false;   
            controller.animator.SetTrigger(AnimationStrings.jump);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingPower);
        }

    }

    private void Update()
    {
        if (IsGrounded()) hasDoubleJump = true;
    }


}
