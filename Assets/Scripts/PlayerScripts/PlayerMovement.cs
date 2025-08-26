using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : PlayerAction
{


    [HideInInspector] public float horizontal;
    [SerializeField] private float speed = 10f;
    private bool isFacingRight = true;
    public bool isMoving;
    public bool IsFacingRight { get { return isFacingRight; } set { isFacingRight = value; } }




    public void Update()
    {
        if(Time.timeScale == 0f)
        {
            horizontal = 0f;
            isMoving = false;
        }



        FlipCheck();


        if (Input.GetKey(KeyCode.U))
        {
            Pixelizable[] pixelList = FindObjectsOfType<Pixelizable>();
            foreach (Pixelizable pixel in pixelList)
            {
                pixel.pixelStatus = Pixelizable.PixelStatus.Retro;
            }
        }

    }

    void FixedUpdate()
    {
        if(!IsWalled() && !controller.IsWallJumping)
            rigidBody.velocity = new Vector2(horizontal * speed, rigidBody.velocity.y);
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 rotator = new Vector3(transform.rotation.x, isFacingRight ? 0f : 180f, transform.rotation.z);
        transform.rotation = Quaternion.Euler(rotator);

        controller.CallFlip();
    }

    private void FlipCheck()
    {
        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }
    }



    public override void Action(InputAction.CallbackContext context)
    {

        if (context.action.name != "Move" || Time.timeScale == 0f) return;

        if (context.canceled) isMoving = false;
        if (context.performed) isMoving = true;

        horizontal = context.ReadValue<Vector2>().x;
       
    }



}
