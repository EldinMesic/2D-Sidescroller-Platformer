using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private PickUpEffect effect;


    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = effect.sprite;
        PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
        collider.isTrigger = true;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            
            effect.Apply(collider.gameObject);
            
        }
            
    }
}
