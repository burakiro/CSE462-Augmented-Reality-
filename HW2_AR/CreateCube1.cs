using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System;
using System.IO;
using UnityEditor.Scripting.Python;
using UnityEditor;


public class CreateCube1 : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        PythonRunner.RunFile($"D:/hw2/helloWorld.py");
        int counter=0;
		int n;
  
// Read the file and display it line by line.  
foreach (string line in System.IO.File.ReadLines(@"D:\file1.txt"))

{  
    //Debug.Log(line);
    counter++;
    if(counter == 1){
        n = int.Parse(line);
        Debug.Log("n value is" + n);
    }
    else{
        float x,y,z;
        string [] coordinates = line.Split(' ');
        x = float.Parse(coordinates[0], CultureInfo.InvariantCulture.NumberFormat);
        y = float.Parse(coordinates[1], CultureInfo.InvariantCulture.NumberFormat);
        z = float.Parse(coordinates[2], CultureInfo.InvariantCulture.NumberFormat);

        Debug.Log("x: " + x + "y: " + y + "z: "+ z);

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        sphere.transform.position = new Vector3(x, y, z);
        sphere.GetComponent<Renderer>().material.color = new Color32(0, 47, 255,255);
    }  
}  

    }
		/*
		        string fileName = @"D:\file.txt";
 
        IEnumerable<string> lines = File.ReadLines(fileName);
		Debug.Log((String.Join(Environment.NewLine, lines)));*/
        
      
		
		/*
 GameObject plane  = GameObject.CreatePrimitive(PrimitiveType.Plane);

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 0.5f, 0);

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(0, 1.5f, 0);

        GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        capsule.transform.position = new Vector3(2, 1, 0);

        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = new Vector3(-2, 1, 0);*/
    // Update is called once per frame
    void Update()
    {

    }
}
