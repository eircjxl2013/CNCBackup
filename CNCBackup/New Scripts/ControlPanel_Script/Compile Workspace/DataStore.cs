using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 编译数据存储结构
/// </summary>
public class DataStore
{
	public int slash_value;
	public List<string> immediate_execution;
	public string motion_string;
	public float x_value;
	public float y_value;
	public float z_value;
	public float s_value;
	public float f_value;
	public float i_value;
	public float j_value;
	public float k_value;
	public float r_value;
	public int t_string;
	public float x_remaining_movement;
	public float y_remaining_movement;
	public float z_remaining_movement;
	
	public List<string> G_code;
	public List<int> modal_index;
	public List<string> modal_string;
	
	public DataStore()
	{
		slash_value = 0;
		immediate_execution = new List<string>();
		motion_string = "";
		x_value = 0;
		y_value = 0;
		z_value = 0;
		s_value = 0;
		f_value = 0;
		i_value = 0;
		j_value = 0;
		k_value = 0;
		r_value = 0;
		t_string = 0;
		x_remaining_movement = 0;
		y_remaining_movement = 0;
		z_remaining_movement = 0;
		G_code = new List<string>();
		modal_index = new List<int>();
		modal_string = new List<string>();
	}
	
	public void Clear()
	{
		slash_value = 0;
		immediate_execution.Clear();
		motion_string = "";
		x_value = 0;
		y_value = 0;
		z_value = 0;
		s_value = 0;
		f_value = 0;
		i_value = 0;
		j_value = 0;
		k_value = 0;
		r_value = 0;
		t_string = 0;
		x_remaining_movement = 0;
		y_remaining_movement = 0;
		z_remaining_movement = 0;
		G_code.Clear();
		modal_index.Clear();
		modal_string.Clear();
	}
	
	public override string ToString ()
	{
		string G_str = "";
		if(G_code.Count > 0)
		{
			for(int i = 0; i < G_code.Count; i++)
				G_str += G_code[i] + "; ";
		}
		else
			G_str = "null";
		string Modal_str = "";
		if(modal_string.Count > 0)
		{
			for(int i = 0; i < modal_string.Count; i++)
				Modal_str += modal_string[i] + "; ";
		}
		else
			Modal_str = "null";
		string Immediate_str = "";
		if(immediate_execution.Count > 0)
		{
			for(int i = 0; i < immediate_execution.Count; i++)
				Immediate_str += immediate_execution[i] + "; ";
		}
		else
			Immediate_str = "null";
		return "Slash: " + slash_value + "; Immediate execution: " + Immediate_str + "; Motion: " + motion_string + "; X: " + x_value.ToString() +
			"; Y: " + y_value.ToString() + "; Z: " + z_value.ToString() + "; S: " + s_value.ToString() + "; F: " + f_value.ToString() + "; I: " + 
				i_value.ToString() + "; J: " + j_value.ToString() + "; K: " + k_value.ToString() + "; R: " + r_value.ToString() + "; T: " + t_string.ToString() + 
				"; X_remain: " + x_remaining_movement.ToString() + "; Y_remain: " + y_remaining_movement.ToString() + "; Z_remain: " + 
				z_remaining_movement.ToString() + "; Modal_str: " + Modal_str + "; G_str: " + G_str;
	}
}

/*
public interface IModal
{
	string[] Modal_Code{get;}
	int ModalIndex(string aim_code);
	bool SetModalCode(string aim_code, int index);
}
 */
/// <summary>
/// 模态代码数据结构，待修改，改进扩展机制
/// </summary>
public class ModalCode_Fanuc_M
{
	private string[] _modal_code;
	public string[] Modal_Code
	{
		get{return _modal_code;}
	}
	private Dictionary<string, int> code_location;
	/// <summary>
	/// 普通初始化 <see cref="ModalCode_Fanuc_M"/> class.
	/// </summary>
	public ModalCode_Fanuc_M()
	{
		_modal_code = new string[]{"G00", "G94", "G80", "G17", "G21", "G98", "G90", "G40", "G50", "G22", 
			"G49", "G67", "G97", "G54", "G64", "G69", "G15", "G40.1", "G25", "G160", "G13.1", "G50.1", 
			"G54.2", "G80.5"};	
		code_location = new Dictionary<string, int>();
		LocationInitialize();
	}
	/// <summary>
	/// 实现复制的初始化 <see cref="ModalCode_Fanuc_M"/> class.
	/// </summary>
	/// <param name='aim_class'>
	/// 复制的目标对象
	/// </param>
	public ModalCode_Fanuc_M (ModalCode_Fanuc_M aim_class)
	{
		_modal_code = new string[]{"G00", "G94", "G80", "G17", "G21", "G98", "G90", "G40", "G50", "G22", 
			"G49", "G67", "G97", "G54", "G64", "G69", "G15", "G40.1", "G25", "G160", "G13.1", "G50.1", 
			"G54.2", "G80.5"};	
		code_location = new Dictionary<string, int>();
		LocationInitialize();
		for(int i = 0; i < aim_class.Modal_Code.Length; i++)
		{
			_modal_code[i] = aim_class.Modal_Code[i];
		}
	}
	
	private void LocationInitialize()
	{
		string[] group01_01 = new string[]{"G0", "G00", "G1", "G01", "G2", "G02", "G3", "G03", "G33", "G75", 
			"G77", "G78", "G79"};
		string[] group05_02 = new string[]{"G93", "G94", "G95"};
		string[] group09_03 = new string[]{"G73", "G74", "G76", "G80", "G81", "G82", "G83", "G84", "G84.2", 
			"G84.3", "G85", "G86", "G87", "G88", "G89"};
		string[] group02_04 = new string[]{"G17", "G18", "G19"};
		string[] group06_05 = new string[]{"G20", "G21"};
		string[] group10_06 = new string[]{"G98", "G99"};
		string[] group03_07 = new string[]{"G90", "G91"};
		string[] group07_08 = new string[]{"G40", "G41", "G42"};
		string[] group11_09 = new string[]{"G50", "G51"};
		string[] group04_10 = new string[]{"G22", "G23"};
		string[] group08_11 = new string[]{"G43", "G44", "G49"};
		string[] group12_12 = new string[]{"G66", "G67"};
		string[] group13_13 = new string[]{"G96", "G97"};
		string[] group14_14 = new string[]{"G54", "G54.1", "G55", "G56", "G57", "G58", "G59"};
		string[] group15_15 = new string[]{"G61", "G62", "G63", "G64"};
		string[] group16_16 = new string[]{"G68", "G69"};
		string[] group17_17 = new string[]{"G15", "G16"};
		string[] group18_18 = new string[]{"G40.1", "G41.1", "G42.1"};
		string[] group25_19 = new string[]{"G25"};
		string[] group20_20 = new string[]{"G160", "G161"};
		string[] group131_21 = new string[]{"G13.1"};
		string[] group22_22 = new string[]{"G50.1", "G51.1"};
		string[] group542_23 = new string[]{"G54.2"};
		string[] group805_24 = new string[]{"G80.5"};
		List<string[]> location_group = new List<string[]>();
		location_group.Add(group01_01);
		location_group.Add(group05_02);
		location_group.Add(group09_03);
		location_group.Add(group02_04);
		location_group.Add(group06_05);
		location_group.Add(group10_06);
		location_group.Add(group03_07);
		location_group.Add(group07_08);
		location_group.Add(group11_09);
		location_group.Add(group04_10);
		location_group.Add(group08_11);
		location_group.Add(group12_12);
		location_group.Add(group13_13);
		location_group.Add(group14_14);
		location_group.Add(group15_15);
		location_group.Add(group16_16);
		location_group.Add(group17_17);
		location_group.Add(group18_18);
		location_group.Add(group25_19);
		location_group.Add(group20_20);
		location_group.Add(group131_21);
		location_group.Add(group22_22);
		location_group.Add(group542_23);
		location_group.Add(group805_24);
		for(int i = 0; i < location_group.Count; i++)
		{
			for(int j = 0; j < location_group[i].Length; j++)
			{
				code_location.Add(location_group[i][j], i);
			}
		}
	}
	
	public int ModalIndex(string aim_code)
	{
		if(code_location.ContainsKey(aim_code))
			return code_location[aim_code];
		else
			return -1;
	}
	
	public bool SetModalCode(string aim_code, int index)
	{
		if(index > 23 || index < 0)
			return false;
		else
		{
			_modal_code[index] = aim_code;
			return true;
		}
	}	
}

/// <summary>
/// 静态加载工件坐标系
/// </summary>
public class LoadCoordinate
{
	public static Vector3  System(string coo_name)
	{
		Vector3 aim_vec = new Vector3(0, 0, 0);
		string filepath = Application.dataPath + "/Resources/Coordinate/" + coo_name + ".txt";
		if(File.Exists(filepath))
		{
			FileStream coo_filestream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
			StreamReader coo_streamreader = new StreamReader(coo_filestream);
			string line_str = coo_streamreader.ReadLine();
			if(line_str != null)
			{
				string[] coo_strarray = line_str.Split(',');
				try
				{
					aim_vec.x = (float)Convert.ToDouble(coo_strarray[0]);
					aim_vec.y = (float)Convert.ToDouble(coo_strarray[1]);
					aim_vec.z = (float)Convert.ToDouble(coo_strarray[2]);
				}
				catch
				{
					Debug.LogError(coo_name + "工件坐标系文件格式不正确！位置: " + filepath);
					aim_vec = Vector3.zero;
				}
			}	
		}
		return aim_vec;
	}
}

