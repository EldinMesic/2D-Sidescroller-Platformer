using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollowObject : MonoBehaviour
{
    [Header("Controls for the falling offset")]
    [SerializeField] private float fallingTreshold = -15f;
    [SerializeField] private float fallingTime = 0.4f;
    [SerializeField] private float fallingOffset = -3f;

    [Header("Controls for follow camera flip rotation time")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float flipRotationTime = 0.5f;


    [SerializeField] private CinemachineVirtualCamera currCamera;
    private CinemachineFramingTransposer framingTransposer;
    private bool isFalling;



    private PlayerMovement player;
    private bool isFacingRight;

    private void Start()
    {
        player = playerTransform.gameObject.GetComponent<PlayerMovement>();
        isFacingRight = player.IsFacingRight;

        framingTransposer = currCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position;
        LerpYOffset();
    }


    public void CallFlip()
    {
        LeanTween.rotateY(gameObject, DetermineEndRotation(), flipRotationTime).setEaseInOutSine();
    }
    private float DetermineEndRotation()
    {
        isFacingRight = !isFacingRight;
        return isFacingRight ? 0f : 180f;
    }



    public void LerpYOffset()
    {
        if (isFalling == player.rigidBody.velocity.y < fallingTreshold) return;

        LeanTween.cancel(gameObject);

        isFalling = player.rigidBody.velocity.y < fallingTreshold;

        float startValue = framingTransposer.m_TrackedObjectOffset.y;
        float endValue = player.rigidBody.velocity.y < fallingTreshold ? fallingOffset : 0f;
        float modifiedFallingTime = player.rigidBody.velocity.y < fallingTreshold ? fallingTime : fallingTime / 2f;

        LeanTween.value(gameObject, SetTransposerValue ,startValue, endValue, modifiedFallingTime);

    }

    private void SetTransposerValue(float value)
    {
        framingTransposer.m_TrackedObjectOffset.y = value;
    }




}
