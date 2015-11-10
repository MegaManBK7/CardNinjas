using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResolutionChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetResolution() {
		switch (GetComponent<Dropdown>().value) {
		case 0:
			Screen.SetResolution(1280, 720, Screen.fullScreen);
			break;
		case 1:
			Screen.SetResolution(1366, 768, Screen.fullScreen);
			break;
		case 2:
			Screen.SetResolution(1600, 900, Screen.fullScreen);
			break;
		case 3:
			Screen.SetResolution(1920, 1080, Screen.fullScreen);
			break;
		}
	}
}
