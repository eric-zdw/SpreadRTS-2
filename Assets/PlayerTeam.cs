using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerTeam : NetworkBehaviour
{
    public int index;
    public Color color;
    public Player[] players;
    public int numberOfTiles;
}
