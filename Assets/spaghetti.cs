using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class spaghetti : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeEnabled() {
		this.GetComponent<Image>().enabled = !this.GetComponent<Image>().enabled;
	}
}
