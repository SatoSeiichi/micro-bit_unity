using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppKit;
using System.Text;

public class ObjContllol : MonoBehaviour {
	ArduinoSerial serial;
	Vector3 rotation;
	public string portNum;
	public Transform rotationObj;
	public string massage;

	enum MOVE_STATE
	{
		UP,
		DOWN,
		LEFT,
		RIGHT,
	}

	void Start()
	{
		serial = ArduinoSerial.Instance;
		bool success = serial.Open(portNum, ArduinoSerial.Baudrate.B_115200);
		if (!success)
		{
			return;
		}
		serial.OnDataReceived += SerialCallBack;
	}
	void OnDisable()
	{
		serial.Close();
		serial.OnDataReceived -= SerialCallBack;
	}
	// Update is called once per frame
	void Update () {
	}

	void SerialCallBack(string m)
	{
		objMove (m);
		objRotation(m);
		massage = m;
	}

	void objRotation(string message)
	{
		string[] a;

		a = message.Split("="[0]);
		if(a.Length != 2) return;  
		int v = int.Parse( a[1]);
		switch(a[0])
		{
		case "pitch":
			rotation = new Vector2(v, rotation.y);
			break;
		case "roll":
			rotation = new Vector2( rotation.x, v);
			break;
		}
		Quaternion AddRot = Quaternion.identity;
		AddRot.eulerAngles = new Vector3( -rotation.x, 0, -rotation.y );

		transform.rotation = AddRot;
	}

	void objMove(string message)
	{
		string[] a;

		a = message.Split("="[0]);
		if(a.Length != 2) return;  
		int v = int.Parse( a[1]);
		string m = a [0];
		if (m == "button") {
			
			string mc = a [1];

			//a_button
			if (mc [0] == '1') {
				print ("a_button");
			}
			//b_button
			if (mc [1] == '1') {
				print ("b_button");
			}
//			//y_button
			if (mc [2] == '1') {
				print ("y_button");
				rotationObj.GetComponent<Renderer> ().material.color = Color.blue;
			}
			//x_button
			if (mc [3] == '1') {
				print ("x_button");
				rotationObj.GetComponent<Renderer> ().material.color = Color.red;
			}
			//up
			if (mc [4] == '1') {
				print ("up");
				transform.SetPositionAndRotation (transform.position + (Vector3.up * 0.05f), transform.rotation);
				rotationObj.GetComponent<Renderer> ().material.color = Color.green;
			}
			//dawn
			if (mc [5] == '1') {
				print ("dawn");
				transform.SetPositionAndRotation (transform.position + (Vector3.down * 0.05f), transform.rotation);
				rotationObj.GetComponent<Renderer> ().material.color = Color.magenta;
			}
			//left
			if (mc [6] == '1') {
				print ("left");
				transform.SetPositionAndRotation (transform.position + (Vector3.left * 0.05f), transform.rotation);
				rotationObj.GetComponent<Renderer> ().material.color = Color.yellow;
			}
			//right
			if (mc [7] == '1') {
				print ("right");
				transform.SetPositionAndRotation (transform.position + (Vector3.right * 0.05f), transform.rotation);
				rotationObj.GetComponent<Renderer> ().material.color = Color.cyan;
			}

		}
	}

	void OnTriggerEnter(Collider co)
	{
		Destroy (co.gameObject);
		serial.Write ("ITEM");
		Debug.LogError("item_get");
	}
}
