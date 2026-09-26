using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] private float visionDistance;
	[SerializeField] private float attackDistance;

	[Tooltip("one in x chance to attack (2 is 50%)")] [SerializeField] private int chanceToAttack;
	[SerializeField] private float attackCooldown;
	[SerializeField] private int maxMissesInARow;
	private float lastTimeAttacked = 0f;
	private int missesInARow;

	[SerializeField] private float minAttackDamage;
	[SerializeField] private float maxAttackDamage;
	private float AttackDamage => Random.Range(minAttackDamage, maxAttackDamage + 1f);

	[SerializeField] private new Rigidbody2D rigidbody;
	[SerializeField] private float speed;

	public float DistanceToPlayer => Vector3.Distance(GameManager.Player.transform.position, transform.position);

	private void Update()
	{
		Attack();
	}

	private void FixedUpdate()
	{
		// Move towards the player
		rigidbody.linearVelocityX = speed * GetMovementInput();
	}

	//? ik this isn't movement input from like a keyboard but idk what to call it ok
	private float GetMovementInput()
	{
		// Check for if the player is within the enemies vision/not too close
		// TODO: If we're inside the attack distance then move out of it
		if (DistanceToPlayer > visionDistance || DistanceToPlayer <= attackDistance) return 0f;
		return Mathf.Sign(GameManager.Player.transform.position.x - transform.position.x);
	}

	private void Attack()
	{
		// The player must be within the attack range to be attacked
		if (DistanceToPlayer > attackDistance) return;

		// Attacks are every x seconds
		float elapsedTime = Time.time - lastTimeAttacked;
		if (elapsedTime < attackCooldown) return;
		lastTimeAttacked = Time.time;

		// One in x chance to attack
		bool shouldAttack = Random.Range(1, chanceToAttack + 1) == 1;
		if (shouldAttack == false && missesInARow < maxMissesInARow)
		{
			missesInARow++;
			return;
		}
		missesInARow = 0;

		// Damage the player
		GameManager.Player.GetComponent<PlayerVitality>().Health -= AttackDamage;
	}

	//! debug stuff for seeing the distances
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, visionDistance);

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackDistance);
	}
}
