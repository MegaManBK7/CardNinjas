using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.Util;
using Assets.Scripts.Managers;

/// <summary>
/// In game menu controller.
/// </summary>
/// <remarks>Handles activating and deactivating pause and game-end screens.</remarks>
public class InGameMenuController : MonoBehaviour {

	// In Game Menu GameObjects
	#region public members
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

	public bool IsMultiplayer;
	#endregion

	#region Monobehaviour
 
	public void Update() {
		if (CustomInput.BoolFreshPress(CustomInput.UserInput.Pause)) {
			if (hideBehaviour.OnScreen)
				this.DismissDialog();
			else
				this.BringUpPause();
		}

		if (hideBehaviour.OnScreenPos.position == hideBehaviour.transform.position && hideBehaviour.OnScreen) {
			GameManager.Pause = true;
			if (CustomInput.BoolFreshPress(CustomInput.UserInput.Cancel)) this.DismissDialog();
		}

		if (this.IsMultiplayer) {
			if (GameManager.Player1Win) {
				this.BringUpWin();
			}
			else if (GameManager.Player1Lose) {
				this.BringUpLose();
			}
		}
		else {
			if (GameManager.Player1Win) {
				this.BringUpP1Win();
			}
			else if (GameManager.Player1Lose) {
				this.BringUpP2Win();
			}
		}
	}
	#endregion

	#region MenuControls
	/// <summary>
	/// Brings up p2 wins window.
	/// </summary>
	public void BringUpP2Win() {
		Win.SetActive(false);
		Lose.SetActive(false);
		P1Win.SetActive(false);
		Pause.SetActive(false);
		P2Win.SetActive(true);
		
		hideBehaviour.OnScreen = true;
		
		es.SetSelectedGameObject(P2WinSelect);
	}

	/// <summary>
	/// Brings up p1 wins window.
	/// </summary>
	public void BringUpP1Win() {
		Win.SetActive(false);
		Lose.SetActive(false);
		Pause.SetActive(false);
		P2Win.SetActive(false);
		P1Win.SetActive(true);
		
		hideBehaviour.OnScreen = true;
		
		es.SetSelectedGameObject(P1WinSelect);
	}

	/// <summary>
	/// Brings up Single Player lose window.
	/// </summary>
	public void BringUpLose() {
		Win.SetActive(false);
		Pause.SetActive(false);
		P1Win.SetActive(false);
		P2Win.SetActive(false);
		Lose.SetActive(true);
		
		hideBehaviour.OnScreen = true;
		
		es.SetSelectedGameObject(LoseSelect);
	}

	/// <summary>
	/// Brings up Single Player win window.
	/// </summary>
	public void BringUpWin() {
		Pause.SetActive(false);
		Lose.SetActive(false);
		P1Win.SetActive(false);
		P2Win.SetActive(false);
		Win.SetActive(true);
		
		hideBehaviour.OnScreen = true;
		
		es.SetSelectedGameObject(WinSelect);
	}

	/// <summary>
	/// Brings up pause window.
	/// </summary>
	public void BringUpPause() {
		Win.SetActive(false);
		Lose.SetActive(false);
		P1Win.SetActive(false);
		P2Win.SetActive(false);
		Pause.SetActive(true);

		hideBehaviour.OnScreen = true;
		
		es.SetSelectedGameObject(PauseSelect);
	}

	/// <summary>
	/// Dismisses the current in game dialog.
	/// </summary>
	public void DismissDialog() {
		hideBehaviour.OnScreen = false;
		GameManager.Pause = false;
	}
	#endregion
}
