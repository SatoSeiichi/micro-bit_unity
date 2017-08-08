
﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySerial;
public class SerialHandler : MonoBehaviour {

	//public GameObject rocket;
	public static UnitySerial.UnitySerial serial;

	public string portName = "COM8";//"/dev/tty.usbmodem1421";
	public int baudRate    = 9600;

	public Action<string> SerialCallBack;

	void Start()
	{
		serial = new UnitySerial.UnitySerial (portName, baudRate, 256); //256);
		//serial.ThreadStart ();
	}
	
	void Update()
	{
		if (serial != null) {
			string str = serial.GetData ();
			if (str != null) {
				//int r = int.Parse (str);
				//Debug.Log (str);

				SerialCallBack (str);
			}
		}
		//Debug.Log (serial.GetData ());
	}
	
	void OnDestroy()
	{
		serial.Close (); //ThreadEnd ();
	}
}

