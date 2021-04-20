using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Tipo de dato Usuario. Contiene una funcion para ser suscrita a evento, ReceiveScore
/// </summary>
public class User
{
    private string name;
    private string email;
    private DateTime date;
    private float[] highScores;
    private int difficulty;

    public string Name
    {
        get
        {
            return name;
        }
    }

    public string Email
    {
        get
        {
            return email;
        }
    }

    public DateTime Date
    {
        get
        {
            return date;
        }
    }

    public float[] HighScores
    {
        get
        {
            return highScores;
        }

        set
        {
            highScores = value;
        }
    }

    public int Difficulty
    {
        get
        {
            return difficulty;
        }
    }

    public User(string name, string email, int numOfTest, int difficulty) {       
        this.name = name;
        this.email = email;
        date = DateTime.Now;
        this.HighScores = new float[3];
        this.difficulty = difficulty;

        
    }

    public void ReceiveScore(int index, float score) {
        highScores[index] = score;
    }
    

}
