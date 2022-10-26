using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isPickUp : MonoBehaviour
{

    public float waitOnPickup = 0.2f;
    public float breakForce = 35f;
    private int gravityObject = 1;
    [HideInInspector] public bool pickedUp = false;
    [HideInInspector] public pickUpIObject playerInteractions;
    [SerializeField] private Rigidbody rb;


    private void OnCollisionEnter(Collision collision)
    {
        if (pickedUp)
        {
            if (collision.relativeVelocity.magnitude > breakForce)
            {
                playerInteractions.BreakConnection();
            }

        }
    }

    //this is used to prevent the connection from breaking when you just picked up the object as it sometimes fires a collision with the ground or whatever it is touching
    public IEnumerator PickUp()
    {
        yield return new WaitForSecondsRealtime(waitOnPickup);
        pickedUp = true;

    }

    public void synLocalGrabity(int gravity)
    {
        gravityObject = gravity;
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(0,-10 * gravityObject, 0) , ForceMode.Acceleration);
    }
}

