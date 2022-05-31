#define DEBUG
// #undef DEBUG
// #define DEBUG2
#undef DEBUG2

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RosSharp.RosBridgeClient;
using System;
using UnityEngine.InputSystem;

public class IncrementPublisher : UnityPublisher<RosSharp.RosBridgeClient.MessageTypes.Std.Int32> {
	private int incrementer = 0;
    
	private RosSharp.RosBridgeClient.MessageTypes.Std.Int32 message;
    
	public void OnEnable() {
		PrepareMessage();
	}
	public void OnPublish(){
		try {
			Publish(message);
		} catch (Exception exception) {
			Debug.Log("EventIncrementPublisher: publish exception: "+ exception.ToString());
		}
	}
    
	private void PrepareMessage() {
		this.message = new RosSharp.RosBridgeClient.MessageTypes.Std.Int32();
	}
    
	private void UpdateMessage(){
		this.message.data = this.incrementer;
		this.incrementer++;
	}
}