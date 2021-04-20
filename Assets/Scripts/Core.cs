using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Core : MonoBehaviour
{
    #region singleton
    private static Core instance;

    public static Core Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion
    
    public UnityAction savedUser, newUser, cantSaveUser, savedHighscores; //Eventos basicos

    private bool isUserSaved; //permite saber si el primer usuario ya se guardo. No se esta utilizado, sin embargo se conserva porque podria ser util.
    private User currentUser;
    private List<User> users; // lista con todos los usuarios, usada para serializar los datos
    public Test currentTest; 

    [SerializeField]
    private BD baseDeDatos;

    [SerializeField]
    private int numOfTest; //numero de test que estan presentes en total.

    #region properties
    public int NumOfTest
    {
        get
        {
            return numOfTest;
        }
    }

    public User CurrentUser
    {
        get
        {
            return currentUser;
        }

        set
        {
            currentUser = value;
        }
    }
    #endregion

    private void Awake()
    {
        #region instanciamiento del singleton
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion 

        users = new List<User>();
    }

    /// <summary>
    /// Crea un nuevo registro de usuario. Este queda en una variable llamada currentUser y espera aqui hasta que se cree otro usuario o hasta que
    /// se decida exportar un excel con los datos. En el momento que alguna de estas dos ocurre es almacenada en una lista llamada users.
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="email"></param>
    /// <param name="difficulty"></param>
    /// <returns></returns>
    public bool SetNewUser(string userName, string email, int difficulty)
    {
        if (CanSaveUser(userName, email))
        {
            if (currentUser != null) {
                users.Add(currentUser);
            }
            currentUser = new User(userName, email, numOfTest, difficulty);
            isUserSaved = true;
            try
            {
                savedUser();
            }
            catch
            {
                Debug.LogWarning("No existen eventos suscritos");
            }
            return true;
        }
        else {
            try
            {
                cantSaveUser();
            }
            catch
            {
                Debug.LogWarning("No existen eventos suscritos");
            }

            return false;
        }
    }

    /// <summary>
    /// Comprueba si un usuario puede ser ingresado a la base de datos
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    public bool CanSaveUser(string userName, string email)
    {
        if (userName == "" || email == "") {
            return false;
        }
        return true;
    }


    public void OpenScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    /// <summary>
    /// Modifica las variables para finalizar el registro de usuarios. Hace uso del BD para exportar un excel con el registro completo.
    /// </summary>
    /// <returns></returns>
    public bool SaveAllUsers()
    {
        users.Add(currentUser);
        CurrentUser = null;
        isUserSaved = false;
        baseDeDatos.ExportExcel(users);
        return true;
    }
}
