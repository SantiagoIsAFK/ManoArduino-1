using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GUIInGame : MonoBehaviour
{
    [SerializeField]
    private Grabbable target;

    [SerializeField]
    private Sprite toGrab, inGrab; //Sprites para el indicador de la zona de agarre

    [SerializeField]
    private Image progressionBar, actionTimeBar, grabSignifier; //barras e indicador de la zona de agarre respectivamente

    [SerializeField]
    private Text notification, date; //Texto de notificacion y texto de la hora


    [SerializeField]
    private Button start;

    private void Awake()
    {
        Core.Instance.currentTest.e_completeProgressionStep += Grab;
        Core.Instance.currentTest.e_StartProgressionStep += NewGrab;
        Core.Instance.currentTest.e_completeTest += FinishTest;

        

        foreach (Level level in Core.Instance.currentTest.levels) {
            level.Target.e_updateGrab += SetActiveGrabSignifier;
        }
    }

    private void Update()
    {
        date.text = DateTime.Now.ToString("hh:mm:ss");  //Actualiza la hora        
        actionTimeBar.rectTransform.localScale = new Vector3(HandControl.instance.TimeInAction/ HandControl.instance.ActionTimeMax, 1f, 1f); //Actualiza la barra de agarre
    }

    /// <summary>
    /// Inicia el test, es usada por el texto central que aparece al abrir cualquier test
    /// </summary>
    public void StartTest() {
        start.gameObject.SetActive(false);
        Core.Instance.currentTest.StartTest();
    }

    /// <summary>
    /// Actualiza la interfaz cuando se agarra la pelota. Es usada por medio de un evento.
    /// </summary>
    public void Grab() {
        grabSignifier.sprite = inGrab;
        progressionBar.rectTransform.localScale = new Vector3((float)(Core.Instance.currentTest.ProgressionStep)/ (float)Core.Instance.currentTest.levels.Length, 1f,1f);

        notification.text = "La agarraste! Abre tu mano de nuevo y espera unos segundos";
    }

    /// <summary>
    /// Actualiza la interfaz cuando se agarra la pelota. Es usada por medio de un evento.
    /// </summary>
    public void NewGrab()
    {
        grabSignifier.sprite = toGrab;

        notification.text = "Vamos de nuevo, intenta agarrar la pelota";
    }

    /// <summary>
    /// INteractua con el indicador de la zona de agarre, Usada por un evento con relacion directa de la animacion.
    /// </summary>
    /// <param name="value"></param>
    public void SetActiveGrabSignifier(bool value) {
        grabSignifier.gameObject.SetActive(value);
    }

    /// <summary>
    /// Actualiza la interfaz cuando se agarra la pelota. Es usada por medio de un evento.
    /// </summary>
    private void FinishTest(int index, float score)
    {
        notification.text = "Lo conseguiste, tu tiempo fue de "+Mathf.FloorToInt(score)+"s. Ahora puedes salir.";
    }

    /// <summary>
    /// Permite regresar al menu inicial
    /// </summary>
    public void ReturnToMenu()
    {
        Core.Instance.OpenScene("Menu");
    }


}
