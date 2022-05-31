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

public class EventTrackerPublisher : UnityPublisher<RosSharp.RosBridgeClient.MessageTypes.Geometry.PoseStamped> {
	public void OnPublish(RosSharp.RosBridgeClient.MessageTypes.Geometry.PoseStamped message){
		try {
			Publish(message);
		} catch (Exception exception) {
			Debug.Log("EventTrackerPublisher: publish exception: "+ exception.ToString());
		}
	}
}