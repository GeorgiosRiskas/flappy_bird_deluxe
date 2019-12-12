using UnityEngine;

/// <summary>
/// The purpose of this class is to check when player passes through an obstacle and reward with a point.
/// It should be added to obstacles on the same gameObject that has a Trigger.
/// </summary>
public class ScoreCounter : MonoBehaviour
{
	//	Whenever an object exits a trigger
	private void OnTriggerExit2D(Collider2D collision)
	{
		//  If the player is dead, the game session has ended
		//  In that case we want everything to stop moving
		//  And so, we don't want to execute any code that would move it
		//  The return command makes the code not execute any further and return to the beginning of the OnTriggerExit2D.
		if (GameManager.Instance.playerIsDead == true)
		{
			return;
		}

		//	If the gameObject that exited the trigger has the tag "Player" then the player just exited the trigger.
		//	In our case nothing else can possibly enter or exit triggers but it is still a good practice to check.
		if (collision.gameObject.tag == "Player")
		{
			//	Increase the score.
			GameManager.Instance.IncreaceScore();
		}
	}
}
