using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestFingerByFingerDecorator : MonoBehaviour
{
    private int testIndex;
    [SerializeField]
    private Image fingersImage;
    [SerializeField]
    private Sprite[] fingerSprites;
    [SerializeField]
    private HandControl hand;

    private void Awake()
    {
        UpdateFingersVector();

        Core.Instance.currentTest.e_completeProgressionStep += UpdateFingersVector;
    }

    // Update is called once per frame
    void UpdateFingersVector()
    {
        bool[] newVector = new bool[5];
        newVector[0] = true;

        if (testIndex < 4)
        {
            for (int i= 0; i<4; i++) {
                if (i == testIndex)
                {
                    newVector[i+1] = true;
                    fingersImage.sprite = fingerSprites[i];
                }
            }

            testIndex++;
        }

        if (testIndex < 3)
        {
            hand.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("TweezerIndex");
        }
        else
        {
            hand.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("TweezerPinky");
        }

        hand.activeFingers = newVector;
    }
}
