/*
using UnityEngine;
using System.Collections;

public class item_pickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}


	// Trigger for knight piece item pickup
	void OnTriggerEnter (Collider other) {
		if (other.GetComponent<Collider>().tag == "Player") {
			print ("Item picked up");
			//send the player a message saying they picked an item up
			other.transform.SendMessage("item_pickup",this.gameObject);
			// destroy item
			Destroy (this.gameObject);

		}
	}
}
*/
