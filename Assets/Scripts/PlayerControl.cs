using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : NetworkBehaviour
{
    CharacterController controller;
    private Vector2 walkDirection;
    private Vector2 walkVelocity;

    [SerializeField]
    private float moveSpeed = 50f;
    private Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner) {
            GetComponent<PlayerInput>().enabled = true;
        }
    }

    void OnMove(InputValue value) {
        var dir = value.Get<Vector2>();

        //print("ID " + OwnerClientId + ": " + IsOwner);

        if (IsOwner) {
            //print("is sending");
            //MovePlayerServerRpc(dir);
            walkDirection = dir;
        }
    }

    void OnLook(InputValue value) {
        if (IsOwner) {
            float sensitivity = PlayerPrefs.GetFloat("LookSensitivity", 0.5f);
            Vector2 diff = value.Get<Vector2>() * sensitivity;
            mousePos = new Vector2(mousePos.x + diff.x, Mathf.Clamp(mousePos.y + diff.y, -90f, 90f));
            //print("mousePos " + mousePos);

            transform.rotation = Quaternion.Euler(0f, mousePos.x, 0f);
        }
    }

    [ServerRpc]
    void MovePlayerServerRpc(Vector2 dir)
    {
    }

    void FixedUpdate() {
        Vector3 translatedVector = (transform.right * walkDirection.x) + (transform.forward * walkDirection.y);
        //print("transled " + translatedVector);
        walkVelocity += moveSpeed * Time.deltaTime * new Vector2(translatedVector.x, translatedVector.z);

        GetComponent<CharacterController>().Move(new Vector3(walkVelocity.x, 0f, walkVelocity.y) * Time.deltaTime);

        float slowFactor = 0.95f;
        if (Vector3.Dot(translatedVector, walkVelocity) <= 0f) {
            slowFactor = 0.9f;
        }
        walkVelocity *= slowFactor;
        
        //print(slowFactor);
    }
    
}