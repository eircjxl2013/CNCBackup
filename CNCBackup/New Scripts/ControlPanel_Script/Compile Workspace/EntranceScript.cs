using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntranceScript : MonoBehaviour {
	NCCodeFormat NCCodeFormat_Script;
	List<List<string>> SourceCode = new List<List<string>>();
	string text_field = "O2";
	// Use this for initialization
	void Start () {
		gameObject.AddComponent("NCCodeFormat");
		NCCodeFormat_Script = gameObject.GetComponent<NCCodeFormat>();
	}

	void  Load(string filename)
	{
		if(SourceCode != null)
		{
			SourceCode.Clear();
			SourceCode = null;
		}
		SourceCode = NCCodeFormat_Script.AllCode(filename);
		Debug.Log(SourceCode.Count);
	}
	
	void OnGUI ()
	{
		//To display
		//test_str = GUI.TextArea(new Rect(10, 10,300, 500), test_str);
		
		GUI.Label(new Rect(140, 80, 100, 20), "请输入程序号：");
		text_field = GUI.TextField(new Rect(245, 80, 195, 20), text_field);
		//运行时，可重新输入不同的NC程序名字，反复启动
		if(GUI.Button(new Rect(140, 110, 300, 30), "启动"))
		{
			Load(text_field);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
