using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnTeleporter : MonoBehaviour
{
    private Transform teleportLocation;
    private Transform player;
    private Animator animator;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        teleportLocation = GameObject.Find("PlayerSpawn").transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(KillPlayer(collision));
        }
    }




    IEnumerator KillPlayer(Collider2D collision)
    {
        collision.GetComponent<PlayerInput>().enabled = false;
        animator = collision.GetComponent<Animator>();

        animator.Play(AnimationStrings.playerDeathAnimation);

        yield return new WaitForSeconds(2.5f);

        animator.SetTrigger(AnimationStrings.respawnPlayer);
        collision.GetComponent<PlayerInput>().enabled = true;
        player.position = teleportLocation.position;

        yield return new WaitForSeconds(0.1f);
        animator.ResetTrigger(AnimationStrings.respawnPlayer);

    }





}
