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
		Application.LoadLevel(level);
	}
}
