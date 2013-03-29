using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text.RegularExpressions;

public class CooSystem : MonoBehaviour {
	
	ControlPanel Main;
	MoveControl MoveControl_script;
	public Vector3 absolute_pos = new Vector3(0,0,0);
	public Vector3 relative_pos = new Vector3(0,0,0);
	public int workpiece_flag = 1;
	
	public Vector3 G00_pos = new Vector3(0,0,0);
	public Vector3 G54_pos = new Vector3(0,0,0);
	public Vector3 G55_pos = new Vector3(0,0,0);
	public Vector3 G56_pos = new Vector3(0,0,0);
	public Vector3 G57_pos = new Vector3(0,0,0);
	public Vector3 G58_pos = new Vector3(0,0,0);
	public Vector3 G59_pos = new Vector3(0,0,0);
	public Vector3 workpiece_coo = new Vector3(0,0,0);
	
	//设定界面修改---陈振华---03.11
	public string parameter_writabel = "1";
	public string TV_check = "0";
	public string hole_code = "1";
	public string input_unit = "0";
	public string IO = "0";
	public string sequence_number = "0";
	public string paper_tape = "0";
	public string SN_stop1 = "0";
	public string SN_stop2 = "0";
	//设定界面修改---陈振华---03.11
	
	void Awake () {
		
	}
	
	// Use this for initialization
	void Start () {
		Main = gameObject.GetComponent<ControlPanel>();
		MoveControl_script = GameObject.Find("move_control").GetComponent<MoveControl>();
		ReadCooFile();
		workpiece_coo = G54_pos;
		workpiece_flag = 1;
		
		//获得设置界面显示值
		if(PlayerPrefs.HasKey("parameter_writabel"))
			parameter_writabel = PlayerPrefs.GetString("parameter_writabel");
		else
		{
			PlayerPrefs.SetString("parameter_writabel", "1");
			parameter_writabel = "1";
		}
		
		if(PlayerPrefs.HasKey("TV_check"))
			TV_check = PlayerPrefs.GetString("TV_check");
		else
		{
			PlayerPrefs.SetString("TV_check", "0");
			TV_check = "0";
		}
		
		if(PlayerPrefs.HasKey("hole_code"))
			hole_code = PlayerPrefs.GetString("hole_code");
		else
		{
			PlayerPrefs.SetString("hole_code", "1");
			hole_code = "1";
		}
		
		if(PlayerPrefs.HasKey("input_unit"))
			input_unit = PlayerPrefs.GetString("input_unit");
		else
		{
			PlayerPrefs.SetString("input_unit", "0");
			input_unit = "0";
		}
		
		if(PlayerPrefs.HasKey("IO"))
			IO = PlayerPrefs.GetString("IO");
		else
		{
			PlayerPrefs.SetString("IO", "0");
			IO = "0";
		}
			
		if(PlayerPrefs.HasKey("sequence_number"))
			sequence_number = PlayerPrefs.GetString("sequence_number");
		else
		{
			PlayerPrefs.SetString("sequence_number", "0");
			sequence_number = "0";
		}
		
		if(PlayerPrefs.HasKey("paper_tape"))
			paper_tape = PlayerPrefs.GetString("paper_tape");
		else
		{
			PlayerPrefs.SetString("paper_tape", "0");
			paper_tape = "0";
		}
		
		if(PlayerPrefs.HasKey("SN_stop1"))
			SN_stop1 = PlayerPrefs.GetString("SN_stop1");
		else
		{
			PlayerPrefs.SetString("SN_stop1", "0");
			SN_stop1 = "0";
		}
		
		if(PlayerPrefs.HasKey("SN_stop2"))
			SN_stop2 = PlayerPrefs.GetString("SN_stop2");
		else
		{
			PlayerPrefs.SetString("SN_stop2", "0");
			SN_stop2 = "0";
		}
		//获得设置界面显示值
	}
	
	//设定界面下移
	public void argu_down()
	{
		switch(Main.argu_setting)
		{
		case 1:
			Main.argu_setting = 2;
			ArguCursorPos();
			break;
		case 2:
			Main.argu_setting = 3;
			ArguCursorPos();
			break;
		case 3:
			Main.argu_setting = 4;
			ArguCursorPos();
			break;
		case 4:
			Main.argu_setting = 5;
			ArguCursorPos();
			break;
		case 5:
			Main.argu_setting = 6;
			ArguCursorPos();
			break;
		case 6:
			Main.argu_setting = 7;
			ArguCursorPos();
			break;
		case 7:
			Main.argu_setting = 8;
			ArguCursorPos();
			break;
		case 8:
			Main.argu_setting = 9;
			ArguCursorPos();
			break;
		}
	}
	
		//设定界面上移
	public void argu_up()
	{
		switch(Main.argu_setting)
		{
		case 9:
			Main.argu_setting = 8;
			ArguCursorPos();
			break;
		case 8:
			Main.argu_setting = 7;
			ArguCursorPos();
			break;
		case 7:
			Main.argu_setting = 6;
			ArguCursorPos();
			break;
		case 6:
			Main.argu_setting = 5;
			ArguCursorPos();
			break;
		case 5:
			Main.argu_setting = 4;
			ArguCursorPos();
			break;
		case 4:
			Main.argu_setting = 3;
			ArguCursorPos();
			break;
		case 3:
			Main.argu_setting = 2;
			ArguCursorPos();
			break;
		case 2:
			Main.argu_setting = 1;
			ArguCursorPos();
			break;
		}
	}
	
	//黄色背景位置
	public void ArguCursorPos()
	{
		switch(Main.argu_setting)
		{
		case 1:
		    Main.argu_setting_cursor_y = 61.5f;
			Main.argu_setting_cursor_w = 16f;
			break;
		case 2:
			Main.argu_setting_cursor_y = 86.5f;
			Main.argu_setting_cursor_w = 16f;
			break;
		case 3:
			Main.argu_setting_cursor_y = 112f;
			Main.argu_setting_cursor_w = 16f;
			break;
		case 4:
			Main.argu_setting_cursor_y = 136.5f;
			Main.argu_setting_cursor_w = 16f;
			break;
		case 5:
			Main.argu_setting_cursor_y = 161.5f;
			Main.argu_setting_cursor_w = 36f;
			break;
		case 6:
			Main.argu_setting_cursor_y = 186.5f;
			Main.argu_setting_cursor_w = 16f;
			break;
		case 7:
			Main.argu_setting_cursor_y = 212f;
			Main.argu_setting_cursor_w = 16f;
			break;
		case 8:
			Main.argu_setting_cursor_y = 236.5f;
			Main.argu_setting_cursor_w = 116f;
			break;
		case 9:
			Main.argu_setting_cursor_y = 261.5f;
			Main.argu_setting_cursor_w = 116f;
			break;
		}
	}
	
	public void set_parameter(string input)
	{
		//Debug.Log(input);
		switch (Main.argu_setting)
		{
		case 1:
			if( input == "0" || input=="1")
			{
				PlayerPrefs.SetString("parameter_writabel", input);
			    parameter_writabel = input;
				//Debug.Log(parameter);
			}
			else
				Debug.Log("请输入0或1");
			break;
		case 2:
		    if( input == "0" || input=="1")
			{
				PlayerPrefs.SetString("TV_check", input);
				TV_check = input;
			}
			else
				Debug.Log("请输入0或1");
			break;
		case 3:
			if( input == "0" || input=="1")
			{
				PlayerPrefs.SetString("hole_code", input);
				hole_code = input;
			}
			else
				Debug.Log("请输入0或1");
			break;
		case 4:
			if( input == "0" || input=="1")
			{
				PlayerPrefs.SetString("input_unit", input);
				input_unit = input;
			}
			else
				Debug.Log("请输入0或1");
			break;
		case 5:
			if(input.Length > 2)
				Debug.Log("请输入0~35");
			else
			{
				if(input.Length == 2)
				{
					Regex num_Reg = new Regex(@"\d{2}");
					if(num_Reg.IsMatch(input))
					{
						int temp_value = Convert.ToInt32(input);
						if(temp_value > 35)
							Debug.Log("请输入0~35");
						else
						{
							PlayerPrefs.SetString("IO", input);
							IO = input;	
						}	
					}
					else
						Debug.Log("请输入0~35");
				}
				else
				{
					
					Regex num_Reg = new Regex(@"\d{1}");
					if(num_Reg.IsMatch(input))
					{
						int temp_value = Convert.ToInt32(input);
						if(temp_value > 35)
							Debug.Log("请输入0~35");
						else
						{
							PlayerPrefs.SetString("IO", input);
							IO = input;	
						}	
					}
					else
						Debug.Log("请输入0~35");
				}
			}
			break;
		case 6:
			if( input == "0" || input=="1")
			{
				PlayerPrefs.SetString("sequence_number", input);
				sequence_number= input;
			}
			else
				Debug.Log("请输入0或1");
			break;
		case 7:
			if( input == "0" || input=="1")
			{
				PlayerPrefs.SetString("paper_tape", input);
				paper_tape = input;
			}
			else
				Debug.Log("请输入0或1");
			break;
		case 8:
			PlayerPrefs.SetString("SN_stop1", input);
			SN_stop1 = input;
			break;
		case 9:
			PlayerPrefs.SetString("SN_stop2", input);
			SN_stop2 = input;
			break;
		default:
			Debug.Log("out of range");
			break;
		}	
	}
	
	//参数界面内容
	public void ReadCooFile () 
	{
		string line_str = "";
		string[] coo_str;
		StreamReader line_str_reader;
		FileStream coo_stream = new FileStream(Application.dataPath+"/Resources/Coordinate/G00.txt", FileMode.OpenOrCreate, FileAccess.Read);
		line_str_reader = new StreamReader(coo_stream);
		line_str = line_str_reader.ReadLine();
		if(line_str == null)
		{
			G00_pos.x = 0f;
			G00_pos.y = 0f;
			G00_pos.z = 0f;
		}
		else
		{
			coo_str = line_str.Split(',');
			G00_pos.x = float.Parse(coo_str[0]);
			G00_pos.y = float.Parse(coo_str[1]);
			G00_pos.z = float.Parse(coo_str[2]);
		}
		line_str_reader.Close();
		coo_stream = new FileStream(Application.dataPath+"/Resources/Coordinate/G54.txt", FileMode.OpenOrCreate, FileAccess.Read);
		line_str_reader = new StreamReader(coo_stream);
		line_str = line_str_reader.ReadLine();
		if(line_str == null)
		{
			G54_pos.x = 0f;
			G54_pos.y = 0f;
			G54_pos.z = 0f;
		}
		else
		{
			coo_str = line_str.Split(',');
			G54_pos.x = float.Parse(coo_str[0]);
			G54_pos.y = float.Parse(coo_str[1]);
			G54_pos.z = float.Parse(coo_str[2]);
		}
		line_str_reader.Close();
		coo_stream = new FileStream(Application.dataPath+"/Resources/Coordinate/G55.txt", FileMode.OpenOrCreate, FileAccess.Read);
		line_str_reader = new StreamReader(coo_stream);
		line_str = line_str_reader.ReadLine();
		if(line_str == null)
		{
			G55_pos.x = 0f;
			G55_pos.y = 0f;
			G55_pos.z = 0f;
		}
		else
		{
			coo_str = line_str.Split(',');
			G55_pos.x = float.Parse(coo_str[0]);
			G55_pos.y = float.Parse(coo_str[1]);
			G55_pos.z = float.Parse(coo_str[2]);
		}
		line_str_reader.Close();
		coo_stream = new FileStream(Application.dataPath+"/Resources/Coordinate/G56.txt", FileMode.OpenOrCreate, FileAccess.Read);
		line_str_reader = new StreamReader(coo_stream);
		line_str = line_str_reader.ReadLine();
		if(line_str == null)
		{
			G56_pos.x = 0f;
			G56_pos.y = 0f;
			G56_pos.z = 0f;
		}
		else
		{
			coo_str = line_str.Split(',');
			G56_pos.x = float.Parse(coo_str[0]);
			G56_pos.y = float.Parse(coo_str[1]);
			G56_pos.z = float.Parse(coo_str[2]);
		}
		line_str_reader.Close();
		coo_stream = new FileStream(Application.dataPath+"/Resources/Coordinate/G57.txt", FileMode.OpenOrCreate, FileAccess.Read);
		line_str_reader = new StreamReader(coo_stream);
		line_str = line_str_reader.ReadLine();
		if(line_str == null)
		{
			G57_pos.x = 0f;
			G57_pos.y = 0f;
			G57_pos.z = 0f;
		}
		else
		{
			coo_str = line_str.Split(',');
			G57_pos.x = float.Parse(coo_str[0]);
			G57_pos.y = float.Parse(coo_str[1]);
			G57_pos.z = float.Parse(coo_str[2]);
		}
		line_str_reader.Close();
		coo_stream = new FileStream(Application.dataPath+"/Resources/Coordinate/G58.txt", FileMode.OpenOrCreate, FileAccess.Read);
		line_str_reader = new StreamReader(coo_stream);
		line_str = line_str_reader.ReadLine();
		if(line_str == null)
		{
			G58_pos.x = 0f;
			G58_pos.y = 0f;
			G58_pos.z = 0f;
		}
		else
		{
			coo_str = line_str.Split(',');
			G58_pos.x = float.Parse(coo_str[0]);
			G58_pos.y = float.Parse(coo_str[1]);
			G58_pos.z = float.Parse(coo_str[2]);
		}
		line_str_reader.Close();
		coo_stream = new FileStream(Application.dataPath+"/Resources/Coordinate/G59.txt", FileMode.OpenOrCreate, FileAccess.Read);
		line_str_reader = new StreamReader(coo_stream);
		line_str = line_str_reader.ReadLine();
		if(line_str == null)
		{
			G59_pos.x = 0f;
			G59_pos.y = 0f;
			G59_pos.z = 0f;
		}
		else
		{
			coo_str = line_str.Split(',');
			G59_pos.x = float.Parse(coo_str[0]);
			G59_pos.y = float.Parse(coo_str[1]);
			G59_pos.z = float.Parse(coo_str[2]);
		}
		line_str_reader.Close();
	}
	
	public void WriteCooChoose (int coo_select , string write_str) 
	{
		switch (coo_select)
		{
		case 1:
			WriteCooFile("G00", write_str);
			break;
		case 2:
			WriteCooFile("G54", write_str);
			break;
		case 3:
			WriteCooFile("G55", write_str);
			break;
		case 4:
			WriteCooFile("G56", write_str);
			break;
		case 5:
			WriteCooFile("G57", write_str);
			break;
		case 6:
			WriteCooFile("G58", write_str);
			break;
		case 7:
			WriteCooFile("G59", write_str);
			break;
		default:
			Debug.Log("out of range");
			break;
		}
	}
	
	void WriteCooFile (string filename, string write_str)
	{
		StreamWriter line_str_writer;
		FileStream coo_stream;
		FileInfo check_exist = new FileInfo(Application.dataPath+"/Resources/Coordinate/"+filename+".txt");
		if(check_exist.Exists)
			coo_stream = new FileStream(Application.dataPath+"/Resources/Coordinate/"+filename+".txt", FileMode.Truncate, FileAccess.Write);
		else
			coo_stream = new FileStream(Application.dataPath+"/Resources/Coordinate/"+filename+".txt", FileMode.Create, FileAccess.Write);
		line_str_writer = new StreamWriter(coo_stream);
		line_str_writer.WriteLine(write_str);
		line_str_writer.Close();
	}
	

	public void Down () 
	{
		switch (Main.coo_setting_1)
		{
		case 1:
			if(Main.coo_setting_2 == 1)
				Main.coo_setting_2 = 2;
			else if(Main.coo_setting_2 == 2)
				Main.coo_setting_2 = 3;
			else
			{
				Main.coo_setting_2 = 1;
				Main.coo_setting_1 = 2;
			}
			CooCursorPos();
			break;
		case 2:
			if(Main.coo_setting_2 == 1)
				Main.coo_setting_2 = 2;
			else if(Main.coo_setting_2 == 2)
				Main.coo_setting_2 = 3;
			else
			{
				Main.coo_setting_2 = 1;
				Main.coo_setting_1 = 3;
			}
			CooCursorPos();
			break;
		case 3:
			if(Main.coo_setting_2 == 1)
				Main.coo_setting_2 = 2;
			else if(Main.coo_setting_2 == 2)
				Main.coo_setting_2 = 3;
			else
			{
				Main.coo_setting_2 = 1;
				Main.coo_setting_1 = 4;
			}
			CooCursorPos();
			break;
		case 4:
			if(Main.coo_setting_2 == 1)
				Main.coo_setting_2 = 2;
			else if(Main.coo_setting_2 == 2)
				Main.coo_setting_2 = 3;
			else
			{
				Main.coo_setting_2 = 1;
				Main.coo_setting_1 = 5;
				Main.OffCooFirstPage = false;
			}
			CooCursorPos();
			break;
		case 5:
			if(Main.coo_setting_2 == 1)
				Main.coo_setting_2 = 2;
			else if(Main.coo_setting_2 == 2)
				Main.coo_setting_2 = 3;
			else
			{
				Main.coo_setting_2 = 1;
				Main.coo_setting_1 = 6;
			}
			CooCursorPos();
			break;
		case 6:
			if(Main.coo_setting_2 == 1)
				Main.coo_setting_2 = 2;
			else if(Main.coo_setting_2 == 2)
				Main.coo_setting_2 = 3;
			else
			{
				Main.coo_setting_2 = 1;
				Main.coo_setting_1 = 7;
			}
			CooCursorPos();
			break;
		case 7:
			if(Main.coo_setting_2 == 1)
				Main.coo_setting_2 = 2;
			else if(Main.coo_setting_2 == 2)
				Main.coo_setting_2 = 3;
			CooCursorPos();
			break;
		}
	}
	
	public void Up () 
	{
		switch (Main.coo_setting_1)
		{
		case 1:
			if(Main.coo_setting_2 == 2)
				Main.coo_setting_2 = 1;
			else if(Main.coo_setting_2 == 3)
				Main.coo_setting_2 = 2;
			CooCursorPos();
			break;
		case 2:
			if(Main.coo_setting_2 == 2)
				Main.coo_setting_2 = 1;
			else if(Main.coo_setting_2 == 3)
				Main.coo_setting_2 = 2;
			else
			{
				Main.coo_setting_2 = 3;
				Main.coo_setting_1 = 1;
			}
			CooCursorPos();
			break;
		case 3:
			if(Main.coo_setting_2 == 2)
				Main.coo_setting_2 = 1;
			else if(Main.coo_setting_2 == 3)
				Main.coo_setting_2 = 2;
			else
			{
				Main.coo_setting_2 = 3;
				Main.coo_setting_1 = 2;
			}
			CooCursorPos();
			break;
		case 4:
			if(Main.coo_setting_2 == 2)
				Main.coo_setting_2 = 1;
			else if(Main.coo_setting_2 == 3)
				Main.coo_setting_2 = 2;
			else
			{
				Main.coo_setting_2 = 3;
				Main.coo_setting_1 = 3;
			}
			CooCursorPos();
			break;
		case 5:
			if(Main.coo_setting_2 == 2)
				Main.coo_setting_2 = 1;
			else if(Main.coo_setting_2 == 3)
				Main.coo_setting_2 = 2;
			else
			{
				Main.coo_setting_2 = 3;
				Main.coo_setting_1 = 4;
				Main.OffCooFirstPage = true;
			}
			CooCursorPos();
			break;
		case 6:
			if(Main.coo_setting_2 == 2)
				Main.coo_setting_2 = 1;
			else if(Main.coo_setting_2 == 3)
				Main.coo_setting_2 = 2;
			else
			{
				Main.coo_setting_2 = 3;
				Main.coo_setting_1 = 5;
			}
			CooCursorPos();
			break;
		case 7:
			if(Main.coo_setting_2 == 2)
				Main.coo_setting_2 = 1;
			else if(Main.coo_setting_2 == 3)
				Main.coo_setting_2 = 2;
			else
			{
				Main.coo_setting_2 = 3;
				Main.coo_setting_1 = 6;
			}
			CooCursorPos();
			break;
		}
	}
	
	public void Left () 
	{
		switch(Main.coo_setting_1)
		{
		case 3:
			Main.coo_setting_1 = 1;
			CooCursorPos();
			break;
		case 4:
			Main.coo_setting_1 = 2;
			CooCursorPos();
			break;
		case 7:
			Main.coo_setting_1 = 5;
			CooCursorPos();
			break;
		}
	}
	
	public void Right () 
	{
		switch(Main.coo_setting_1)
		{
		case 1:
			Main.coo_setting_1 = 3;
			CooCursorPos();
			break;
		case 2:
			Main.coo_setting_1 = 4;
			CooCursorPos();
			break;
		case 5:
			Main.coo_setting_1 = 7;
			CooCursorPos();
			break;
		}
	}
	
	public void PageUp ()
	{
		switch (Main.coo_setting_1)
		{
		case 5:
			Main.coo_setting_1 = 1;
			break;
		case 6:
			Main.coo_setting_1 = 2;
			break;
		case 7:
			Main.coo_setting_1 = 3;
			break;
		}
	}
	
	public void PageDown ()
	{
		switch (Main.coo_setting_1)
		{
		case 1:
			Main.coo_setting_1 = 5;
			break;
		case 2:
			Main.coo_setting_1 = 6;
			break;
		case 3:
			Main.coo_setting_1 = 7;
			break;
		case 4:
			Main.coo_setting_1 = 7;
			Main.coo_setting_2 = 3;
			CooCursorPos();
			break;
		}
	}
	
	public void CooCursorPos () 
	{
		switch(Main.coo_setting_1)
		{
		case 1:
		case 5:
			Main.coo_setting_cursor_x = 131f;
			if(Main.coo_setting_2 == 1)
				Main.coo_setting_cursor_y = 120f;
			else if(Main.coo_setting_2 == 2)
				Main.coo_setting_cursor_y = 150f;
			else
				Main.coo_setting_cursor_y = 180f;
			break;
		case 2:
		case 6:
			Main.coo_setting_cursor_x = 131f;
			if(Main.coo_setting_2 == 1)
				Main.coo_setting_cursor_y = 240f;
			else if(Main.coo_setting_2 == 2)
				Main.coo_setting_cursor_y = 270f;
			else
				Main.coo_setting_cursor_y = 300f;
			break;
		case 3:
		case 7:
			Main.coo_setting_cursor_x = 376f;
			if(Main.coo_setting_2 == 1)
				Main.coo_setting_cursor_y = 120f;
			else if(Main.coo_setting_2 == 2)
				Main.coo_setting_cursor_y = 150f;
			else
				Main.coo_setting_cursor_y = 180f;
			break;
		case 4:
			Main.coo_setting_cursor_x = 376f;
			if(Main.coo_setting_2 == 1)
				Main.coo_setting_cursor_y = 240f;
			else if(Main.coo_setting_2 == 2)
				Main.coo_setting_cursor_y = 270f;
			else
				Main.coo_setting_cursor_y = 300f;
			break;
		}
	}
	
	public void SearchNo (string num_str) 
	{
		string str_temp = num_str.TrimStart('0', ' ');
		switch (str_temp)
		{
		case "":
			Main.coo_setting_1 = 1;
			Main.coo_setting_2 = 1;
			CooCursorPos();
			Main.OffCooFirstPage = true;
			break;
		case "1":
			Main.coo_setting_1 = 2;
			Main.coo_setting_2 = 1;
			CooCursorPos();
			Main.OffCooFirstPage = true;
			break;
		case "2":
			Main.coo_setting_1 = 3;
			Main.coo_setting_2 = 1;
			CooCursorPos();
			Main.OffCooFirstPage = true;
			break;
		case "3":
			Main.coo_setting_1 = 4;
			Main.coo_setting_2 = 1;
			CooCursorPos();
			Main.OffCooFirstPage = true;
			break;
		case "4":
			Main.coo_setting_1 = 5;
			Main.coo_setting_2 = 1;
			CooCursorPos();
			Main.OffCooFirstPage = false;
			break;
		case "5":
			Main.coo_setting_1 = 6;
			Main.coo_setting_2 = 1;
			CooCursorPos();
			Main.OffCooFirstPage = false;
			break;
		case "6":
			Main.coo_setting_1 = 7;
			Main.coo_setting_2 = 1;
			CooCursorPos();
			Main.OffCooFirstPage = false;
			break;
		default:
			break;
		}
	}
	
	public void Measure (string coo_value)
	{
		char[] coo_choose = coo_value.ToCharArray();
		string value_str = "";
		float value_f = 0;
		if(coo_choose.Length < 2 || (coo_choose[0] != 'X' && coo_choose[0] != 'Y'  && coo_choose[0] != 'Z' && coo_choose[0] != 'x' && coo_choose[0] != 'y' && coo_choose[0] != 'z' ))
		{
			Debug.Log("Format Error!!!");
			return;
		}
		else
		{
			for(int i = 1; i < coo_choose.Length; i++)
			{
				if(coo_choose[i] != '.'  && coo_choose[i] != '+' && coo_choose[i] != '-')
				{
					if(coo_choose[i] < '0' || coo_choose[i] > '9')
					{
						Debug.Log("Format Error!!!");
						return;
					}
				}
				else if(coo_choose[i] == '+' || coo_choose[i] == '-')
				{
					if(i != 1)
					{
						Debug.Log("Format Error!!!");
						return;
					}
				}
				value_str += coo_choose[i];
			}
			if(value_str == "+" || value_str == "-")
			{
				Debug.Log("Format Error!!!");
				return;
			}
			value_f = float.Parse(value_str);
			//Debug.Log(value_f);
			switch(coo_choose[0])
			{
			case 'X':
			case 'x':
				Measure_choose(1, value_f, 1);
				Main.coo_setting_2 = 1;
				break;
			case 'Y':
			case 'y':
				Measure_choose(2, value_f, 1);
				Main.coo_setting_2 = 2;
				break;
			case 'Z':
			case 'z':
				Measure_choose(3, value_f, 1);
				Main.coo_setting_2 = 3;
				break;
			default:
				Debug.Log("Format Error!!!");
				break;
			}
			CooCursorPos();
		}		
	}
	
	public void PlusInput (string input_value, bool plus_flag) 
	{
		char[] coo_choose = input_value.ToCharArray();
		string value_str = "";
		float value_f = 0;
		for(int i = 0; i < coo_choose.Length; i++)
		{
			if(coo_choose[i] != '.'  && coo_choose[i] != '+' && coo_choose[i] != '-')
			{
				if(coo_choose[i] < '0' || coo_choose[i] > '9')
				{
					Debug.Log("Format Error!!!");
					return;
				}
			}
			else if(coo_choose[i] == '+' || coo_choose[i] == '-')
			{
				if(i != 0)
				{
					Debug.Log("Format Error!!!");
					return;
				}
			}
			value_str += coo_choose[i];
		}
		if(value_str == "+" || value_str == "-")
		{
			Debug.Log("Format Error!!!");
			return;
		}
		value_f = float.Parse(value_str);
		if(plus_flag)
			Measure_choose(Main.coo_setting_2, value_f, 2);
		else
			Measure_choose(Main.coo_setting_2, value_f, 3);
	}
	
	void Measure_choose (int xyz_select, float value_f, int mode_flag) 
	{
		string write_str = "";
		switch (Main.coo_setting_1)
		{
		case 1:
			if(xyz_select == 1)
			{
				if(mode_flag == 1)
					G00_pos.x = MoveControl_script.MachineCoo.x - value_f;
				else if(mode_flag == 2)
					G00_pos.x += value_f; 
				else
					G00_pos.x = value_f; 
				write_str = G00_pos.x+","+G00_pos.y+","+G00_pos.z;
				WriteCooChoose(1,write_str);
			}
			else if(xyz_select == 2)
			{
				if(mode_flag == 1)
					G00_pos.y = MoveControl_script.MachineCoo.y - value_f;
				else if(mode_flag == 2)
					G00_pos.y += value_f; 
				else
					G00_pos.y = value_f; 
				write_str = G00_pos.x+","+G00_pos.y+","+G00_pos.z;
				WriteCooChoose(1,write_str);
			}
			else
			{
				if(mode_flag == 1)
					G00_pos.z = MoveControl_script.MachineCoo.z - value_f;
				else if(mode_flag == 2)
					G00_pos.z += value_f; 
				else
					G00_pos.z = value_f; 
				write_str = G00_pos.x+","+G00_pos.y+","+G00_pos.z;
				WriteCooChoose(1,write_str);
			}
			Main.OffCooFirstPage = true;
			Workpiece_Change();
			break;
		case 2:
			if(xyz_select == 1)
			{
				if(mode_flag == 1)
					G54_pos.x = MoveControl_script.MachineCoo.x - value_f;
				else if(mode_flag == 2)
					G54_pos.x += value_f; 
				else
					G54_pos.x = value_f; 
				write_str = G54_pos.x+","+G54_pos.y+","+G54_pos.z;
				WriteCooChoose(2,write_str);
			}
			else if(xyz_select == 2)
			{
				if(mode_flag == 1)
					G54_pos.y = MoveControl_script.MachineCoo.y - value_f;
				else if(mode_flag == 2)
					G54_pos.y += value_f; 
				else
					G54_pos.y = value_f; 
				write_str = G54_pos.x+","+G54_pos.y+","+G54_pos.z;
				WriteCooChoose(2,write_str);
			}
			else
			{
				if(mode_flag == 1)
					G54_pos.z = MoveControl_script.MachineCoo.z - value_f;
				else if(mode_flag == 2)
					G54_pos.z += value_f; 
				else
					G54_pos.z = value_f; 
				write_str = G54_pos.x+","+G54_pos.y+","+G54_pos.z;
				WriteCooChoose(2,write_str);
			}
			Main.OffCooFirstPage = true;
			Workpiece_Change();
			break;
		case 3:
			if(xyz_select == 1)
			{
				if(mode_flag == 1)
					G55_pos.x = MoveControl_script.MachineCoo.x - value_f;
				else if(mode_flag == 2)
					G55_pos.x += value_f; 
				else
					G55_pos.x = value_f; 
				write_str = G55_pos.x+","+G55_pos.y+","+G55_pos.z;
				WriteCooChoose(3,write_str);
			}
			else if(xyz_select == 2)
			{
				if(mode_flag == 1)
					G55_pos.y = MoveControl_script.MachineCoo.y - value_f;
				else if(mode_flag == 2)
					G55_pos.y += value_f; 
				else
					G55_pos.y = value_f; 
				write_str = G55_pos.x+","+G55_pos.y+","+G55_pos.z;
				WriteCooChoose(3,write_str);
			}
			else
			{
				if(mode_flag == 1)
					G55_pos.z = MoveControl_script.MachineCoo.z - value_f;
				else if(mode_flag == 2)
					G55_pos.z += value_f; 
				else
					G55_pos.z = value_f; 
				write_str = G55_pos.x+","+G55_pos.y+","+G55_pos.z;
				WriteCooChoose(3,write_str);
			}
			Main.OffCooFirstPage = true;
			Workpiece_Change();
			break;
		case 4:
			if(xyz_select == 1)
			{
				if(mode_flag == 1)
					G56_pos.x = MoveControl_script.MachineCoo.x - value_f;
				else if(mode_flag == 2)
					G56_pos.x += value_f; 
				else
					G56_pos.x = value_f; 
				write_str = G56_pos.x+","+G56_pos.y+","+G56_pos.z;
				WriteCooChoose(4,write_str);
			}
			else if(xyz_select == 2)
			{
				if(mode_flag == 1)
					G56_pos.y = MoveControl_script.MachineCoo.y - value_f;
				else if(mode_flag == 2)
					G56_pos.y += value_f; 
				else
					G56_pos.y = value_f;
				write_str = G56_pos.x+","+G56_pos.y+","+G56_pos.z;
				WriteCooChoose(4,write_str);
			}
			else
			{
				if(mode_flag == 1)
					G56_pos.z = MoveControl_script.MachineCoo.z - value_f;
				else if(mode_flag == 2)
					G56_pos.z += value_f; 
				else
					G56_pos.z = value_f;
				write_str = G56_pos.x+","+G56_pos.y+","+G56_pos.z;
				WriteCooChoose(4,write_str);
			}
			Main.OffCooFirstPage = true;
			Workpiece_Change();
			break;
		case 5:
			if(xyz_select == 1)
			{
				if(mode_flag == 1)
					G57_pos.x = MoveControl_script.MachineCoo.x - value_f;
				else if(mode_flag == 2)
					G57_pos.x += value_f; 
				else
					G57_pos.x = value_f;
				write_str = G57_pos.x+","+G57_pos.y+","+G57_pos.z;
				WriteCooChoose(5,write_str);
			}
			else if(xyz_select == 2)
			{
				if(mode_flag == 1)
					G57_pos.y = MoveControl_script.MachineCoo.y - value_f;
				else if(mode_flag == 2)
					G57_pos.y += value_f; 
				else
					G57_pos.y = value_f;
				write_str = G57_pos.x+","+G57_pos.y+","+G57_pos.z;
				WriteCooChoose(5,write_str);
			}
			else
			{
				if(mode_flag == 1)
					G57_pos.z = MoveControl_script.MachineCoo.z - value_f;
				else if(mode_flag == 2)
					G57_pos.z += value_f; 
				else
					G57_pos.z = value_f;
				write_str = G57_pos.x+","+G57_pos.y+","+G57_pos.z;
				WriteCooChoose(5,write_str);
			}
			Main.OffCooFirstPage = false;
			Workpiece_Change();
			break;
		case 6:
			if(xyz_select == 1)
			{
				if(mode_flag == 1)
					G58_pos.x = MoveControl_script.MachineCoo.x - value_f;
				else if(mode_flag == 2)
					G58_pos.x += value_f; 
				else
					G58_pos.x = value_f;
				write_str = G58_pos.x+","+G58_pos.y+","+G58_pos.z;
				WriteCooChoose(6,write_str);
			}
			else if(xyz_select == 2)
			{
				if(mode_flag == 1)
					G58_pos.y = MoveControl_script.MachineCoo.y - value_f;
				else if(mode_flag == 2)
					G58_pos.y += value_f; 
				else
					G58_pos.y = value_f;
				write_str = G58_pos.x+","+G58_pos.y+","+G58_pos.z;
				WriteCooChoose(6,write_str);
			}
			else
			{
				if(mode_flag == 1)
					G58_pos.z = MoveControl_script.MachineCoo.z - value_f;
				else if(mode_flag == 2)
					G58_pos.z += value_f; 
				else
					G58_pos.z = value_f;
				write_str = G58_pos.x+","+G58_pos.y+","+G58_pos.z;
				WriteCooChoose(6,write_str);
			}
			Main.OffCooFirstPage = false;
			Workpiece_Change();
			break;
		case 7:
			if(xyz_select == 1)
			{
				if(mode_flag == 1)
					G59_pos.x = MoveControl_script.MachineCoo.x - value_f;
				else if(mode_flag == 2)
					G59_pos.x += value_f; 
				else
					G59_pos.x = value_f;
				write_str = G59_pos.x+","+G59_pos.y+","+G59_pos.z;
				WriteCooChoose(7,write_str);
			}
			else if(xyz_select == 2)
			{
				if(mode_flag == 1)
					G59_pos.y = MoveControl_script.MachineCoo.y - value_f;
				else if(mode_flag == 2)
					G59_pos.y += value_f; 
				else
					G59_pos.y = value_f;
				write_str = G59_pos.x+","+G59_pos.y+","+G59_pos.z;
				WriteCooChoose(7,write_str);
			}
			else
			{
				if(mode_flag == 1)
					G59_pos.z = MoveControl_script.MachineCoo.z - value_f;
				else if(mode_flag == 2)
					G59_pos.z += value_f; 
				else
					G59_pos.z = value_f;
				write_str = G59_pos.x+","+G59_pos.y+","+G59_pos.z;
				WriteCooChoose(7,write_str);
			}
			Main.OffCooFirstPage = false;
			Workpiece_Change();
			break;
		default:
			break;
		}
	}
	
	
	
	// Update is called once per frame
	void Update () {
		absolute_pos = MoveControl_script.MachineCoo - G00_pos - workpiece_coo;
		//Debug.Log(workpiece_coo.x +","+workpiece_coo.y+","+workpiece_coo.z);
		relative_pos = MoveControl_script.MachineCoo - G00_pos - workpiece_coo;
	}
	
	
	public void Workpiece_Change () {
		switch (workpiece_flag)
		{
		case 1:
			workpiece_coo = G54_pos;
			break;
		case 2:
			workpiece_coo = G55_pos;
			Debug.Log("2222222222");
			break;
		case 3:
			workpiece_coo = G56_pos;
			break;
		case 4:
			workpiece_coo = G57_pos;
			break;
		case 5:
			workpiece_coo = G58_pos;
			break;
		case 6:
			workpiece_coo = G59_pos;
			break;
		default:
			Debug.Log("workpiece flag is wrong!!!");
			break;
		}
	}
}
