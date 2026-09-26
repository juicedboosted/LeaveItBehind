using System;
using UnityEngine;

public class Curse : MonoBehaviour
{
	[SerializeField] private new BoxCollider2D collider;

	[SerializeField] private float floatingAmount;
	[SerializeField] private float floatingSpeed = 1f;

	[SerializeField] private float speed;

	public GameObject Victim;

	private Vector2 newPosition;
	private Vector2 floatingPosition;

	private void Update()
	{
		newPosition = transform.position;

		FollowVictim();
		FloatInPlace();

		transform.position = newPosition + floatingPosition;
	}

	private void FollowVictim()
	{
		if (Victim == null) return;

		Vector2 directionToVictim = ((Vector2)Victim.transform.position - newPosition).normalized;
		newPosition += (directionToVictim * speed) * Time.deltaTime;
	}

	private void FloatInPlace()
	{
		// Only float if we're not following a victim
		// TODO: Maybe still float if we're on the victim and haven't got to move
		if (Victim != null) return;

		floatingPosition.y = MathF.Sin(Time.time * floatingSpeed) * floatingAmount;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") == false) return;
		Victim = collision.gameObject;
		Debug.Log(Victim + " is cursed");
	}
}
