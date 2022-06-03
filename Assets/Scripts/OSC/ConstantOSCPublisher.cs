//#define DEBUG
#undef DEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(OSC))]
public class ConstantOSCPublisher : MonoBehaviour {
	public Transform scaleCenter;
	public GameObject preview;
	public AnimationCurve curve;
	public Vector2 minMaxDistance = new Vector2(0, 2);

	private OSC osc;
	public ConstantTracker tracker;
	private OscMessage message = new OscMessage();
	public string topic = "/trackedObjects";
	public float scalar = 1;
	private Vector3 lastPosition = Vector3.zero;

	public bool simpleSender = false;


	void Start(){
		this.topic += "" + this.tracker.gameObject.name.Replace(" ", string.Empty);
        
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
			if (simpleSender)
				SimpleUpdateMessage();
			else
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
		Vector3 oscPoint = this.tracker.orientationInfo.position;
		Vector3 localScalingCenterPos = this.tracker.tTrackable.InverseTransformPoint(scaleCenter.position);
		Vector3 difference = oscPoint - localScalingCenterPos;
		float normalixzedDistance = Mathf.Clamp01(Mathf.InverseLerp(minMaxDistance.x, minMaxDistance.y, difference.magnitude));
		float curvedDistance = curve.Evaluate(normalixzedDistance);
		float newDistance = Mathf.Lerp(minMaxDistance.x, minMaxDistance.y, curvedDistance);
		difference = difference.normalized * newDistance;
		oscPoint = localScalingCenterPos + difference * scalar;
		preview.transform.position = this.tracker.tTrackable.TransformPoint(oscPoint);
		oscPoint = oscPoint.UnityPointToOscPoint();
		this.message.values.Add(Mathf.Clamp(oscPoint.x, .75f, 1.0f));
		this.message.values.Add(oscPoint.y);
		this.message.values.Add(oscPoint.z);
		// this.message.pose.orientation = this.tracker.orientationInfo.rotation.eulerAngles.UnityEulerToRosQuaternion();
	}
	private void SimpleUpdateMessage()
	{
		this.message.values.Clear();
		Vector3 oscPoint = this.tracker.orientationInfo.position;
		oscPoint = oscPoint.UnityPointToOscPoint();
		this.message.values.Add(oscPoint.x);
		this.message.values.Add(oscPoint.y);
		this.message.values.Add(oscPoint.z);
	}
	private void Publish(OscMessage message){
		#if DEBUG
		Debug.Log("ConstantOSCPublisher:sending: " + message.ToString());
		#endif
		this.osc.Send(message);
	}
}