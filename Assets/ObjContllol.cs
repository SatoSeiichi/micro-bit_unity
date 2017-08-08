using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjContllol : MonoBehaviour {

	public SerialHandler _SerialHandler;　//SerialHandler.csの参照
	Vector3 rotation;
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
		_SerialHandler.SerialCallBack = SerialCallBack;
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			SerialHandler.serial.Write ("ITEM");
		}
	}

	void SerialCallBack(string m)
	{
		switch (m) {
		case "a_button":
			print (m);
			break;
		case "b_button":
			print (m);
			break;
		case "y_button":
			print (m);
			rotationObj.GetComponent<Renderer> ().material.color = Color.red;
			break;
		case "x_button":
			print (m);
			rotationObj.GetComponent<Renderer> ().material.color = Color.blue;
			break;
		case "up":
			print (m);
			objMove (MOVE_STATE.UP);
			rotationObj.GetComponent<Renderer> ().material.color = Color.green;
			break;
		case "dawn":
			print (m);
			objMove (MOVE_STATE.DOWN);
			rotationObj.GetComponent<Renderer> ().material.color = Color.magenta;
			break;
		case "left":
			print (m);
			objMove (MOVE_STATE.LEFT);
			rotationObj.GetComponent<Renderer> ().material.color = Color.yellow;
			break;
		case "right":
			print (m);
			objMove (MOVE_STATE.RIGHT);
			rotationObj.GetComponent<Renderer> ().material.color = Color.cyan;
			break;
		default:
			//objRotation(m);
			break;
		}
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

	void objMove(MOVE_STATE state)
	{
		switch (state) {
		case MOVE_STATE.UP:
			transform.SetPositionAndRotation (transform.position + (Vector3.up * 0.05f),transform.rotation );
			break;
		case MOVE_STATE.DOWN:
			transform.SetPositionAndRotation (transform.position + (Vector3.down * 0.05f),transform.rotation );
			break;
		case MOVE_STATE.LEFT:
			transform.SetPositionAndRotation (transform.position + (Vector3.left * 0.05f),transform.rotation );
			break;
		case MOVE_STATE.RIGHT:
			transform.SetPositionAndRotation (transform.position + (Vector3.right * 0.05f),transform.rotation );
			break;
		}
	}

	void OnTriggerEnter(Collider co)
	{
		Destroy (co.gameObject);
		SerialHandler.serial.Write ("ITEM");
		Debug.LogError("item_get");
	}
}
