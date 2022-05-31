using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCSettings {
	public int inPort  = 6969;
	public string outIP = "127.0.0.1";
	public int outPort  = 6161;
}
public class OSCServerFromFile : OSC {
	public string configAddress = "C:/_CONFIG/osc_config.txt";
    
	public override void Awake(){
		string text = System.IO.File.ReadAllText(this.configAddress);
		OSCSettings settings = JsonUtility.FromJson<OSCSettings>(text);
		this.inPort = settings.inPort;
		this.outIP = settings.outIP;
		this.outPort = settings.outPort;
		base.Awake();
	}
}