using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GUIMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject userMenu, testListMenu; //Canvas de los menus

    [SerializeField]
    private Text[] testsScore; //score por cada test

    [SerializeField]
    private Text notification, date;

    //variables para ingresar un nuevo usuario
    [SerializeField]
    private InputField userName, email;
    [SerializeField]
    private Slider difficulty;

    private void Awake()
    {
        if (Core.Instance.CurrentUser == null)
        {
            ShowUserMenu();
            userName.text = "";
            email.text = "";
        }
        else
        {
            UpdateGUI();
            notification.text = "Recuerda guardar el registro al final de la sesión";
        }
    }

    private void Update()
    {
        date.text = DateTime.Now.ToString("hh:mm:ss");
    }

    public void UpdateGUI() {
        for (int i = 0; i < 3; i++)
        {
            testsScore[i].text = Core.Instance.CurrentUser.HighScores[i] + "";
        }
    }

    /// <summary>
    /// Permite al usuario usar un boton para ingresar un nuevo usuario. Este boton usa el Core para ello.
    /// </summary>
    public void CreateNewUser() {
        if (Core.Instance.SetNewUser(userName.text, email.text, (int)difficulty.value))
        {
            notification.text = "Usuario registrado";
            ShowTestListMenu();
        }
        else
        {
            notification.text = "No ha completado los datos";
        }
    }

    /// <summary>
    /// Permite exportar el registro de los usuarios
    /// </summary>
    public void SaveUser() {
        if (Core.Instance.SaveAllUsers())
        {
            notification.text = "Registro guardado";
            ShowUserMenu();
        }
        else
        {
            notification.text = "El registro no pudo ser guardado";
        }
    }
    

    public void OpenNewTest(string scene) {
        Core.Instance.OpenScene(scene);
    }

    /// <summary>
    /// Intercambia de menus/canvas
    /// </summary>
    public void ShowUserMenu() {
        userMenu.SetActive(true);
        testListMenu.SetActive(false);
    }

    /// <summary>
    /// Intercambia de menus/canvas
    /// </summary>
    public void ShowTestListMenu()
    {
        UpdateGUI();
        userMenu.SetActive(false);
        testListMenu.SetActive(true);
    }

}
