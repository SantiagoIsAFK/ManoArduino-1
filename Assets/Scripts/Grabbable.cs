using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    public static Grabbable currentInstance; 

    public delegate void UpdateState(bool value);
    public UpdateState e_updateGrab; //informa cuando entra o sale de la zona de agarre

    private bool canGrab; //define si esta en la zona de agarre, es modificada por el animator
    public bool isGrabbed; //define si ya esta agarrada, es modificada por Hand



    private void Start()
    {
        HandControl.instance.e_grab += Grab;
        HandControl.instance.e_ungrab += Ungrab;
    }

    public bool CanBeGrabbed() {
        if (canGrab && !isGrabbed) {
            return true;
        }
        return false;
    }
    /// <summary>
    /// Modifica el Grabbed ademas de actualizar la animacion
    /// </summary>
    /// <param name="value"></param>
    private void Grab() {
        this.GetComponent<Animator>().SetBool("grabbing", false);
        isGrabbed = true;
    }
    private void Ungrab()
    {
        this.GetComponent<Animator>().SetBool("grabbing", true);
        isGrabbed = false;
    }


    /// <summary>
    /// Activa la zona de agarre
    /// </summary>
    public void ActiveSignifier() {
        if (currentInstance == this) {
            canGrab = true;
            e_updateGrab(canGrab);
        }
    }
    /// <summary>
    /// Desactiva la zona de agarre
    /// </summary>
    public void DesactiveSignifier()
    {
        //falta hacer para q se desactive el signifier luego de un progressionstep
        if (currentInstance == this)
        {
            canGrab = false;
            e_updateGrab(canGrab);
        }
    }
    
}
