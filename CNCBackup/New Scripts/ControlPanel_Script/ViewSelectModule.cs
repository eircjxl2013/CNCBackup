//内容--增加脚本文件ViewSeleModule，用于实现数控机床的正视图、俯视图和左视图
//姓名--刘旋，时间--2013-4-1

using UnityEngine;
using System.Collections;

public class ViewSelectModule : MonoBehaviour {
	GameObject mainObject;//内容--用于获得机床的基准坐标点，姓名--刘旋，时间--2013-4-1
	GameObject mainCamera;//内容--用于对摄像机进行设置，姓名--刘旋，时间--2013-4-1
	float x_distance;//内容--x轴方向上摄像机偏离机床基准点的距离，姓名--刘旋，时间--2013-4-1
	float y_distance;//内容--y轴方向上摄像机偏离机床基准点的距离，姓名--刘旋，时间--2013-4-1
	float z_distance;//内容--z轴方向上摄像机偏离机床基准点的距离，姓名--刘旋，时间--2013-4-1
	Vector3 front_position;//内容--正视图下摄像机偏离机床基准点的位置坐标，姓名--刘旋，时间--2013-4-1
	Vector3 top_position;//内容--俯视图下摄像机偏离机床基准点的位置坐标，姓名--刘旋，时间--2013-4-1
	Vector3 left_position;//内容--左视图下摄像机偏离机床基准点的位置坐标，姓名--刘旋，时间--2013-4-1
	
	
	void Awake()
	{
		mainObject=GameObject.Find("zero");//内容--选取机床零点坐标作为机床的基准点，姓名--刘旋，时间--2013-4-1
		mainCamera=GameObject.Find("Main Camera");
		z_distance=30f;
		left_position=new Vector3(0f,1.2f,z_distance);
		y_distance=45f;
		top_position=new Vector3(0f,y_distance,-1.1f);
		x_distance=30f;
		front_position=new Vector3(-x_distance,1.2f,-0.5f);
		
	}

	// Use this for initialization
	void Start () {
		
	
	}
	void OnGUI()
	{
		if(GUI.Button(new Rect(10,190,100,40),"Front View"))
			Front();
		if(GUI.Button(new Rect(10,240,100,40),"Top View"))
			Top();
		if(GUI.Button(new Rect(10,290,100,40),"Left View"))
			Left();
	}
	void Front()
	{
		mainCamera.transform.position = mainObject.transform.position;
		mainCamera.transform.rotation = mainObject.transform.rotation;//内容--使摄像机的位置和旋转信息与机床基准点保持一致，姓名--刘旋，时间--2013-4-1
		mainCamera.transform.position += front_position;
		mainCamera.transform.Rotate(0f, 90f, 0f);//内容--摄像机相对于基准点进行偏离和旋转，姓名--刘旋，时间--2013-4-1
		mainCamera.transform.eulerAngles = new Vector3(0, 90f, 0);
	}
	void Top()
	{
		mainCamera.transform.position = mainObject.transform.position;
		mainCamera.transform.rotation = mainObject.transform.rotation;
		mainCamera.transform.Rotate(90f, 0f, 0f);
		mainCamera.transform.position += top_position;
		mainCamera.transform.eulerAngles = new Vector3(90f, 0, 0);
	}
	void Left()
	{
		mainCamera.transform.position = mainObject.transform.position;
		mainCamera.transform.rotation = mainObject.transform.rotation;
		mainCamera.transform.position += left_position;
		mainCamera.transform.Rotate(0f,180f, 0);
		mainCamera.transform.position = new Vector3(0, 0, 0);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
