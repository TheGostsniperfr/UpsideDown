using UnityEngine;

public class Active_Generator : MonoBehaviour
{
    
    [SerializeField] private GameObject ring1;
    [SerializeField] private GameObject ring2;
    [SerializeField] private GameObject ring3;
    [SerializeField] private GameObject ring4;

    private Vector3 axeY = new Vector3 (0,1,0);
    private Vector3 axeX = new Vector3 (1,0,0);
    private Vector3 axeDiag = new Vector3 (1,1,0);

    private float speed = 50f;
    

    // Update is called once per frame
    void Update()
    {
        ring1.transform.Rotate(axeY * speed * Time.deltaTime);
        ring2.transform.Rotate(axeX * speed * Time.deltaTime);
        ring3.transform.Rotate(axeDiag * speed * Time.deltaTime);
        ring4.transform.Rotate(axeDiag * -speed * Time.deltaTime);
    }
}
