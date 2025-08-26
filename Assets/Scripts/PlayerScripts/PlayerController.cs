using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(PlayerAction))]
public class PlayerController : MonoBehaviour
{
    private CameraFollowObject cameraFollowObject;
    private Rigidbody2D rigidBody;

    [HideInInspector] public Animator animator;

    private bool isMoving = false;
    private bool isGrounded = true;
    private bool isWalled = false;
    private bool isWallJumping = false;

    public bool IsMoving { get { return isMoving; } set 
        { 
            isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        } 
    }

    public bool IsGrounded
    {
        get { return isGrounded; }
        set
        {
            isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    public bool IsWalled
    {
        get { return isWalled; }
        set
        {
            isWalled = value;
            animator.SetBool(AnimationStrings.isWalled, value);
        }
    }

    public bool IsWallJumping
    {
        get { return isWallJumping; }
        set
        {
            isWallJumping = value;
        }
    }



    private void Awake()
    {
        cameraFollowObject = (CameraFollowObject)FindObjectOfType(typeof(CameraFollowObject));
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(TryGetComponent(out PlayerMovement playerMovement))
        {
            IsMoving = playerMovement.isMoving;
        }
        
        IsGrounded = GetComponent<PlayerAction>().IsGrounded();

        IsWalled = GetComponent<PlayerAction>().IsWalled();

        if(TryGetComponent(out PlayerWallJump playerWallJump))
        {
            IsWallJumping = playerWallJump.isWallJumping;
        }



        animator.SetFloat(AnimationStrings.yVelocity, rigidBody.velocity.y);
    }



    public void CallFlip()
    {
        cameraFollowObject.CallFlip();
    }

}
