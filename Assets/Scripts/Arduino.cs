using UnityEngine;
using System.IO.Ports;
using System;

public class Arduino : MonoBehaviour
{
    SerialPort serial;

    public string Port;
    public int Baudios = 9600;
    public int N = 1;
    public int TimeOut = 30;
    
    void Start(){
        serial = new SerialPort(@"\\.\" + Port,Baudios,Parity.None, 8, StopBits.One);
        Data = new float[N];
    }
    void Update(){
        //UpdateData();
    }

    string Read(){
        try {
            string s = serial.ReadLine();
            return s;
        } catch (TimeoutException e) {
            return "";
        }
        
    }

    float[] oldData;
    public float[] Data;
    public void UpdateData() {

        if (!serial.IsOpen) {
            try {
                serial.Open();
                serial.ReadTimeout = TimeOut;
            } catch (InvalidOperationException e) {
                
            }
            
        } else {

            float[] data = new float[N];

            if (oldData == null) {
                oldData = data;
            }

            string[] dataString = Read().Split(' ');

            if (N == dataString.Length) {

                for (int i = 0; i < data.Length; i++) {
                    try {
                        data[i] = float.Parse(dataString[i].Replace(".", ","));
                    } catch (FormatException e) {
                        data[i] = oldData[i];
                    }
                }

                oldData = data;
                Data = data;
            } else {
                Data = oldData;
            }
        }
    }

    void OnApplicationQuit() {
        Close();
    }

    public void Close() {
        if (serial != null) {
            serial.Close();
        }
    }

}
