using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class ExitOnAction : MonoBehaviour {
	public InputActionReference exitActionReference;
    
	void Awake(){
		this.exitActionReference.action.Enable();
		this.exitActionReference.action.performed += (context) => OnExit();
	}
    
	void OnExit(){
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}