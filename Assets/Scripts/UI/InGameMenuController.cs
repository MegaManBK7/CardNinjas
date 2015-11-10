using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.Util;

public class InGameMenuController : MonoBehaviour {

	public GameObject Pause;
	public GameObject PauseSelect;

	public GameObject Win;
	public GameObject WinSelect;

	public GameObject Lose;
	public GameObject LoseSelect;

	public GameObject P1Win;
	public GameObject P1WinSelect;

	public GameObject P2Win;
	public GameObject P2WinSelect;

	public EventSystem es;
	public UIHideBehaviour hideBehaviour;

	#region Monobehaviour

	public void Update() {
		if (CustomInput.BoolFreshPress(CustomInput.UserInput.Pause)) {
			if (hideBehaviour.OnScreen)
				this.DismissDialog();
			else
				this.BringUpPause();
		}

		if (hideBehaviour.OnScreenPos.position == hideBehaviour.transform.position && hideBehaviour.OnScreen)
			Time.timeScale = 0.00000001f;
	}
	#endregion

	#region MenuControls
	public void BringUpP2Win() {
		Win.SetActive(false);
		Lose.SetActive(false);
		P1Win.SetActive(false);
		Pause.SetActive(false);
		P2Win.SetActive(true);
		
		hideBehaviour.OnScreen = true;
		
		es.SetSelectedGameObject(P2WinSelect);
	}

	public void BringUpP1Win() {
		Win.SetActive(false);
		Lose.SetActive(false);
		Pause.SetActive(false);
		P2Win.SetActive(false);
		P1Win.SetActive(true);
		
		hideBehaviour.OnScreen = true;
		
		es.SetSelectedGameObject(P1WinSelect);
	}

	public void BringUpLose() {
		Win.SetActive(false);
		Pause.SetActive(false);
		P1Win.SetActive(false);
		P2Win.SetActive(false);
		Lose.SetActive(true);
		
		hideBehaviour.OnScreen = true;
		
		es.SetSelectedGameObject(LoseSelect);
	}

	public void BringUpWin() {
		Pause.SetActive(false);
		Lose.SetActive(false);
		P1Win.SetActive(false);
		P2Win.SetActive(false);
		Win.SetActive(true);
		
		hideBehaviour.OnScreen = true;
		
		es.SetSelectedGameObject(WinSelect);
	}

	public void BringUpPause() {
		Win.SetActive(false);
		Lose.SetActive(false);
		P1Win.SetActive(false);
		P2Win.SetActive(false);
		Pause.SetActive(true);

		hideBehaviour.OnScreen = true;
		
		es.SetSelectedGameObject(PauseSelect);
	}

	public void DismissDialog() {
		hideBehaviour.OnScreen = false;
		Time.timeScale = 1;
	}
	#endregion
}
