using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResolutionChange : MonoBehaviour {

	Dropdown dropdown;
	public ConfirmationDialogController CDC;

	public Resolution lastResolution;

	public bool notFirstTime = false;

	// Use this for initialization
	void Awake () {
		dropdown = GetComponent<Dropdown>();
		int i = 0;
		foreach (Resolution res in Screen.resolutions) {
			Dropdown.OptionData opt = new Dropdown.OptionData();
			opt.text = res.ToString();
			dropdown.options.Add(opt);
			if (Screen.currentResolution.ToString() == res.ToString()) dropdown.value = i;
			i++;
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetResolution() {
		if (notFirstTime) {
			lastResolution = Screen.currentResolution;
			Screen.SetResolution(Screen.resolutions[this.dropdown.value].width, Screen.resolutions[this.dropdown.value].height, Screen.fullScreen);
			CDC.BringUpKeep();
			CDC.ConfirmKeepAction = ChangeResolutionBack;
		}
		else {
			notFirstTime = true;
		}
	}

	public void ChangeResolutionBack() {
		Screen.SetResolution(lastResolution.width, lastResolution.height, Screen.fullScreen);
	}
}
