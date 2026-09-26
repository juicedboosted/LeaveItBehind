using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameObject player;
	public static GameObject Player => player;

	private void Awake()
	{
		player = FindFirstObjectByType<PlayerMovement>().gameObject;
	}
}
