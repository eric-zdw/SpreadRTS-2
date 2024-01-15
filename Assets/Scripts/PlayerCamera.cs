using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private Player playerTarget;

    [SerializeField]
    private Vector3 offset;

    private Vector3 lookDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!playerTarget) {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player")) {
                if (g.GetComponent<Player>().IsOwner) {
                    playerTarget = g.GetComponent<Player>();
                }
            }
        }
        else {
            transform.position = playerTarget.transform.position + playerTarget.transform.forward * offset.z + playerTarget.transform.up * offset.y + playerTarget.transform.right * offset.x;
            transform.LookAt(playerTarget.transform.position + Vector3.up * 2f);
        }
        //transform
    }
}
