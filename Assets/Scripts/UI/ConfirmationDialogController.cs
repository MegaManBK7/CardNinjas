using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.Util;

/// <summary>
/// Confirmation dialog controller.
/// </summary>
public class ConfirmationDialogController : MonoBehaviour {
	
	// In Game Dialog GameObjects
	#region public members
	public GameObject Confirmation;
	public GameObject ConfirmationSelect;
	
	public GameObject Keep;
	public GameObject KeepSelect;
	
	public EventSystem es;
	public UIHideBehaviour hideBehaviour;

	public Action ConfirmKeepAction;
	public float Timer = 3.0f;

	public GameObject PreviousSelected;
	#endregion
	
	#region Monobehaviour
	
	public void Update() {
		if (CustomInput.BoolFreshPress(CustomInput.UserInput.Cancel)) {
			if (hideBehaviour.OnScreen)
				this.DismissDialog();
		}

		if (Keep.activeSelf || this.hideBehaviour.OnScreen) {
			Timer -= Time.deltaTime;
			if (Timer < 0.0f) {
				if (ConfirmKeepAction != null) ConfirmKeepAction();
				this.DismissDialog();
			}
		}
	}
	#endregion
	
	#region DialogControls
	public void BringUpConfirm() {
		Keep.SetActive(false);
		Confirmation.SetActive(true);
		
		hideBehaviour.OnScreen = true;

		PreviousSelected = es.currentSelectedGameObject;
		es.SetSelectedGameObject(ConfirmationSelect);
	}

	public void BringUpKeep() {
		Confirmation.SetActive(false);
		Keep.SetActive(true);
		
		hideBehaviour.OnScreen = true;

		PreviousSelected = es.currentSelectedGameObject;
		es.SetSelectedGameObject(KeepSelect);
	}

	public void DismissDialog() {
		hideBehaviour.OnScreen = false;
		this.Timer = 3.0f;
		this.ConfirmKeepAction = null;
		es.SetSelectedGameObject(PreviousSelected);
	}
	#endregion

	#region Button Action Calls
	public void ButtonActionCall() {
		if (ConfirmKeepAction != null) ConfirmKeepAction();
		this.DismissDialog();
	}
	#endregion
}
