using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineFollow : MonoBehaviour
{
    CinemachineVirtualCamera vc;

    // Start is called before the first frame update
    void Start()
    {
        vc = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!vc.Follow) {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player")) {
                if (g.GetComponent<Player>().IsOwner) {
                    vc.Follow = g.transform;
                }
            }
        }
    }
}
