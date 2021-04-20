using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMenu : MonoBehaviour
{
    //Machetazo

    public void Return()
    {
        Core.Instance.OpenScene("Menu");
    }
}
