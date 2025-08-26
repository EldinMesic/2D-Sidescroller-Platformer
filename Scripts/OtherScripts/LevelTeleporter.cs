using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTeleporter : MonoBehaviour
{
    private CinemachineVirtualCamera currCamera;
    private Transform teleportLocation;
    private Transform player;

    private void Awake()
    {
        currCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        player = GameObject.Find("Player").transform;
        teleportLocation = GameObject.Find("PlayerSpawn").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            LevelManager.Instance.LoadNextScene();
            currCamera.enabled = false;
            player.position = teleportLocation.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            currCamera.enabled = true;
        }
    }
}

