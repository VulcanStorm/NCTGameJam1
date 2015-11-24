using UnityEngine;
using System.Collections;

public class item_return_hunter : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			player_carry plScript = other.GetComponent<player_carry>();
			if(plScript.is_carrying == true && plScript.is_hunter == true){
				score_manager.singleton.hunter_return ();
				plScript.ReturnItem ();
			}
		}
	}
}
