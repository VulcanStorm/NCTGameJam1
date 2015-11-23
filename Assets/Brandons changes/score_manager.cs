using UnityEngine;
using System.Collections;

public class score_manager : MonoBehaviour {

	public static score_manager singleton = null;
	public int remaining_pieces = 10;
	public int vampire_score = 0;
	public int hunter_score = 0;

	void Awake () {
		singleton = this;
	}

	void OnDestroy () {
		singleton = null;
	}

	void Update () {
		if (remaining_pieces == 0) {
			// KILL THE GAME
			print("Game over, all pieces collected.");
		}
	}

	public void vampire_return () {
		remaining_pieces -= 1;
		vampire_score += 10;
	}

	public void hunter_return () {
		remaining_pieces -= 1;
		hunter_score += 10;
	}
}
