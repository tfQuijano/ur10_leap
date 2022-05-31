#define DEBUG
// #undef DEBUG
// #define DEBUG2
#undef DEBUG2

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RosSharp.RosBridgeClient;
using System;
using System.Threading;
using RosSharp.RosBridgeClient.Protocols;

public class RosServerFromFile : RosConnector {
	public string configAddress = "C:/_CONFIG/config.txt";
	public TMPro.TextMeshProUGUI statusIndicator;
    
	private enum Status { Waiting, Successful, Failed, Disconnected };
	private Status status = Status.Waiting;
    
	public override void Awake(){
		string text = System.IO.File.ReadAllText(this.configAddress);
		this.RosBridgeServerUrl = text;
		base.Awake();
	}
	void Update(){
		switch (this.status) {
		 case Status.Waiting:
			 this.statusIndicator.text = "Connecting to " + this.RosBridgeServerUrl;
			 break;
		 case Status.Successful:
			 this.statusIndicator.text = "Connection to " + this.RosBridgeServerUrl + " successful";
			 break;
		 case Status.Failed:
			 this.statusIndicator.text = "Connection to " + this.RosBridgeServerUrl + " failed";
			 break;
		 case Status.Disconnected:
			 this.statusIndicator.text = "Connection to " + this.RosBridgeServerUrl + " disconnected";
			 break;
		}
	}
	protected override void ConnectAndWait() {
		RosSocket = ConnectToRos(protocol, RosBridgeServerUrl, OnConnected, OnClosed, Serializer);
        
		if (!IsConnected.WaitOne(SecondsTimeout * 1000)) {
			Debug.LogWarning("Failed to connect to RosBridge at: " + RosBridgeServerUrl);
			this.status = Status.Failed;
		}
	}
	protected override void OnConnected(object sender, EventArgs e) {
		base.OnConnected(sender, e);
		this.status = Status.Successful;
	}
    
	protected override void OnClosed(object sender, EventArgs e) {
		base.OnClosed(sender, e);
		this.status = Status.Disconnected;
	}
}