//#define DEBUG
#undef DEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(OSC))]
public class ConstantOSCPublisher : MonoBehaviour {
    
	private OSC osc;
	public ConstantTracker tracker;
	private OscMessage message = new OscMessage();
	public string topic = "/trackedObjects";
    
	void Start(){
		this.topic += "_" + this.tracker.gameObject.name.Replace(" ", string.Empty);
        
		this.osc = GetComponent<OSC>();
		PrepareMessage();
        
		this.tracker.onUpdated -= OnPublish;
		this.tracker.onUpdated += OnPublish;
	}
	private void OnPublish(){
		if (!this.osc.IsOpen) {
			Debug.Log("ConstantOSCPublisher: OSC not open");
			return;
		}
		try {
			UpdateMessage();
			Publish(message);
		} catch (Exception exception) {
			Debug.Log("ConstantOSCPublisher: publish exception: "+ exception.ToString());
		}
	}
	private void PrepareMessage() {
		this.message = new OscMessage();
		this.message.address = this.topic;
	}
	private void UpdateMessage(){
		this.message.values.Clear();
		Vector3 oscPoint = this.tracker.orientationInfo.position.UnityPointToOscPoint();
		this.message.values.Add(oscPoint.x);
		this.message.values.Add(oscPoint.y);
		this.message.values.Add(oscPoint.z);
		// this.message.pose.orientation = this.tracker.orientationInfo.rotation.eulerAngles.UnityEulerToRosQuaternion();
	}
	private void Publish(OscMessage message){
		#if DEBUG
		Debug.Log("ConstantOSCPublisher:sending: " + message.ToString());
		#endif
		this.osc.Send(message);
	}
}