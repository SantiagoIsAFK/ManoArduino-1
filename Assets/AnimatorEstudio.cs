using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEstudio : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Saltar();
        }
    }
    
    public void Saltar(){
        
        this.GetComponent<Animator>().SetTrigger("Jump");
    
    }
}
