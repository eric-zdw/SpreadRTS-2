using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SmallGuyAction : ScriptableObject
{
    private readonly float smallGuyCost;    
    public float SmallGuyCost => smallGuyCost;
    public abstract void OnCast();

}
