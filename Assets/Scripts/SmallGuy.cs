using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class SmallGuy : NetworkBehaviour
{
    [SerializeField]
    private Player owner;
    private Vector3 direction;
    private const float wanderRadius = 5f;

    [SerializeField]
    private const float wanderSpeed = 4f;
    [SerializeField]
    private float followSpeed = 30f;
    private Vector2 followOffset;
    private Vector3 followOffsetVelocity;
    private Vector3 followTargetPos;

    // Start is called before the first frame update
    void Start()
    {
        followSpeed = Random.Range(15f, 20f);
        followOffset = Random.insideUnitCircle * 20f;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsServer) {
            if (!owner) {
                //wander around
                transform.position += wanderSpeed * Time.deltaTime * transform.forward;
                transform.rotation *= Quaternion.Euler(0f, 60f * Time.deltaTime, 0f);
            }
            else if (owner) {
                followTargetPos = new Vector3(owner.transform.position.x + followOffset.x, transform.position.y, owner.transform.position.z + followOffset.y);
                if (Vector3.Distance(transform.position, followTargetPos) > 2f) {
                    transform.LookAt(followTargetPos);
                    transform.position += followSpeed * Time.deltaTime * transform.forward;
                }
            }
        }
    }

    public Player Owner {
        get {return owner;}
        set {
            owner = value;
        }
    }
    
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, followTargetPos);
    }
}
