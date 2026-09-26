using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float Speed;
	public float JumpForce;

	[SerializeField] private SpriteRenderer Sprite;
	[SerializeField] private Sprite WalkingLeftSprite;
	[SerializeField] private Sprite WalkingRightSprite;
	[SerializeField] private Sprite StandingStillSprite;

	[SerializeField] private Transform groundCheck;
	[SerializeField] private new Rigidbody2D rigidbody;
	[SerializeField] private LayerMask groundLayer;

	private float horizontalInput;
	public Direction Direction => (Direction)horizontalInput;

	// Check for if our feet are touching the ground layer
	private bool OnTheGroundRn => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

	private void Update()
	{
		// Get movement input
		horizontalInput = Input.GetAxisRaw("Horizontal");

		// Jump
		// TODO: Put this in fixed update maybe
		if (Input.GetButtonDown("Jump") && OnTheGroundRn) rigidbody.linearVelocity = Vector2.up * JumpForce;

		// Face the correct way
		HandleSprite();
	}

	private void HandleSprite()
	{
		Sprite.sprite = Direction switch
		{
			Direction.Left => WalkingLeftSprite,
			Direction.Right => WalkingRightSprite,
			_ => StandingStillSprite,
		};
	}

	private void FixedUpdate()
	{
		// Move
		rigidbody.linearVelocityX = Speed * horizontalInput;
	}
}

public enum Direction
{
	Left = -1,
	None = 0,
	Right = 1
}