using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The purpose of this script is to hold important information about the progress of the game.
/// </summary>
public class GameManager : MonoBehaviour
{
	//	This class is made statically accesible.
	//	In simple words that means that if we need to access it from another script,
	//	instead of having to do GetComponent<GameManager> or something similar, we can simple access it in one line of code by typing GameManager.Instance
	public static GameManager Instance;

	public bool playerIsDead;   //	To check if the player is dead

	public int score;   //	The score value

	// Awake is called before Start
	private void Awake()
	{
		//	Whenever we access GameManager.Instance we are accessing this class.
		//	We want to do this before start because maybe some function will need to access it on Start
		//	If it's not set yet it would cause a Null Reference Exception error.
		Instance = this;
	}


	// Start is called before the first frame update
	void Start()
	{
		//	When the game starts we want to update the score for the first time.
		UiManager.Instance.UI_UpdateScore(score);
	}

	/// <summary>
	/// Updates the score value and the Ui
	/// </summary>
	public void IncreaceScore()
	{
		//	Increment the score
		score++;

		//	Update the Ui of the score using the score value that was just updated.
		UiManager.Instance.UI_UpdateScore(score);
	}

	/// <summary>
	/// Restarts the game by reloading the Main scene
	/// </summary>
	public void RestartGame()
	{
		//	Find the active scene.
		Scene activeScene = SceneManager.GetActiveScene();
		//	Load the scene to restart the game.
		SceneManager.LoadScene(activeScene.name);
	}

	/// <summary>
	/// Kills the player and enables the Game Over options in the UI.
	/// </summary>
	public void KillPlayer()
	{
		//	The player is set as dead.
		playerIsDead = true;
		//	Show the Game Over options
		UiManager.Instance.ShowGameOverUI();
	}


}
