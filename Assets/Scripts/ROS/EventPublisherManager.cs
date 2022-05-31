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

public class EventPublisherManager : MonoBehaviour {
	public string topicPrefix;
	public ConstantTracker tracker;
    
	[System.Serializable]
	public class TopicData {
		public string topicPostfix;
		public string indicatorMessage;
	}
    
	public TopicData[] topics;
	private List<EventTrackerPublisher> publishers = new List<EventTrackerPublisher>();
	private IncrementPublisher incrementPublisher;
	private int activeIndex = 0;
	public InputActionReference switchTopicActionReference;
	public InputActionReference publishActionReference;
	public InputActionReference incrementPublishActionReference;
    
	public TMPro.TextMeshProUGUI statusIndicator;
    
	private string FrameId = "map";
	private RosSharp.RosBridgeClient.MessageTypes.Geometry.PoseStamped message;
    
	public void OnEnable() {
		EventTrackerPublisher publisher;
		foreach  (TopicData data in this.topics) {
			publisher = gameObject.AddComponent<EventTrackerPublisher>();
			publisher.Topic = this.topicPrefix + "_" + this.tracker.gameObject.name.Replace(" ", string.Empty) + "_" + data.topicPostfix;
			this.publishers.Add(publisher);
		}
		this.incrementPublisher = gameObject.AddComponent<IncrementPublisher>();
		this.incrementPublisher.Topic = this.topicPrefix+"_"+"increment";
        
		this.switchTopicActionReference.action.Enable();
		this.switchTopicActionReference.action.performed += (context) => OnSwitchTopic();
		this.publishActionReference.action.Enable();
		this.publishActionReference.action.performed += (context) => OnPublish();
		this.incrementPublishActionReference.action.Enable();
		this.incrementPublishActionReference.action.performed += (context) => OnPublishIncrement();
        
		PrepareMessage();
        
		UpdateStatusIndicator();
	}
    
	private void OnSwitchTopic(){
		this.activeIndex++;
		this.activeIndex = this.activeIndex % this.topics.Length;
		Debug.Log("EventPublisherManager: active index: "+ this.activeIndex);
		UpdateStatusIndicator();
	}
    
	private void UpdateStatusIndicator(){
		this.statusIndicator.text = this.topics[this.activeIndex].indicatorMessage;
	}
    
	private void OnPublish(){
		try {
			UpdateMessage();
			this.publishers[this.activeIndex].OnPublish(this.message);
		} catch (Exception exception) {
			Debug.Log("EventPublisherManager: publish exception: "+ exception.ToString());
		}
	}
	private void OnPublishIncrement(){
		this.incrementPublisher.OnPublish();
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