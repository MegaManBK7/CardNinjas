using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResolutionChange : MonoBehaviour {

	Dropdown dropdown;

	// Use this for initialization
	void Start () {
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
		Screen.SetResolution(Screen.resolutions[this.dropdown.value].width, Screen.resolutions[this.dropdown.value].height, Screen.fullScreen);
	}
}
