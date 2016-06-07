using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class SelectVehicle : MonoBehaviour {

    public StreamWriter LoadFile(string FileName)
    {
        StreamWriter file = new StreamWriter(Environment.CurrentDirectory + FileName + ".txt");
        if(file == null)
            File.Create(Environment.CurrentDirectory + FileName + ".txt");
        return file;
    }

    public void WritetoFile(string txt)
    {
        StreamWriter file = LoadFile("CurrentVehicle");
        file.Write(txt);
        file.Close();
    }
}
