using UnityEngine;

/// <summary>
/// The purpose of this class is to rearrange the environment tiles endlessly
/// </summary>
public class RepeatingEnvironmentController : MonoBehaviour
{
	//	All SerializeField are used to expose a private variable to the Inspector. In that way we can set those values there.
	[SerializeField] private Transform[] environmentTiles; //	Gathering all the Environment Tiles' Transforms, so we can later rearrange them

	private SpriteRenderer spriteRenderer; //	We need to have a reference to any of the environment tile sprites to get the width of the tile

	private float environmentTileWidth; //	The width of an environment tile.
	private float environmentTileWidthRepositionOffset; //	How many units we want to move an environmet piece to tile it.


	// Start is called before the first frame update
	void Start()
	{
		//	The environment tiles are parented under this object.
		//	Looking for a sprite renderer component there. It will assign whichever it finds first.
		//	We don't mind in this case because the tiles are identical (prefabs) and all the sprites used have the same width
		spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();

		//	The width is the size of the sprite renderer on the X axis.
		environmentTileWidth = spriteRenderer.size.x;

		//	The amount of units that the tile will have to move is twice its own width.
		//	In that way it will snap exactly to the left edge of the tile that is on its right.
		environmentTileWidthRepositionOffset = environmentTileWidth * 2;
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

		//	Looping through all the environment Tiles
		foreach (Transform tile in environmentTiles)
		{
			//	If any of them has moved to the left one time its own width,
			//	then it has moved outside the rendered area based on our setup.
			if (tile.position.x <= -environmentTileWidth)
			{
				//	We want to position it to the right of the element that is now rendered
				//	It should move twice its own width to the right.
				tile.position = new Vector3(tile.position.x + environmentTileWidthRepositionOffset, tile.position.y, tile.position.z);
			}
		}
	}
}
