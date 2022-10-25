
using UnityEngine;

public class pickUpIObject : MonoBehaviour
{
    [Header("Pickup settings")]
    [SerializeField] private Transform pickUpArea;
    [SerializeField] private GameObject obj;
    private Rigidbody objRB;

    [Header("physics parameters")]
    [SerializeField] private float pickUpRange = 3.0f;
    [SerializeField] private float pickUpForce = 150.0f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(obj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    pickUpObject(hit.transform.gameObject);
                }
            }
            else
            {
                dropObject();
            }
        }
        if (obj != null)
        {
            moveObject();
        }
    }

    private void moveObject()
    {
        if(Vector3.Distance(obj.transform.position, pickUpArea.position) > 0.1f)
        {
            Vector3 moveDirection = (pickUpArea.position - obj.transform.position);
            objRB.AddForce(moveDirection * pickUpForce);
        }
    }

    private void pickUpObject(GameObject item)
    {
        if (item.GetComponent<Rigidbody>())
        {
            objRB = item.GetComponent<Rigidbody>();
            objRB.useGravity =false;
            objRB.drag = 10;
            objRB.constraints = RigidbodyConstraints.FreezeRotation;

            objRB.transform.parent = pickUpArea;
            obj = item;
        }
    }

    private void dropObject()
    {

        objRB.useGravity = true;
        objRB.drag = 1;
        objRB.constraints = RigidbodyConstraints.None;

        objRB.transform.parent = null;
        obj = null;
        
    }

}
