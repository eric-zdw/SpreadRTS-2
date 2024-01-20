using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Multiplayer.Samples.Utilities.ClientAuthority;

public abstract class SmallGuyAction : NetworkBehaviour
{
    private readonly int smallGuyCost;    
    public int SmallGuyCost => smallGuyCost;
    public Player user;
    public abstract void Cast();

}
