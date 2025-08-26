using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rigidBody;
    [HideInInspector] public PlayerController controller;
    [HideInInspector] public Transform groundBox;
    [HideInInspector] public LayerMask groundLayer;

    [HideInInspector] public Transform wallBox;

    public virtual void Start()
    {
        TryGetComponent(out rigidBody);
        groundBox = transform.GetChild(0);
        groundLayer = LayerMask.GetMask("Ground");

        wallBox = transform.GetChild(1);


        controller = GetComponent<PlayerController>();
        GetComponent<PlayerInput>().onActionTriggered += Action;
    }
    public virtual void Action(InputAction.CallbackContext context) { }

    private void OnDestroy()
    {
        GetComponent<PlayerInput>().onActionTriggered -= Action;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundBox.position, new Vector2(0.345f, 0.05f), 0f, groundLayer);
    }

    public bool IsWalled()
    {
        if (IsGrounded()) return false;
        if ((rigidBody.velocity.x > 0 && transform.rotation.y == 0) || (rigidBody.velocity.x < 0 && transform.rotation.y != 0))
        {
            return Physics2D.OverlapCircle(wallBox.position, 0.3f, groundLayer);
        }
        else
        {
            return false;
        }

        
    }

}
