using UnityEngine;
using System.Collections;

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
}
