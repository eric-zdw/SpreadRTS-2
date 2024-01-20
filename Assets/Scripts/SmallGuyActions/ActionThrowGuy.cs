using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ActionThrowGuy : SmallGuyAction
{
    public GameObject throwProjectile;
    
    public override void Cast() {
        if (user.IsOwner) {
            SpawnGrenadeServerRpc();
        }
    }

    [ServerRpc]
    void SpawnGrenadeServerRpc() {
        GameObject obj = Instantiate(throwProjectile, user.transform.position, Quaternion.identity);
        obj.GetComponent<NetworkObject>().Spawn();

        user.SmallGuysCount -= SmallGuyCost;
    }

}
