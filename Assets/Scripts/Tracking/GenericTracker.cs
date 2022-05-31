#define DEBUG
// #undef DEBUG
// #define DEBUG2
#undef DEBUG2

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class OrientationInfoList {
	public List<OrientationInfo> points;
    
	public OrientationInfoList(List<OrientationInfo> _points) {
        
		this.points = _points;
	}
}
[System.Serializable]
public class OrientationInfo {
	public Vector3 position;
	public Vector3 rotationEuler;
	public Quaternion rotation {
		get {
			return Quaternion.Euler(this.rotationEuler);
		}
	}
	public OrientationInfo()
	{
		this.position = new Vector3();
		this.rotationEuler = new Vector3();
	}
    
	public OrientationInfo(Transform tBase, Transform tTrackable){
		// NB! improve
		this.position = //tTrackable.position - tBase.position;
		                tTrackable.InverseTransformPoint(tBase.position);
		this.rotationEuler = tTrackable.eulerAngles - tBase.eulerAngles;
	}
    
	public void Add(OrientationInfo other) {
		this.position += other.position;
		this.rotationEuler += other.rotationEuler;
	}
	public void Divide(float divisor) {
		this.position /= divisor;
		this.rotationEuler /= divisor;
	}
}

public class GenericTracker : MonoBehaviour {
	public Transform tTrackable;
	public Transform tBase;
    
	protected OrientationInfo RecordValue(){
		#if DEBUG2
		Debug.Log("GenericTracker: Recording Value");
		#endif
		// Save Location
		return new OrientationInfo(this.tBase, this.tTrackable);
	}
}