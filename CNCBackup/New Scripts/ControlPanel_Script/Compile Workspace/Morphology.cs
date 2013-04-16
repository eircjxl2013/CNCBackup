using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IG_Code
{
	string ErrorMessage {get;}
	bool G_Check(string g_str, int row_index);
}

public interface IM_Code
{
	string ErrorMessage {get;}
	bool M_Check(string m_str, int row_index);
}

class G_FANUC_OI_M_T:IG_Code
{
	private string _errorMessage;
	public string ErrorMessage
	{
		get {return _errorMessage;}
		//set {_errorMessage = value;}
	}
	private List<string> satisfactoryG_T = new List<string>() {"G0", "G00", "G1", "G01", "G2", "G02", "G3", "G03", "G4", "G04",  "G5.4", "G05.4", "G7.1", "G07.1", "G8", "G08",
		"G9", "G09", "G10", "G11", "G12.1", "G13.1", "G17", "G18", "G19", "G20", "G21", "G22", "G23", "G25", "G26", "G27", "G28", "G30", "G31", "G32", "G33", "G34", "G36", "G37", "G39", "G40", "G41", 
		"G42", "G50", "G50.3", "G50.2", "G51.2", "G50.4", "G50.5", "G50.6", "G51.4", "G51.5", "G51.6", "G52", "G53", "G54", "G55", "G56", "G57", 
		"G58", "G59", "G61", "G63", "G64", "G65", "G66", "G67", "G68", "G69", "G70", "G71", "G72", "G73", "G74", "G75", "G76", "G80", "G81", 
		"G82", "G83", "G83.1", "G84", "G84.2", "G85", "G87", "G88", "G89", "G90", "G92", "G94", "G91.1", "G96", "G97", "G96.1", "G96.2", "G96.3", "G96.4", "G98", "G99", };
	public G_FANUC_OI_M_T()
	{
		_errorMessage = "";
	}
	
	public bool G_Check(string g_str, int row_index)
	{
		_errorMessage = "";
		Debug.Log("G_FANUC_T");
		if(satisfactoryG_T.IndexOf(g_str) == -1)
		{
			_errorMessage = "(" + row_index + "): " + "系统中不存在该G指令";
			return false;
		}
		else
			return true;
	}
}

class G_FANUC_OI_M_M:IG_Code
{
	private string _errorMessage;
	public string ErrorMessage
	{
		get {return _errorMessage;}
		//set {_errorMessage = value;}
	}
	private List<string> satisfactoryG_M = new List<string>() {"G0", "G00", "G1", "G01", "G2", "G02", "G3", "G03", "G4", "G04", "G5.1", "G05.1",  "G5.4", "G05.4", "G7.1", "G07.1",
		"G9", "G09", "G10", "G11", "G15", "G16", "G17", "G18", "G19", "G20", "G21", "G22", "G23", "G27", "G28", "G29", "G30", "G31", "G33","G37", "G39", "G40", "G41", 
		"G42", "G40.1", "G41.1", "G42.1", "G43", "G44", "G45", "G46", "G47", "G48", "G49", "G50", "G51", "G50.1", "G51.1", "G52", "G53", "G54", "G54.1", "G55", "G56", "G57", 
		"G58", "G59", "G60", "G61", "G62", "G63", "G64", "G65", "G66", "G67", "G68", "G69", "G73", "G74", "G75", "G76", "G77", "G78", "G79", "G80", "G80.4", "G81.4", "G81", 
		"G82", "G83", "G84", "G84.2", "G84.3", "G85", "G86", "G87", "G88", "G89", "G90", "G91", "G91.1", "G92", "G92.1", "G93", "G94", "G95", "G96", "G97", "G98", "G99", 
		"G160", "161"};
	public G_FANUC_OI_M_M()
	{
		_errorMessage = "";
	}
	
	public bool G_Check(string g_str, int row_index)
	{
		_errorMessage = "";
		Debug.Log("G_FANUC_M");
		if(satisfactoryG_M.IndexOf(g_str) == -1)
		{
			_errorMessage = "(" + row_index + "): " + "系统中不存在该G指令";
			return false;
		}
		else
			return true;
	}
}

class M_FANUC_OI_M:IM_Code
{
	private string _errorMessage;
	public string ErrorMessage
	{
		get {return _errorMessage;}
		//set {_errorMessage = value;}
	}
	
	public M_FANUC_OI_M()
	{
		_errorMessage = "";
	}
	
	private List<string> satisfactoryM = new List<string>() {"M0", "M00", "M1", "M01", "M2", "M02", "M3", "M03", "M4", "M04", 
		"M5", "M05", "M6", "M06", "M7", "M07", "M8", "M08", "M9", "M09", "M19", "M30", "M98", "M98"};
	public bool M_Check(string m_str, int row_index)
	{
		_errorMessage = "";
		Debug.Log("M_FANUC");
		if(satisfactoryM.IndexOf(m_str) == -1)
		{
			_errorMessage = "(" + row_index + "): " + "系统中不存在该M指令";
			return false;
		}
		else
			return true;
	}
}

public class VersatilityCode
{
	void Versatility()
	{
		Debug.Log("Versatility Code");
	}
}

public abstract class CompileBase
{
	protected IG_Code g_code_check;
	protected IM_Code m_code_check;
	private string _errorMessage;
	public string ErrorMessage
	{
		get{return _errorMessage;}
	}
	public CompileBase()
	{
		_errorMessage = "";
	}
	
	public bool G_Check(string code_str, int row_index)
	{
		_errorMessage = "";
		bool temp_flag = this.g_code_check.G_Check(code_str, row_index);
		_errorMessage = this.g_code_check.ErrorMessage;
		return temp_flag;
	}

	public bool M_Check(string code_str, int row_index)
	{
		_errorMessage = "";
		bool temp_flag = this.m_code_check.M_Check(code_str, row_index);
		_errorMessage = this.m_code_check.ErrorMessage;
		return temp_flag;
	}
}

public class FANUC_OI_M:CompileBase
{
	public FANUC_OI_M()
	{
		this.g_code_check = new G_FANUC_OI_M_M();
		this.m_code_check = new M_FANUC_OI_M();
	}
}

