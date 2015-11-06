using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.Util;
using UnityEngine.EventSystems;

public class SliderFix : MonoBehaviour {

	public Slider slider;
	public SoundPlayer sp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.RightArrow)) Navigate(CustomInput.UserInput.Right);
		if (Input.GetKey(KeyCode.LeftArrow)) Navigate(CustomInput.UserInput.Left);
	}

	#region NAVIGATION
	private void Navigate(CustomInput.UserInput direction)
	{
		if ( EventSystem.current.currentSelectedGameObject == this.gameObject) {
			switch(direction)
			{
			case CustomInput.UserInput.Left:
				slider.value -= .01f;
				break;
			case CustomInput.UserInput.Right:
				slider.value += .01f;
				break;
			}
			if (sp) sp.SetVolume(slider.value);
		}
	}
	#endregion
}
