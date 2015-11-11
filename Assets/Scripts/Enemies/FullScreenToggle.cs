using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.Util;
using UnityEngine.EventSystems;

public class FullScreenToggle : MonoBehaviour {

	public Toggle toggle;

	public Resolution lastResolution;
	public ConfirmationDialogController CDC;

	// Use this for initialization
	void Start () {
		toggle.onValueChanged.AddListener (delegate {FullscreenUpdate ();});
		toggle.isOn = Screen.fullScreen;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void FullscreenUpdate() {
		lastResolution = Screen.currentResolution;
		Screen.fullScreen = toggle.isOn;
		Screen.SetResolution(Screen.resolutions[Screen.resolutions.Length-1].width, Screen.resolutions[Screen.resolutions.Length-1].width, toggle.isOn);
		
		CDC.BringUpKeep();
		CDC.ConfirmKeepAction = ChangeResolutionBack;
	}

	public void ChangeResolutionBack() {
		Screen.SetResolution(lastResolution.width, lastResolution.height, Screen.fullScreen);
	}
}
