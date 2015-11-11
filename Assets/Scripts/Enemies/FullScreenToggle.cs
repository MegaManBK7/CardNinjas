using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.Util;
using UnityEngine.EventSystems;

public class FullScreenToggle : MonoBehaviour {

	public Toggle toggle;
	public bool isToggleOn;

	public Resolution lastResolution;
	public ConfirmationDialogController CDC;

	// Use this for initialization
	void Start () {
		toggle.isOn = Screen.fullScreen;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void FullscreenUpdate() {
		if (EventSystem.current.currentSelectedGameObject == this.gameObject) {
			lastResolution = Screen.currentResolution;
			Screen.fullScreen = toggle.isOn;
			
			CDC.BringUpKeep();
			CDC.Go = this.ChangeResolutionBack;
		}
	}

	public void FlipToggle() {
		this.toggle.isOn = !this.toggle.isOn;
	}

	public void ChangeResolutionBack() {
		Screen.SetResolution(lastResolution.width, lastResolution.height, Screen.fullScreen);
		Debug.Log(CDC.PreviousSelected);

		//  What the hell unity
		//if (this.toggle.isOn) this.toggle.isOn = false;
		//if(this.toggle.isOn == false) this.toggle.isOn = true;
		//this.toggle.isOn = !this.toggle.isOn;
		this.FlipToggle();

		Debug.Log(CDC.PreviousSelected);
	}
}
