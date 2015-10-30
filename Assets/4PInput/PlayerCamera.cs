using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	int playerNumber = 0;
	Camera_Follow camScript = null;
	
	void Awake () {
	camScript = this.GetComponent<Camera_Follow>();
	}
	
	public void SetTarget (GameObject go){
	camScript.SetTarget(go);
	}
}
