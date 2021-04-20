using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salto : MonoBehaviour
{
    public float velocidad;

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+velocidad, this.transform.position.z); 
    }
}
