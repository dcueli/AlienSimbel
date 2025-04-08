using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngine.U2D.IK;

public class ScenesTransition : MonoBehaviour {
	private Animator Animator;

	[SerializeField, Tooltip("Last animation clip")] private AnimationClip LastAnimClip;

	// ParameterStringMode for the callback method
	private Action<string> cb;
	private string SceneName;


	private void Awake() {
		Animator = GetComponent<Animator>();
	}

	public void StartTransition(Action<string> pCb, string pScnName) {
		// Trigger transition
		Animator.SetTrigger(name:"StartTransition");
		
		// Invoke tehe calback method with delay
		cb = pCb;
		SceneName = pScnName;
		Invoke(methodName:"InvokeCb", LastAnimClip.length);
	}

	// Invoke to callback method with parameters (object scene)
	private void InvokeCb() {
		// Call to the callback method
		cb.Invoke(SceneName);
	}
}
