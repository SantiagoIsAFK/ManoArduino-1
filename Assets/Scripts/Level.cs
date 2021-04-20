using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private Grabbable target;

    public Grabbable Target { get => target;}

    public int numLevel;


    private void Awake()
    {
        Core.Instance.currentTest.e_completeProgressionStep += StopLevel;

        StopLevel();
    }


    public void StartLevel() {
        this.gameObject.SetActive(true);


        target.GetComponent<Animator>().SetInteger("numTest", numLevel + 1);
    }

    public void StopLevel()
    {
        this.gameObject.SetActive(false);

        try
        {
            if (Core.Instance.currentTest.levels[Core.Instance.currentTest.ProgressionStep] == this)
            {
                Core.Instance.currentTest.e_StartProgressionStep = StartLevel;
            }
        }
        catch { 
        
        }
    }
}
