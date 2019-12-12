using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The purpose of this script is to modify the UI elements
/// </summary>
public class UiManager : MonoBehaviour
{
	//	This class is made statically accesible.
	//	In simple words that means that if we need to access it from another script,
	//	instead of having to do GetComponent<UiManager> or something similar, we can simple access it in one line of code by typing UiManager.Instance
	public static UiManager Instance;

	//  The SerializeField is used to expose it in the editor even though it is a private variable.
	//  The values are not set in this script but in the Inspector
	[SerializeField] private Text scoreText;    //	The UI text component that is under the UI_Canvas.
	[SerializeField] private GameObject gameOverUI; //	The Gameobject that has as child objects the Game Over text and the restart button.

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
		//	When the game starts we want the Game Over related elements
		HideGameOverUI();
	}

	/// <summary>
	/// Shows the Game Over UI elements.
	/// </summary>
	public void ShowGameOverUI()
	{
		//	Enables the Gameobject that has as child objects the Game Over text and the restart button.
		gameOverUI.SetActive(true);
	}

	/// <summary>
	/// Hides the Game Over UI elements.
	/// </summary>
	public void HideGameOverUI()
	{
		//	Disables the Gameobject that has as child objects the Game Over text and the restart button.
		gameOverUI.SetActive(false);
	}

	/// <summary>
	/// Updates the score in the UI text.
	/// </summary>
	/// <param name="score"></param>
	public void UI_UpdateScore(int score)
	{
		//	Changing the text accordingly
		scoreText.text = "SCORE: " + score;
	}



}
