using UnityEngine;
using System.Collections;
using Assets.Scripts.UI;

public class UILevelSwitch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LevelSwitch(int level) {
		Debug.Log(level);
		Application.LoadLevel(level);
	}

	public void ReloadLevel() {
		LevelSwitch(Application.loadedLevel);
	}

	public void SetLoadingLevel(string _level) {
		LoadingScreen.LevelToLoad = _level;
	}
}
