using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Multiplayer.Samples.Utilities.ClientAuthority;

[RequireComponent(typeof(ClientNetworkTransform))]
public class Player : NetworkBehaviour
{
    private NetworkVariable<float> smallGuyFollowRadius = new NetworkVariable<float>();
    private NetworkVariable<int> smallGuysCount = new NetworkVariable<int>();
    [SerializeField]
    private GameObject smallGuyPrefab;
    public SmallGuyAction[] actions;

    [SerializeField]
    private PlayerTeam team;
    public PlayerTeam Team {
        get { return team; }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int SmallGuysCount {
        get {return smallGuysCount.Value;}
        set {
            smallGuysCount.Value = value;
            smallGuyFollowRadius.Value = Mathf.Sqrt(smallGuysCount.Value * 0.1f / Mathf.PI);
        }
    }

    public override void OnNetworkSpawn()
    {
        print("calling?1");
        if (IsOwner) {
            print("calling?2");
            SpawnSmallGuyServerRpc(20);
        }
    }

    [ServerRpc]
    private void SpawnSmallGuyServerRpc(int count) {
        print("calling3");
        for (int i = 0; i < count; i++) {
            print("Yay!");
            Vector2 rand = Random.insideUnitCircle * smallGuyFollowRadius.Value;
            SmallGuy sg = Instantiate(smallGuyPrefab, transform.position + new Vector3(rand.x, 0f, rand.y), Quaternion.identity).GetComponent<SmallGuy>();
            sg.GetComponent<NetworkObject>().Spawn();
            sg.Owner = this;
        }
    }
}
