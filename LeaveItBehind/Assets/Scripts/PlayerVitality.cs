using System;
using TMPro;
using UnityEngine;

public class PlayerVitality : MonoBehaviour
{
	public bool Dead { get; private set; } = false;
	public float MaxHealth = 100f;

	private float health;
	public float Health
	{
		get => health;
		set
		{
			health = value;
			health = Math.Clamp(health, 0, MaxHealth);
			if (health <= 0) Die();

			healthText.text = $"{health:F0}/{MaxHealth} (TODO: add a nice health bar thing instead of this)";
		}
	}

	[SerializeField] private TMP_Text healthText;

	private void Awake() => Health = MaxHealth;

	public void Die()
	{
		// Can't die twice
		if (Dead == true) return;

		Debug.Log("Dead");
		Dead = true;
	}
}
