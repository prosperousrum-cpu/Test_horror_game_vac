using Player;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.Progress;

public class Triger_coffe_cup : MonoBehaviour
{
    [SerializeField] private int _timer_ready = 3;
    [SerializeField] private int _Count_coffe_cup = 2;
    [SerializeField] private GameObject Count_null;
    
    public PickUper _Hold;
    public Transform Coffe_cup_position_in_coffe_machine_position;
    public Transform Coffe_cup_cap_in_coffe_cup_position;

    private GameObject Coffe_cup_in_machine;
    public GameObject Coffe_cup_cap_in_machine;

    private bool _Coffe_cup_in_machine = false;
    private bool _Coffe_cup_in_machine_ready = false;

    private void Example()
    {
        Coffe_cup_in_machine.transform.Find("Cylinder").GetComponent<Renderer>().enabled = true;
        _Coffe_cup_in_machine_ready = true;
        _Count_coffe_cup--;
    }

    private void OnTriggerEnter(Collider other)    // when the coffe object enter in the coffe machine
    {

        if ((_Count_coffe_cup == 0)) { Count_null.GetComponent<Renderer>().enabled = true; }

        if (other.gameObject.CompareTag("Coffe_cup") && !_Coffe_cup_in_machine && !(_Count_coffe_cup == 0))
        {
            Coffe_cup_in_machine = other.gameObject;
            Coffe_cup_in_machine.GetComponent<Rigidbody>().isKinematic = true;
            Coffe_cup_in_machine.transform.position = Coffe_cup_position_in_coffe_machine_position.position;
            Coffe_cup_in_machine.transform.rotation = Coffe_cup_position_in_coffe_machine_position.rotation;

            Coffe_cup_cap_in_coffe_cup_position = Coffe_cup_in_machine.transform.GetChild(0);



            Invoke("Example", _timer_ready);

            /*other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.transform.position = Coffe_cup_position_in_coffe_machine_position.position;
            other.gameObject.transform.rotation = Coffe_cup_position_in_coffe_machine_position.rotation;   */

            _Coffe_cup_in_machine = true;


        }

        


        

        if (_Coffe_cup_in_machine && other.CompareTag("Coffe_cup_cap") && _Coffe_cup_in_machine_ready )
        {
            Coffe_cup_cap_in_machine = other.gameObject;
            Coffe_cup_cap_in_machine.GetComponent<Rigidbody>().isKinematic = true;
            Coffe_cup_cap_in_machine.transform.SetParent(Coffe_cup_cap_in_coffe_cup_position);
            Coffe_cup_cap_in_machine.transform.position = Coffe_cup_cap_in_coffe_cup_position.position;
            Coffe_cup_cap_in_machine.transform.rotation = Coffe_cup_cap_in_coffe_cup_position.rotation;
            Coffe_cup_cap_in_machine.GetComponent<Collider>().enabled = false;


            Coffe_cup_in_machine.tag = "ready_Cofffe";




            Coffe_cup_in_machine.GetComponent<Rigidbody>().isKinematic = false;

            _Coffe_cup_in_machine = false;


            Debug.Log("you can start the coffe machine now");

        }
        else
        {
            Debug.Log("чтото не то");
        }


    }


   

    
}
