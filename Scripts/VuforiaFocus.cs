using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VuforiaFocus : MonoBehaviour {
	void Start() {
		VuforiaApplication.Instance.OnVuforiaStarted += OnVuforiaStarted;
		VuforiaApplication.Instance.OnVuforiaPaused += OnPaused;
	}

	void OnVuforiaStarted() {
		VuforiaBehaviour.Instance.CameraDevice.SetFocusMode(FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		VuforiaBehaviour.Instance.CameraDevice.SetCameraMode(Vuforia.CameraMode.MODE_DEFAULT);
	}

	void OnPaused(bool paused) {
		if(!paused) { // Resumed
			// Set again auto focus mode when app is resumed
			VuforiaBehaviour.Instance.CameraDevice.SetFocusMode(FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		}
	}
}