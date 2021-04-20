using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class BD : MonoBehaviour
{

    /// <summary>
    /// Serializa datos y los convierte en un archivo tipo CSV, compatible con Excel
    /// </summary>
    /// <param name="users"></param>
    public void ExportExcel(List<User> users)
    {

        string ruta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) 
            + "/Terapia "+ DateTime.Now.Day +" "+ DateTime.Now.Month +" "+ DateTime.Now.Year + " "+ DateTime.Now.Millisecond + ".csv";

        //El archivo existe? lo BORRAMOS
        if (File.Exists(ruta))
        {
            File.Delete(ruta);
        }

        //Crear el archivo
        var sr = File.CreateText(ruta);

        string datosCSV = "Nombre,Email,Dificultad,Agarre completo de esfera, Agarre completo de cubo, Agarre de pinzas" + System.Environment.NewLine;

        foreach (User t_user in users) {
            datosCSV += t_user.Name+",";
            datosCSV += t_user.Email + ",";

            
        switch (t_user.Difficulty) {
            case 0:
                    datosCSV += "Facil,";
                break;
            case 1:
                    datosCSV += "Intermedio,";
                break;
            case 2:
                    datosCSV += "Dificil,";
                break;
            default:
                break;
        }


            datosCSV += t_user.HighScores[0] + ",";
            datosCSV += t_user.HighScores[1] + ",";
            datosCSV += t_user.HighScores[2] + "," + System.Environment.NewLine;
        }

        sr.WriteLine(datosCSV);

        //Dejar como sólo de lectura
        FileInfo fInfo = new FileInfo(ruta);
        fInfo.IsReadOnly = true;

        //Cerrar
        sr.Close();

        //Abrimos archivo recien creado
        Application.OpenURL(ruta);
    }
}
