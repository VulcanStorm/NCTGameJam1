using UnityEngine;
using System.Collections;

public class item_return_vampire : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			player_carry plScript = other.GetComponent<player_carry>();
			if(plScript.is_carrying == true && plScript.is_vampire == true){
				score_manager.singleton.vampire_return ();
				plScript.ReturnItem ();
			}
		}
	}
}
