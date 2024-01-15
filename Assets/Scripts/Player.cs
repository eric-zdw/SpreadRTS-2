using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Multiplayer.Samples.Utilities.ClientAuthority;

[RequireComponent(typeof(ClientNetworkTransform))]
public class Player : NetworkBehaviour
{
    private float smallGuyFollowRadius;
    private int smallGuysCount = 20;
    [SerializeField]
    private GameObject smallGuyPrefab;

    [SerializeField]
    private int teamIndex;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int SmallGuysCount {
        get {return smallGuysCount;}
        set {
            smallGuysCount = value;
            smallGuyFollowRadius = Mathf.Sqrt(smallGuysCount * 0.1f / Mathf.PI);
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner) {
            SpawnSmallGuyServerRpc(smallGuysCount);
        }
    }

    [ServerRpc]
    private void SpawnSmallGuyServerRpc(int count) {
        for (int i = 0; i < count; i++) {
            Vector2 rand = Random.insideUnitCircle * smallGuyFollowRadius;
            SmallGuy sg = Instantiate(smallGuyPrefab, transform.position + new Vector3(rand.x, 0f, rand.y), Quaternion.identity).GetComponent<SmallGuy>();
            sg.GetComponent<NetworkObject>().Spawn();
            sg.Owner = this;
        }
    }
}
