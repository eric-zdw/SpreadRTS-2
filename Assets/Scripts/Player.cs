using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Multiplayer.Samples.Utilities.ClientAuthority;

[RequireComponent(typeof(ClientNetworkTransform))]
public class Player : NetworkBehaviour
{
<<<<<<< HEAD
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
=======
    private float smallGuyFollowRadius;
    private int smallGuysCount = 20;
    [SerializeField]
    private GameObject smallGuyPrefab;

    [SerializeField]
    private int teamIndex;
>>>>>>> 3eb06d622e80a5bf2e508f9f9786a6f84ceb83e6

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD

=======
>>>>>>> 3eb06d622e80a5bf2e508f9f9786a6f84ceb83e6
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int SmallGuysCount {
<<<<<<< HEAD
        get {return smallGuysCount.Value;}
        set {
            smallGuysCount.Value = value;
            smallGuyFollowRadius.Value = Mathf.Sqrt(smallGuysCount.Value * 0.1f / Mathf.PI);
=======
        get {return smallGuysCount;}
        set {
            smallGuysCount = value;
            smallGuyFollowRadius = Mathf.Sqrt(smallGuysCount * 0.1f / Mathf.PI);
>>>>>>> 3eb06d622e80a5bf2e508f9f9786a6f84ceb83e6
        }
    }

    public override void OnNetworkSpawn()
    {
<<<<<<< HEAD
        print("calling?1");
        if (IsOwner) {
            print("calling?2");
            SpawnSmallGuyServerRpc(20);
=======
        if (IsOwner) {
            SpawnSmallGuyServerRpc(smallGuysCount);
>>>>>>> 3eb06d622e80a5bf2e508f9f9786a6f84ceb83e6
        }
    }

    [ServerRpc]
    private void SpawnSmallGuyServerRpc(int count) {
<<<<<<< HEAD
        print("calling3");
        for (int i = 0; i < count; i++) {
            print("Yay!");
            Vector2 rand = Random.insideUnitCircle * smallGuyFollowRadius.Value;
=======
        for (int i = 0; i < count; i++) {
            Vector2 rand = Random.insideUnitCircle * smallGuyFollowRadius;
>>>>>>> 3eb06d622e80a5bf2e508f9f9786a6f84ceb83e6
            SmallGuy sg = Instantiate(smallGuyPrefab, transform.position + new Vector3(rand.x, 0f, rand.y), Quaternion.identity).GetComponent<SmallGuy>();
            sg.GetComponent<NetworkObject>().Spawn();
            sg.Owner = this;
        }
    }
}
