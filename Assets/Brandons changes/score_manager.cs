using UnityEngine;
using System.Collections;

public class score_manager : MonoBehaviour {

	public static score_manager singleton = null;
	public int remaining_pieces = 10;
	public int vampire_score = 0;
	public int hunter_score = 0;
	
	public bool gameOver = false;
	int screenWidth = 0;
	int screenHeight = 0;

	void Awake () {
		singleton = this;
	}

	void OnDestroy () {
		singleton = null;
	}
	
	void Start(){
		screenWidth = Screen.width;
		screenHeight = Screen.height;
	}
	
	void Update () {
		
	}

	void OnGUI () {
		if(gameOver == true){
			GUI.Box(new Rect((screenWidth/2)-150,(screenHeight/2)-40,300,80), "!GAME OVER!");
			GUI.Box (new Rect((screenWidth/2)-150,(screenHeight/2)+40,150,25),"Vampires Score: "+vampire_score);
			GUI.Box (new Rect((screenWidth/2),(screenHeight/2)+40,150,25),"Hunters Score: "+hunter_score);
		}
	}
	
	void CheckForVictory(){
		if (remaining_pieces == 0) {
			// KILL THE GAME
			print("Game over, all pieces collected.");
			gameOver = true;
			GameManager.singleton.GameOver();
		}
	}
	
	public void vampire_return () {
		remaining_pieces -= 1;
		vampire_score += 10;
		CheckForVictory();
	}

	public void hunter_return () {
		remaining_pieces -= 1;
		hunter_score += 10;
		CheckForVictory();
	}
}
