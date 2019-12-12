using UnityEngine;

/// <summary>
/// The purpose of this class is to rearrange the obstacles
/// </summary>
public class ObstaclesController : MonoBehaviour
{
	private Vector2 obstacleReferencePos;   //	A reference for the starting position of this object. Used for rearranging the obstacles.

	//  The SerializeField is used to expose it in the editor even though it is a private variable.
	//  The values are not set in this script but in the Inspector
	[SerializeField] private Transform[] obstacles; // Gathering all the obstacles' Transforms, so we can later rearrange them
	[SerializeField] private float distanceX;   //	Used for rearranging the obstacles on the X axis - the distance between thm
	[SerializeField] private float obstacleMinY;    //	Used for rearranging the obstacles on the y axis - the lowest it can position itself
	[SerializeField] private float obstacleMaxY;    //	Used for rearranging the obstacles on the y axis - the highest it can position itself

	private int obstacleIndex = 0;  //	Used for accessing different elements of the obstacles array.	

	// Start is called before the first frame update
	void Start()
	{
		//	The first time we want to reposition an obstacle it will be distanceX to the right of the starting position.
		//	In the beginning of the game this reference is wherever this object is positioned.
		obstacleReferencePos = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		//  If the player is dead, the game session has ended
		//  In that case we want everything to stop moving
		//  And so, we don't want to execute any code that would move it
		//  The return command makes the code not execute any further and return to the beginning of the Update loop.
		if (GameManager.Instance.playerIsDead == true)
		{
			return;
		}

		/*	We want the obstacles to loop and always have distance equal to distanceX between them (on the X axis).
		 *	We want the obstacles to use any number between the obstacleMinY and the obstacleMaxY whenever they are rearranged (on the Y axis).
		 *	Every time this object moves a distance equal to distanceX to the left we want to rearrange the obstacles.
		 *	We want to arrange them one at a time, every time the condition is met (described in line above).
		 *	We want to position an obstacle distanceX units to the right of the last positioned obstacle
		 *	
		 *	We have to make sure of 2 things: 
		 *	1) The obstacles are positioned away from the area that is rendered in the game and far enough to not cause any collisions.
		 *	The x axis is important because it will be used as a reference for the first obstacle of the array.
		 *	2) The obstacles should be enough for the player to never see them move while playing. 
		 *	The amount depends on the aspect ratio used and the amount of obstacles that can be seen at the same time. In this example 3 are enough for mobile portrait 9:16 aspect ratio.
	*/

		//	If the x value of the currrent position has moved equal to the obstacleReferencePos position - distanceX, it's time to rearrange the obstacles.
		//	Subtraction is used because the obstacles move to the left.
		if (transform.position.x <= (obstacleReferencePos.x - distanceX))
		{
			//	We need to reassign the reference position for the next time the position will move equal to distanceX.
			//	The new reference position will be wherever it was previously but moved distanceX units to the left.
			obstacleReferencePos = new Vector2(obstacleReferencePos.x - distanceX, transform.position.y);

			//	Calculate a random number between the obstacle's minimum and maximum Y.
			//	It will be used to change the obstacle's position on the Y axis.
			float randomY = Random.Range(obstacleMinY, obstacleMaxY);

			//	If the first obstacle is rearranged
			if (obstacleIndex == 0)
			{
				//	Position it distanceX amount of units to the right of the last element in the array. 
				obstacles[obstacleIndex].position = new Vector3(obstacles[obstacles.Length - 1].position.x + distanceX, randomY, obstacles[obstacleIndex].position.z);
			}
			//	If any other obstacle is rearranged
			else
			{
				//	Position it distanceX amount of units to the right of its previous element in the array. 
				obstacles[obstacleIndex].position = new Vector3(obstacles[obstacleIndex - 1].position.x + distanceX, randomY, obstacles[obstacleIndex].position.z);
			}

			// Increase the index by 1, so next time the Update loop happens the next element will be moved.
			obstacleIndex++;

			//	If the index reached a value equal to the array's length, i.e 1 more than the last element.
			if (obstacleIndex == obstacles.Length)
			{
				//	Then we want to access the 1st element again.
				obstacleIndex = 0;
			}
		}
	}
}
