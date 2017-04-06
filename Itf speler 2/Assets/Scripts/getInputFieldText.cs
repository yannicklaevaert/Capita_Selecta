using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;

public class getInputFieldText : MonoBehaviour {

	public InputField Field;
	public Text TextBox;

	public void CopyText(){
		//File.ReadAllLines(@"\\IPADDRESS\directories\file.txt");
		string path = @"///martijn's public files on martijn-2x13/hello.txt";
		Debug.Log(new DirectoryInfo(path).Exists);
		//string path = @"../../test.txt";
		//string path = @"../../test/hello.txt";
		//string path = @"../../martijn's public files/hello.txt";
		string[] test = File.ReadAllLines(path);
		foreach (string s in test){
			Debug.Log(s);
		}
		var  fileName = "MyFile.txt";
		TextBox.text = Field.text;
		if (File.Exists(fileName))
        {
            Debug.Log(fileName+" already exists.");
        }
        var sr = File.AppendText(fileName);
        sr.WriteLine ("This is my file.");
        sr.Close();
	}
}