using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Difficulty {
    easy,
    medium,
    hard
}

public enum TestType
{
    Sphere,
    Cube,
    Tweezers
}

public class Test : MonoBehaviour
{
    //eventos basicos
    public UnityAction e_completeProgressionStep, e_StartProgressionStep;
    public delegate void ScoreAction(int numTest, float score);
    public ScoreAction e_completeTest;

    [SerializeField]
    private int indexType;
    public Level[] levels;


    private bool inActivity, inRest; // estados
    private float activityTime; //activity hace referencia en el tiempo que un usuario empieza a usar el flexometro (tiempo de agarre)
    private float restStepTime; //rest hace referencia a un tiempo de descanso entre el momento que se agarra una pelota y el siguiente agarre. Este indica el actual.

    [SerializeField]
    private float restMinTime; //Indica el tiempo de descanso que debe ocurrir para continuar.


    #region properties
    private int progressionStep;

    public bool InActivity
    {
        get
        {
            return inActivity;
        }
    }

    public bool InRest
    {
        get
        {
            return inRest;
        }
    }

    public int ProgressionStep
    {
        get
        {
            return progressionStep;
        }
    }

    public float ActivityTime
    {
        get
        {
            return activityTime;
        }
    }
    #endregion

    private void Awake()
    {
        Core.Instance.currentTest = this;
        e_completeTest += Core.Instance.CurrentUser.ReceiveScore;
        progressionStep = 0;

        Grabbable.currentInstance = levels[progressionStep].Target;
    }

    private void Start()
    {
        HandControl.instance.e_grab += Grab;
    }

    void Update()
    {
        if (inRest) { //Tiempo de descanso
            restStepTime += Time.deltaTime;
            if (restStepTime > restMinTime) {
                e_StartProgressionStep();
                restStepTime = 0;
               inRest = false;
            }
        }
        else if (inActivity) { //Tiempo de actividad (de agarre)
            activityTime += Time.deltaTime;
        }
    }

    public void StartTest() { //Luego de apretar el boton inicial
        inActivity = true;
        inRest = false;

        e_StartProgressionStep();
    }

    /// <summary>
    /// Permite registrar un nuevo agarre. Incluye operaciones para completar un paso en la progresion del test o para finalizar el test completo. Suscrito al estado de la mano.
    /// </summary>
    private void Grab() {
        if (inActivity) {
            progressionStep++;
            
            if (progressionStep == levels.Length) //numero de pasos de progresion.
            {
                CompleteTest();
            }
            else {
                try
                {
                    Grabbable.currentInstance = levels[progressionStep].Target;
                    e_completeProgressionStep();
                }
                catch
                {
                    Debug.LogWarning("No existen eventos suscritos");
                }

                inRest = true;
            }

        }
    }

    /// <summary>
    /// Finaliza el test completamente, ocurre cuando el GrabBall completo todos los pasos de progresion
    /// </summary>
    private void CompleteTest()
    {
        inActivity = false;
        e_completeProgressionStep();
        e_completeTest(indexType, activityTime);

        try
        {
        }
        catch
        {
            Debug.LogWarning("No existen eventos suscritos");
        }

    }
}
