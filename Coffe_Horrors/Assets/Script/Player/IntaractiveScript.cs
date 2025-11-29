using UnityEngine;

public class IntaractiveScript : MonoBehaviour
{

    //[SerializeField] private Transform _cameraRotate;

    void Start()
    {
        
    }


    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3f))
        {
            Debug.DrawRay(transform.position, transform.forward * 3, Color.green);
            Debug.Log("Did Hit");

        }else
        {
            Debug.DrawRay(transform.position, transform.forward * 3, Color.red);
            Debug.Log("Did not hit");

        }
        
    }
}
