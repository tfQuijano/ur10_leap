#define DEBUG
// #undef DEBUG
// #define DEBUG2
#undef DEBUG2

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantTracker : GenericTracker {
	public float timeFrequency;
	private Coroutine trackingRoutine;
	public int bufferSize = 10;
    
	private OrientationInfo[] buffer;
	private int currentBufferIndex = 0;
	[HideInInspector]
	public OrientationInfo orientationInfo {
		get {
			OrientationInfo average = new OrientationInfo();
			for (int i = 0; i < this.bufferSize; i++) {
				average.Add(this.buffer[i]);
			}
			average.Divide(this.bufferSize);
			return average;
		}
	}
    
	public delegate void OnUpdated();
	public OnUpdated onUpdated;
    
	void OnEnable ()
	{
        
		this.buffer = new OrientationInfo[this.bufferSize];
		this.currentBufferIndex = 0;
        
		this.trackingRoutine = StartCoroutine(Track());
	}
    
	IEnumerator Track()
	{
		for (int i = 0; i < this.bufferSize; i++) {
			this.buffer[this.currentBufferIndex] = RecordValue();
			this.currentBufferIndex = (this.currentBufferIndex + 1) % this.bufferSize;
		}
		while (true) {
			this.buffer[this.currentBufferIndex] = RecordValue();
			this.currentBufferIndex = (this.currentBufferIndex + 1) % this.bufferSize;
            
			this.onUpdated?.Invoke();
			yield return new WaitForSeconds(this.timeFrequency);
		}
	}
}