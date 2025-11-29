using UnityEngine;

public class CoffeCup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         this.GetComponent<Renderer>().material.color= Color.white;
    }

    // Update is called once per frame
    public void Use()
    {
        
            this.GetComponent<Renderer>().material.color = Color.blue;
        

    }
}
