using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerCamera))]

public class Camera_Follow : MonoBehaviour {
	public GameObject target = null;
	public Transform targetTransform = null;
	public float distance = 13f;
	public float height = 4f;
	
	float distSqrd = 0f;
	float distToTarget = 0f;
	Vector3 wantedPos = new Vector3();
	Vector3 vel = new Vector3();
	Vector3 lookPos = new Vector3();
	Transform thisTransform = null;
	
	public void SetTarget (GameObject t){
	target = t;
	targetTransform = target.transform;
	
	}
	
	// Use this for initialization
	void Start () {
	thisTransform = this.transform;
	if(target != null){
	targetTransform = target.transform;
		}
		distSqrd = distance * distance;
	}
	
	public void SetHitPoint (Vector3 pos){
	lookPos = pos;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	if(targetTransform != null){
	wantedPos = targetTransform.TransformPoint(0,height,-distance);
	thisTransform.position = Vector3.SmoothDamp(thisTransform.position,wantedPos,ref vel,0.5f);
	thisTransform.LookAt(lookPos);
		}
	}
}
