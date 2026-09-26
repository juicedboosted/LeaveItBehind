using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Npc : MonoBehaviour
{
	[SerializeField] public float interactionRadius;
	[SerializeField] public KeyCode interactionKey = KeyCode.E;

	[SerializeField] private float letterDelay;
	[SerializeField] private List<string> textContent;
	[SerializeField] private bool canBeTalkedToMultipleTimes = true;

	[SerializeField] private TMP_Text text;

	public bool CurrentlyTyping { get; private set; }
	private bool talkedToBefore;
	private int textIndex = 0;
	private int letterIndex = 0;

	private float lastTimeLetterTyped;

	private void Update()
	{
		// Check for if we're typing
		if (CurrentlyTyping) Type();

		// Check for if we're already been interacted with
		if (canBeTalkedToMultipleTimes == false && talkedToBefore) return;

		// Check for if the player interacts with us
		bool withinDistance = Vector2.Distance(GameManager.Player.transform.position, transform.position) <= interactionRadius;
		if ((withinDistance && Input.GetKey(interactionKey)) == false) return;

		// Begin/start typing
		BeginTyping();
	}

	public void BeginTyping()
	{
		CurrentlyTyping = true;
		
		lastTimeLetterTyped = 0f;
		textIndex = 0;
		letterIndex = 0;
		text.text = "";
		
		talkedToBefore = true;
	}

	private void Type()
	{
		// Check for if we can move to the next letter
		float elapsedTime = Time.time - lastTimeLetterTyped;
		if (elapsedTime < letterDelay) return;
		lastTimeLetterTyped = Time.time;

		// Add the letter
		text.text += textContent[textIndex][letterIndex];
		letterIndex++;

		// Check for if we've got to move to the next paragraph
		if (letterIndex >= textContent[textIndex].Length)
		{
			// 'Reset' the 'carriage'
			textIndex++;
			letterIndex = 0;
			text.text += "\n";
		}

		// Check for if we've finished
		if (textIndex >= textContent.Count)
		{
			CurrentlyTyping = false;
			return;
		}
	}

	//! debug stuff
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(transform.position, interactionRadius);
	}
}
