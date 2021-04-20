using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandControl : MonoBehaviour
{
    public static HandControl instance;

    public UnityAction e_grab;
    public UnityAction e_ungrab;

    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private Arduino arduino;


    private float[] fingersValue; //representa el valor del flexometro actual
    float normalGribValue;
    public bool[] activeFingers;

    [NaughtyAttributes.MinMaxSlider(0, 2024)]
    public Vector2 threshold; //minimo y maximo valor de sensibilidad del flexometro    
    public bool mapWithArduino;
    public float debugValue;

    private bool inGrab, inAction;
    [SerializeField]
    private float actionTimeMax;
    private float timeInAction;

    public float TimeInAction
    {
        get
        {
            return timeInAction;
        }
    }

    public float ActionTimeMax
    {
        get
        {
            return actionTimeMax;
        }
    }

    private void Awake()
    {
        instance = this;

        actionTimeMax = 3-Core.Instance.CurrentUser.Difficulty;
        debugValue = threshold.y;
        fingersValue = new float[5];

    }

    void Update()
    {
        if (Core.Instance.currentTest.InActivity || Core.Instance.currentTest.InRest) {
            UpdateGrabValues();
        }

        if (inAction) {
            timeInAction += Time.deltaTime;
            if (timeInAction > actionTimeMax) {
                timeInAction = actionTimeMax;
            }
        } else{
            timeInAction = 0;
        }
    }

    /// <summary>
    /// Actualiza los valores que corresponden al control de la mano
    /// </summary>
    public void UpdateGrabValues() {
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

            if (Input.GetKey(KeyCode.R))
            {
                fingersValue[1] = debugValue;
            }
            else
            {
                fingersValue[1] = threshold.x;
            }

            if (Input.GetKey(KeyCode.E))
            {
                fingersValue[2] = debugValue;
            }
            else
            {
                fingersValue[2] = threshold.x;
            }

            if (Input.GetKey(KeyCode.W))
            {
                fingersValue[3] = debugValue;
            }
            else
            {
                fingersValue[3] = threshold.x;
            }

            if (Input.GetKey(KeyCode.Q))
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

        CalculateGrabForce();

        if (normalGribValue > 0.1) //si esta abajo de 0.1 de presion en el flexometro entonces se usa una animacion Idle de descanso, sino se mapea la mano
        {
            inAction = true;
            if (normalGribValue > 0.95)
            {
                inGrab = true;

                
                if (Grabbable.currentInstance != null) {
                    if (Grabbable.currentInstance.CanBeGrabbed() && (actionTimeMax > timeInAction)) {
                        e_grab();
                    }
                }
                
            }
            else {
                inGrab = false; 
                try
                {
                    e_ungrab();
                }
                catch
                {

                }
            }
            m_animator.SetBool("inIdle", false);
        }
        else
        {
            inAction = false;
            inGrab = false;
            try
            {
                e_ungrab();
            }
            catch { 
            
            }
            m_animator.SetBool("inIdle", true);
        }


        UpdateAnimator();

    }

    private void CalculateGrabForce() {
        normalGribValue = 0;
        int fingersNum=0;

        for (int i = 0; i < 5; i++)
        {
            if (activeFingers[i]) {
                normalGribValue += GetNormalizedFinger(i);
                fingersNum++;
            }
        }

        normalGribValue = normalGribValue / fingersNum;
    }

    private float  GetNormalizedFinger(int index) {
        return ((fingersValue[index] - threshold.x) / (threshold.y - threshold.x));
    }

    /// <summary>
    /// actualiza la rotacion de la mano con el valor del flexometro y una animacion de agarre
    /// </summary>
    private void UpdateAnimator()
    {
        m_animator.SetFloat("handTime", normalGribValue);
        m_animator.SetFloat("thumbTime", GetNormalizedFinger(0));

        if (activeFingers[1])
        {
            m_animator.SetFloat("indexTime", GetNormalizedFinger(1));
        }
        else
        {
            m_animator.SetFloat("indexTime", 0);
        }

        if (activeFingers[2])
        {
            m_animator.SetFloat("middleTime", GetNormalizedFinger(2));
        }
        else
        {
            m_animator.SetFloat("middleTime", 0);
        }

        if (activeFingers[3])
        {
            m_animator.SetFloat("ringTime", GetNormalizedFinger(3));
        }
        else
        {
            m_animator.SetFloat("ringTime", 0);
        }

        if (activeFingers[4])
        {
            m_animator.SetFloat("pinkyTime", GetNormalizedFinger(4));
        }
        else
        {
            m_animator.SetFloat("pinkyTime", 0);
        }


    }

}
