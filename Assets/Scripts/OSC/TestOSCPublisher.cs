using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(OSC))]
public class TestOSCPublisher : MonoBehaviour {
    
	private OSC osc;
	private OscMessage message = new OscMessage();
	public string topic = "/trackedObjects";
    
	void Start(){
		//this.topic += "_" + this.tracker.gameObject.name.Replace(" ", string.Empty);
        
		this.osc = GetComponent<OSC>();
	}
	[ContextMenu("Publish")]
	public void OnPublish(){
		if (!this.osc.IsOpen) {
			Debug.Log("TestOSCPublisher: OSC not open");
			return;
		}
		try {
			UpdateMessage();
			Publish(message);
		} catch (Exception exception) {
			Debug.Log("TestOSCPublisher: publish exception: "+ exception.ToString());
		}
	}
	private void UpdateMessage(){
		this.message = new OscMessage();
		this.message.address = this.topic;
		this.message.values.Add("Hello");
	}
	private void Publish(OscMessage message){
		Debug.Log("TestOSCPublisher:sending: " + message.ToString());
		this.osc.Send(message);
	}
}