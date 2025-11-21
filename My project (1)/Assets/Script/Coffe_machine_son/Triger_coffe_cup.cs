using Player;
using UnityEngine;

public class Triger_coffe_cup : MonoBehaviour
{
    public PickUper _Hold;
    public Transform Coffe_cup_position_in_coffe_machine_position;
    public Transform Coffe_cup_cap_in_coffe_cup_position;

    public GameObject Coffe_cup_in_machine;

    private bool _Coffe_cup_in_machine = false;
    private bool _Coffe_tablet_in_machine = false;

    private void OnTriggerEnter(Collider other)    // when the coffe object enter in the coffe machine
    {
        if (other.gameObject.CompareTag("Coffe_cup") && !_Hold._HoldingObject && !_Coffe_cup_in_machine)
        {
            Coffe_cup_in_machine = other.gameObject;
            Coffe_cup_in_machine.GetComponent<Rigidbody>().isKinematic = true;
            Coffe_cup_in_machine.transform.position = Coffe_cup_position_in_coffe_machine_position.position;
            Coffe_cup_in_machine.transform.rotation = Coffe_cup_position_in_coffe_machine_position.rotation;

            /*other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.transform.position = Coffe_cup_position_in_coffe_machine_position.position;
            other.gameObject.transform.rotation = Coffe_cup_position_in_coffe_machine_position.rotation;   */

            _Coffe_cup_in_machine = true;

        }


        if (_Coffe_cup_in_machine && other.gameObject.CompareTag("Coffe_tablet") && !_Hold._HoldingObject)
        {
            Destroy(other.gameObject);
            _Coffe_tablet_in_machine = true;
        }
        else if (!_Coffe_cup_in_machine)
        {
            Debug.Log("put the coffe cup first");

        }

        if (_Coffe_cup_in_machine && _Coffe_tablet_in_machine && other.CompareTag("Coffe_cup_cap") && !_Hold._HoldingObject)
        {
            Destroy(other.gameObject);

            Coffe_cup_in_machine.tag = "ready_Cofffe";
            Coffe_cup_in_machine.GetComponent<Rigidbody>().isKinematic = false;
            Coffe_cup_in_machine.GetComponent<Renderer>().material.color = Color.yellow;

            _Coffe_cup_in_machine = _Coffe_tablet_in_machine = false;


            Debug.Log("you can start the coffe machine now");

        }


    }


   

    
}
