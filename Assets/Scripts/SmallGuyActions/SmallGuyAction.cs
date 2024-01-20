using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using Unity.Netcode;
using Unity.Multiplayer.Samples.Utilities.ClientAuthority;

public abstract class SmallGuyAction : NetworkBehaviour
{
    private readonly int smallGuyCost;    
    public int SmallGuyCost => smallGuyCost;
    public Player user;
    public abstract void Cast();
=======

public abstract class SmallGuyAction : ScriptableObject
{
    private readonly float smallGuyCost;    
    public float SmallGuyCost => smallGuyCost;
    public abstract void OnCast();
>>>>>>> 3eb06d622e80a5bf2e508f9f9786a6f84ceb83e6

}
