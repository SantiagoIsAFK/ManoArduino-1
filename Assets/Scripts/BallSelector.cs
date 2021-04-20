using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSelector : MonoBehaviour
{
    private int i;
    [SerializeField]
    private GameObject[] balls;

    // permite cambiar entre una lista de pelotas
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            i--;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            i++;

        i =Mathf.Clamp(i, 0, 2);

        UpdateGO();
    }

    private void UpdateGO() { //activa la pelota correcta
        balls[0].SetActive(false);
        balls[1].SetActive(false);
        balls[2].SetActive(false);

        balls[i].SetActive(true);
    }
}
