using UnityEngine;

/// <summary>
/// The purpose of this class is to detect collisions with the player. It should be added to all objects that the player can collide with.
/// It should be added to obstacles on the same gameObject that has a Collider2D.
/// </summary>
public class CollisionHandler : MonoBehaviour
{
	//	Whenever a collision happens
	private void OnCollisionEnter2D(Collision2D collision)
	{
		//	If the gameObject the collision happened with has the tag "Player" then the collision was with the Player.
		//	In our case nothing else can possibly collide with the obstacles but it is still a good practice to check.
		if (collision.gameObject.tag == "Player")
		{
			//	If a collision happened Kill the player
			GameManager.Instance.KillPlayer();
		}
	}
}
