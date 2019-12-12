using UnityEngine;

/// <summary>
/// The purpose of this script is to do all actions related to the player's movement, animations, sounds
/// </summary>
public class Player : MonoBehaviour
{
	//	We declare the components that we will use for the player.
	private Animator animator;  //	The Animator component
	private AudioSource audioSource;    //	The AudioSource component
	private Rigidbody2D rb2D;   //	The RigidBody2D component

	//  The SerializeField is used to expose it in the editor even though it is a private variable.
	//  The values are not set in this script but in the Inspector
	[SerializeField] private Vector2 force; // The force that is applied when pressing the mouse button.
	[SerializeField] private float rotationMultiplier;  //	The value that we use to control how much the player shoudl rotate while moving.
	[SerializeField] private float playerMaxY;  //	Used to limit the player from flying off scrren.

	// Start is called before the first frame update
	private void Start()
	{
		//	Getting the respective components from the Player's GameObject
		rb2D = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponent<Animator>();
		audioSource = gameObject.GetComponent<AudioSource>();
	}


	// Update is called once per frame
	private void Update()
	{
		//  If the player is dead, the game session has ended
		//  In that case we want everything to stop moving
		//  And so, we don't want to execute any code that would move it
		//  The return command makes the code not execute any further and return to the beginning of the Update loop.
		if (GameManager.Instance.playerIsDead == true)
		{
			//	Before it returns, play the death animation.
			animator.SetBool("Death", true);
			return;
		}

		//	If the left mouse button is pressed.
		//	On mobile the same line of code checks for a tap.
		if (Input.GetMouseButtonDown(0))
		{
			//	Play the audioclio that is set in the AudioSource
			audioSource.Play();
			//	Play the Flap animation
			animator.SetTrigger("Flap");

			//	First zero out the velocity that has accumulated.
			rb2D.velocity = Vector2.zero;
			//	Then add force to move the player
			rb2D.AddForce(force);
		}

		//	We don't want the player to be able to fly over the screen
		//	The Mathf.Clamp function allows us to set a minimum and maximum that transform.position.y can be.
		//	In our case we don't want to limit the minimum because the player will collide with the ground.
		float positionY = Mathf.Clamp(transform.position.y, -Mathf.Infinity, playerMaxY);
		//	Setting the Player's position using the clamped value.
		transform.position = new Vector2(transform.position.x, positionY);

		//	Changing the rotation(euler Angles). The only axis that should change is the z
		//	We can use the velocity on the y since it's quite useful.
		//	When the player moves upwards the velocity is positive while when it's falling negative.
		//	This is the basic idea for the rotation as well.
		transform.eulerAngles = new Vector3(0, 0, rb2D.velocity.y * rotationMultiplier);
	}


}
