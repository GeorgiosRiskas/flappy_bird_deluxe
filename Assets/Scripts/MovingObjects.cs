using UnityEngine;

/// <summary>
/// The purpose of this class is to move (Translate) to the given direction, in this case left. 
/// All objects that move to the left should be parented under this.
/// </summary>
public class MovingObjects : MonoBehaviour
{
	//  The SerializeField is used to expose it in the editor even though it is a private variable.
	//  The values are not set in this script but in the Inspector
	//  Saving the information for the speed of movement
	[SerializeField] private float speed;


	// Update is called once per frame
	void Update()
	{
		//  If the player is dead, the game session has ended
		//  In that case we want everything to stop moving
		//  And so, we don't want to execute any code that would move it
		//  The return command makes the code not execute any further and return to the beginning of the Update loop.
		if (GameManager.Instance.playerIsDead)
		{
			return;
		}

		//	Moves this transform towards the left by Time.deltaTime
		transform.Translate(Vector3.left * speed * Time.deltaTime);
	}
}
