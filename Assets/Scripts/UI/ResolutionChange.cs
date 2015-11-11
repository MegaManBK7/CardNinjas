using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.Util;

public class ResolutionChange : MonoBehaviour {

	Dropdown dropdown;
	public ConfirmationDialogController CDC;

	public Resolution lastResolution;
	public int lastOption;

	public bool notFirstTime = false;

	public Selectable Quality;

	// Use this for initialization
	void Awake () {
		dropdown = GetComponent<Dropdown>();
		int i = 0;
		foreach (Resolution res in Screen.resolutions) {
			Dropdown.OptionData opt = new Dropdown.OptionData();
			opt.text = res.ToString();
			dropdown.options.Add(opt);
			if (Screen.currentResolution.ToString() == res.ToString()) 
				dropdown.gameObject.transform.GetChild(0).GetComponent<Text>().text = res.ToString();
			i++;
		}


	}
	
	// Update is called once per frame
	void Update () {		
		if (CustomInput.BoolFreshPress(CustomInput.UserInput.Accept) && EventSystem.current.currentSelectedGameObject == this.gameObject) {
			Navigation customNav = new Navigation();
			customNav.mode = Navigation.Mode.Automatic;
			dropdown.navigation = customNav;
		}
	}

	public void SetResolution() {
		Debug.Log(this.dropdown.value);
		EventSystem.current.SetSelectedGameObject(this.gameObject);
		Navigation customNav = new Navigation();
		customNav.mode = Navigation.Mode.Explicit;
		customNav.selectOnDown = Quality;
		dropdown.navigation = customNav;

		lastResolution = Screen.currentResolution;
		lastOption = dropdown.value;
		Screen.SetResolution(Screen.resolutions[this.dropdown.value].width, Screen.resolutions[this.dropdown.value].height, Screen.fullScreen);
		CDC.BringUpKeep();
		CDC.Go = ChangeResolutionBack;
	}

	public void ChangeResolutionBack() {
		Screen.SetResolution(lastResolution.width, lastResolution.height, Screen.fullScreen);
		//this.dropdown.value = this.lastOption;
	}
}
