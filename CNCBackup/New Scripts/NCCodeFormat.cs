using UnityEngine;
using System.Collections;
//For List
using System.Collections.Generic;
//For Regex --- Regular Expression:正则表达式
using System.Text.RegularExpressions;

public class NCCodeFormat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Debug.Log(CodeFormat("while#if#s3fdaififgd##s###f[g][][][[####hello]").Count);		
		
		Debug.Log(CodeFormat("G10G30s768G12f199z723i+23j-234").Count);
	}
	
	/// <summary>
	/// 对输入的每一NC程序段进行格式化.
	/// </summary>
	/// <returns>
	/// 返回List<string>类型的格式化后的NC代码.
	/// </returns>
	/// <param name='sourceCode'>
	/// 传入的NC程序段,类型为string.
	/// </param>
	public List<string> CodeFormat(string sourceCode)
	{
		//Initialize code segment data struct
		List<string> code_segment = new List<string>();
		//Remove the "space" , "%" and ";" in the source NC code and make each letter uppercase letters
		sourceCode = sourceCode.ToUpper().Trim().Trim(';', '；', '%');
		//Remove the annotation of NC code like: (blah blah blah)
		/*
		 * 正则表达式字符串匹配
		 * 解决“()”匹配问题，可识别嵌套的(***(***))
		 * 涉及到了平衡组，用堆栈的思想进行判断
		 * 详情请见以下网址
		 * http://deerchao.net/tutorials/regex/regex.htm
		 * 注意： 是否需要考虑只有"("或者“)”的情况？这种情况下是否要报错？载入代码时可以不用考虑，编译代码时再考虑。
		*/
		Regex annotation_Reg = new Regex(@"\([^\(\)]*(((?'Open'\()[^\(\)]*)+((?'-Open'\))[^\(\)]*)+)*(?(Open)(?!))\)");
		MatchCollection annotation_Col = annotation_Reg.Matches(sourceCode);
		if(annotation_Col.Count > 0)
		{
			for(int i = 0; i < annotation_Col.Count; i++)
			{
				sourceCode = sourceCode.Replace(annotation_Col[i].Value.ToString(), "");
			}	
		}
		//If sourceCode is not null, format the source NC code then. Otherwise, return an empty List<string>() type.
		if(sourceCode != "")
		{
			//To judge whether it is macroprogram or not
			Regex macro_Reg = new Regex(@"((#+)|(\[+)|(\]+)|(=+)|(GOTO)|(IF)|(WHILE)|(END))", RegexOptions.IgnoreCase);
			MatchCollection macro_Col = macro_Reg.Matches(sourceCode);
			if(macro_Col.Count > 0)
			{
				// It contains macroprogram
				// Todo：考虑有宏程序
				/* For test
				for(int i = 0; i < macro_Col.Count; i++)
				{
					Debug.Log(i+1 + ": " + macro_Col[i].Value);		
				}
				*/
			}
			else
			{
				//It doest't contain macroprogram
				Regex format_normal_Reg = new Regex(@"([A-Z]+[^A-Z^\s]+)+", RegexOptions.IgnoreCase);
				Match format_normal_Mat = format_normal_Reg.Match(sourceCode);
				if(format_normal_Mat.Groups.Count > 1)
				{
					for(int i = 0; i < format_normal_Mat.Groups[1].Captures.Count; i++)
					{
						code_segment.Add(format_normal_Mat.Groups[1].Captures[i].Value);
						//Debug.Log(format_normal_Mat.Groups[1].Captures[i].Value);
					}
					code_segment.Add(";");
				}
				else
					Debug.Log("正常的NC代码还有未考虑到的情况或者代码格式完全不对！");
			}
		}
		return code_segment;
	}
}
