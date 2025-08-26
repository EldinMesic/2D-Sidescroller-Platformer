using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PickUps/AbilityUnlocker")]
public class AbilityUnlocker : PickUpEffect
{
    enum Abilities
    {
        PlayerMovement,
        PlayerJump,
        PlayerDoubleJump,
        PlayerWallJump
    }

    [SerializeField] private Abilities ability;
    [SerializeField] private string message;

    public override void Apply(GameObject target)
    {
        if (target.gameObject.GetComponent(ability.ToString()) == null)
        {
            target.gameObject.AddComponent(Type.GetType(ability.ToString()));
            FindFirstObjectByType<AbilityTextManager>().GetComponent<AbilityTextManager>().ShowAbilityMessage(message);
        }
            
    }
}
