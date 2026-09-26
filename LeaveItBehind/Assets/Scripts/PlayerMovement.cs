using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	[Header("Movement")]
	public float m_moveSpeed;
	public float m_jumpForce;

	[Header("Ground Check")]
	[SerializeField] private Transform groundCheck;
	[SerializeField] private LayerMask groundLayer;

	//[SerializeField] private SpriteRenderer Sprite;
	//[SerializeField] private Sprite WalkingLeftSprite;
	//[SerializeField] private Sprite WalkingRightSprite;
	//[SerializeField] private Sprite StandingStillSprite;

	[SerializeField] private new Rigidbody2D m_rigidBody;

	private Animator m_animator;
	private SpriteRenderer m_spriteRenderer;

	private float m_moveInput;
	private bool m_isGrounded;


	// Check for if our feet are touching the ground layer
	private bool OnTheGroundRn => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);


	private void Awake()
	{
		m_rigidBody = GetComponent<Rigidbody2D>();
		m_animator = GetComponent<Animator>();
		m_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		// Get movement input
		m_moveInput = Input.GetAxisRaw("Horizontal");

		// Jump
		// TODO: Put this in fixed update maybe
		if (Input.GetButtonDown("Jump") && OnTheGroundRn) m_rigidBody.linearVelocity = Vector2.up * m_jumpForce;

        UpdateAnimations();
        HandleSprite();
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		m_rigidBody.linearVelocity = new Vector2(m_moveInput * m_moveSpeed, m_rigidBody.linearVelocity.y);
	}

	private void HandleSprite()
	{
		//flips sprite
		if (m_moveInput > 0)
		{
			m_spriteRenderer.flipX = false;
		}
		else if (m_moveInput < 0)
		{
			m_spriteRenderer.flipX = true;
		}
	}

	/// <summary>
	/// Updates animation parameters using player movement
	/// </summary>
	private void UpdateAnimations()
	{
		m_animator.SetFloat("Speed", Mathf.Abs(m_moveInput));
		m_animator.SetFloat("YVelocity", m_rigidBody.linearVelocity.y);
		m_animator.SetBool("IsGrounded", m_isGrounded);
	}
}
