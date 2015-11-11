using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.Util;
using UnityEngine.EventSystems;

public class FullScreenToggle : MonoBehaviour {

	public Toggle toggle;

	// Use this for initialization
	void Start () {
		toggle.onValueChanged.AddListener (delegate {FullscreenUpdate ();});
		toggle.isOn = Screen.fullScreen;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void FullscreenUpdate() {
		Screen.fullScreen = toggle.isOn;
		Screen.SetResolution(Screen.resolutions[Screen.resolutions.Length].width, Screen.resolutions[Screen.resolutions.Length].width, toggle.isOn);
	}


}
