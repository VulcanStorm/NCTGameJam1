using UnityEngine;
using System.Collections;

public class player_carry : MonoBehaviour {

	public bool is_vampire = false;
	public bool is_hunter = true;

	public bool is_carrying = false;
	public Transform pieceHolder = null;

	item_pickup heldItem = null;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PickupItem(item_pickup item){
		is_carrying = true;
		heldItem = item;
	}

	public void DropItem(){
		if (is_carrying == true) {
			is_carrying = false;
			heldItem.DropPiece ();
			heldItem = null;
		}
	}

	public void ReturnItem () {
		if (is_carrying == true) {
			is_carrying = false;
			heldItem.DropPiece ();
			heldItem.CapturePiece();
			heldItem = null;
		}
	}
}
