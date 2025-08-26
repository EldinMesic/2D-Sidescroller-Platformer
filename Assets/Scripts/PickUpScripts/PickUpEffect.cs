using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PickUpEffect : ScriptableObject
{
    public Sprite sprite;
    public abstract void Apply(GameObject target);
    
}
