using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XR_AN_PlugScript : MonoBehaviour
{
    [Tooltip("Feature for one using only")]
    public bool OneTime = false;
    [Tooltip("Plug follow this local EmptyObject")]
    public Transform HeroHandsPosition;
    [Tooltip("SocketObject with collider(shpere, box etc.) (is trigger = true)")]
    public Collider Socket; // need Trigger
    public XR_AN_DoorScript DoorObject;

    bool follow = false, isConnected = false, followFlag = false, youCan = true;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void TryAction()
    {
        if (youCan) Interaction();

        // frozen if it is connected to PowerOut
        if (isConnected)
        {
            gameObject.transform.position = Socket.transform.position;
            gameObject.transform.rotation = Socket.transform.rotation;
            DoorObject.isOpened = true;
        }
        else
        {
            DoorObject.isOpened = false;
        }
    }

    void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.E) && !follow)
        {
            isConnected = false; // unfrozen
            follow = true;
            followFlag = false;
        }

        if (follow)
        {
            rb.drag = 10f;
            rb.angularDrag = 10f;
            if (followFlag)
            {
                var distance = Vector3.Distance(transform.position, Camera.main.transform.position);
                if (distance > 3f || Input.GetKeyDown(KeyCode.E))
                {
                    follow = false;
                }
            }

            followFlag = true;
            rb.AddExplosionForce(-1000f, HeroHandsPosition.position, 10f);
            // second variant of following
            //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, objectLerp.position, 1f);
        }
        else
        {
            rb.drag = 0f;
            rb.angularDrag = .5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == Socket)
        {
            isConnected = true;
            follow = false;
            DoorObject.rbDoor.AddRelativeTorque(new Vector3(0, 0, 20f));
        }
        if (OneTime) youCan = false;
    }
}
