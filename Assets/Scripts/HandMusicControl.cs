using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandMusicControl : MonoBehaviour
{
    public static HandMusicControl instance;

    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private Arduino arduino;


    public float[] fingersValue; //representa el valor del flexometro actual

    [NaughtyAttributes.MinMaxSlider(0, 2024)]
    public Vector2 threshold; //minimo y maximo valor de sensibilidad del flexometro    
    public bool mapWithArduino;
    public float debugValue;



    private void Awake()
    {
        instance = this;

        debugValue = threshold.y;
        fingersValue = new float[5];
    }

    void Update()
    {
        UpdateGrabValues();

    }

    /// <summary>
    /// Actualiza los valores que corresponden al control de la mano
    /// </summary>
    public void UpdateGrabValues()
    {
        if (mapWithArduino) //Mapea con los datos del arduino, si no es asi, mapea con una variable publica para controlarse desde el inspector
        {
            arduino.UpdateData();
            fingersValue[0] = arduino.Data[0];
            fingersValue[1] = arduino.Data[1];
            fingersValue[2] = arduino.Data[2];
            fingersValue[3] = arduino.Data[3];
            fingersValue[4] = arduino.Data[4];
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                fingersValue[0] = debugValue;
            }
            else
            {
                fingersValue[0] = threshold.x;
            }

            if (Input.GetKey(KeyCode.U))
            {
                fingersValue[1] = debugValue;
            }
            else
            {
                fingersValue[1] = threshold.x;
            }

            if (Input.GetKey(KeyCode.I))
            {
                fingersValue[2] = debugValue;
            }
            else
            {
                fingersValue[2] = threshold.x;
            }

            if (Input.GetKey(KeyCode.O))
            {
                fingersValue[3] = debugValue;
            }
            else
            {
                fingersValue[3] = threshold.x;
            }

            if (Input.GetKey(KeyCode.P))
            {
                fingersValue[4] = debugValue;
            }
            else
            {
                fingersValue[4] = threshold.x;
            }
        }

        //clamp para no exceder el limite
        fingersValue[0] = Mathf.Clamp(fingersValue[0], threshold.x, threshold.y);
        fingersValue[1] = Mathf.Clamp(fingersValue[1], threshold.x, threshold.y);
        fingersValue[2] = Mathf.Clamp(fingersValue[2], threshold.x, threshold.y);
        fingersValue[3] = Mathf.Clamp(fingersValue[3], threshold.x, threshold.y);
        fingersValue[4] = Mathf.Clamp(fingersValue[4], threshold.x, threshold.y);



        UpdateAnimator();

    }


    public float GetNormalizedFinger(int index)
    {
        return ((fingersValue[index] - threshold.x) / (threshold.y - threshold.x));
    }

    /// <summary>
    /// actualiza la rotacion de la mano con el valor del flexometro y una animacion de agarre
    /// </summary>
    private void UpdateAnimator()
    {
        m_animator.SetFloat("thumbTime", GetNormalizedFinger(0));
        m_animator.SetFloat("indexTime", GetNormalizedFinger(1));
        m_animator.SetFloat("middleTime", GetNormalizedFinger(2));
        m_animator.SetFloat("ringTime", GetNormalizedFinger(3));
        m_animator.SetFloat("pinkyTime", GetNormalizedFinger(4));



    }
}
