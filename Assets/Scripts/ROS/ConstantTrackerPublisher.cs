#define DEBUG
// #undef DEBUG
// #define DEBUG2
#undef DEBUG2

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RosSharp.RosBridgeClient;
using System;

public class ConstantTrackerPublisher : UnityPublisher<RosSharp.RosBridgeClient.MessageTypes.Geometry.PoseStamped> {
	public ConstantTracker tracker;
    
	private string FrameId = "map";
	private RosSharp.RosBridgeClient.MessageTypes.Geometry.PoseStamped message;
    
	public void OnEnable() {
		this.Topic += "_" + tracker.gameObject.name.Replace(" ", string.Empty);
        
		PrepareMessage();
        
		this.tracker.onUpdated -= OnPublish;
		this.tracker.onUpdated += OnPublish;
	}
    
	private void OnPublish(){
		try {
			UpdateMessage();
			Publish(message);
		} catch (Exception exception) {
			Debug.Log("ConstantTrackerPublisher: publish exception: "+ exception.ToString());
		}
	}
    
	private void PrepareMessage() {
		this.message = new RosSharp.RosBridgeClient.MessageTypes.Geometry.PoseStamped {
			header = new RosSharp.RosBridgeClient.MessageTypes.Std.Header() {
				frame_id = this.FrameId
			},
			pose =  new RosSharp.RosBridgeClient.MessageTypes.Geometry.Pose()
		};
	}
    
	private void UpdateMessage(){
		this.message.header.Update();
		this.message.pose.position = this.tracker.orientationInfo.position.UnityPointToRosPoint();
		this.message.pose.orientation = this.tracker.orientationInfo.rotation.eulerAngles.UnityEulerToRosQuaternion();
	}
}