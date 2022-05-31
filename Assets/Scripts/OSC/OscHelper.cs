using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class OSCHelper {
	public static Vector3 UnityPointToOscPoint(this Vector3 unityPoint) {
		return new Vector3(unityPoint.x,
		                   unityPoint.z,
		                   unityPoint.y);
	}
	public static Vector3 OscPointToUnity(this Vector3 oscPoint) {
		return new Vector3(oscPoint.x,
		                   oscPoint.z,
		                   oscPoint.y);
	}
    
	// public static RosSharp.RosBridgeClient.MessageTypes.Geometry.Quaternion UnityEulerToRosQuaternion(this Vector3 unityEulerOrientation) {
	// 	Quaternion unityOrientation = Quaternion.Euler(unityEulerOrientation.x, unityEulerOrientation.z, unityEulerOrientation.y);
	//
	// 	return new RosSharp.RosBridgeClient.MessageTypes.Geometry.Quaternion(
	// 	                                                                     Convert.ToDouble(unityOrientation.x),
	// 	                                                                     Convert.ToDouble(unityOrientation.y),
	// 	                                                                     Convert.ToDouble(unityOrientation.z),
	// 	                                                                     Convert.ToDouble(unityOrientation.w));
	// }
	//
	// public static Vector3 RosQuaternionToUnityEuler(this RosSharp.RosBridgeClient.MessageTypes.Geometry.Quaternion rosOrientation)
	// {
	// 	Quaternion unityOrientation = new Quaternion(
	// 	                                             Convert.ToSingle(rosOrientation.x),
	// 	                                             Convert.ToSingle(rosOrientation.y),
	// 	                                             Convert.ToSingle(rosOrientation.z),
	// 	                                             Convert.ToSingle(rosOrientation.w));
	// 	Vector3 rosEulerOrientation = unityOrientation.eulerAngles;
	//
	// 	return new Vector3(
	// 	                   rosEulerOrientation.x,
	// 	                   rosEulerOrientation.z,
	// 	                   rosEulerOrientation.y);
	// }
}