using Player;
using UnityEngine;

public class Usage : MonoBehaviour
{

    [SerializeField] private Transform slot;
    [SerializeField] private GameObject _obj;

    public int count_obj = 3;


    void Start()
    {
        this.GetComponent<Renderer>().material.color = Color.white;
    }

    public void Use()
    {
        if (!(count_obj == 0))
        {
            this.GetComponent<Renderer>().material.color = Color.blue;
            Instantiate(_obj, slot.transform.position, Quaternion.identity);




            count_obj--;
        }
        else
        {
            Debug.Log("No_item");

        }
    }


}

