using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TossProjectile : NetworkBehaviour
{
    public Vector3 target;
    private Color color;
    private Player owner;
    [SerializeField]
    private const float tossDuration = 2f;
    private float tossTime = 5f; 
    private float tossHeight;

    private float t = 0f;

    void Start() {
        tossTime = 5f;
        //transform.position = owner.transform.position;
        tossHeight = target.y - transform.position.y + 10f;

        Vector3 displacement = target - transform.position;
        float gravity = Physics.gravity.y * 2f;

        float uX = displacement.x / (Mathf.Sqrt(-2f * tossHeight / gravity) + Mathf.Sqrt(2f * (displacement.y - tossHeight) / Physics.gravity.y));
        float uZ = displacement.z / (Mathf.Sqrt(-2f * tossHeight / Physics.gravity.y) + Mathf.Sqrt(2f * (displacement.y - tossHeight) / Physics.gravity.y));
        float uY = Mathf.Sqrt(-2f * Physics.gravity.y * tossHeight);

        GetComponent<Rigidbody>().velocity = new Vector3(uX, uY, uZ);
    }

    public override void OnNetworkSpawn()
    {
        print("working?");
        if (IsServer) {
            tossTime = 5f;
            //transform.position = owner.transform.position;
            tossHeight = target.y - transform.position.y + 10f;
    
            Vector3 displacement = target - transform.position;
    
            float uX = displacement.x / (Mathf.Sqrt(-2f * tossHeight / Physics.gravity.y) + Mathf.Sqrt(2f * (displacement.y - tossHeight) / -18f));
            float uZ = displacement.z / (Mathf.Sqrt(-2f * tossHeight / Physics.gravity.y) + Mathf.Sqrt(2f * (displacement.y - tossHeight) / -18f));
            float uY = Mathf.Sqrt(-2f * Physics.gravity.y * tossHeight);
    
            GetComponent<Rigidbody>().velocity = new Vector3(uX, uY, uZ);
        }
    }

    public void FixedUpdate() {
        if (IsServer) {
            t += Time.deltaTime;
            Vector3 xz = Vector3.LerpUnclamped(new Vector3(owner.transform.position.x, 0f, owner.transform.position.z), new Vector3(target.x, 0f, target.z), t / tossDuration);
            //transform.position = Vector3.Lerp(transform.position, target, t / tossDuration);
        }
    }

    public Player Owner {
        get { return owner; }
        set {
            owner = value;
            color = owner.Team.color;
        }
    }
}
