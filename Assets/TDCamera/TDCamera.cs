using UnityEngine;
using System.Collections;

public class TDCamera : MonoBehaviour {

	public Transform target = null;
	Transform thisTransform;

	Vector3 desiredRot;
	Vector3 desiredPos;
	Vector3 targetPos;
	Vector2 dist;

	public float distanceMultiplier = 3;

	public int desiredDistFromTarget = 10;
	public int desiredAngle = 30;

	public int maxZoom = 18;
	public int minZoom = 1;
	public int zoom = 10;

	public int zoomSpeed = 7;
	public int rotateSpeed = 120;
	public int scrollAmount = 0;

	// Use this for initialization
	void Start () {
		thisTransform = this.transform;
	}

	void Update () {
		scrollAmount = (int)Input.GetAxis ("Mouse ScrollWheel");

		zoom += scrollAmount;
		zoom = Mathf.Clamp (zoom, minZoom, maxZoom);

		desiredAngle = (5 * zoom);
		desiredDistFromTarget = zoom;

	}

	// Update is called once per frame
	void FixedUpdate () {

		targetPos = target.position;

		// calculate the desired distances
		dist.x = desiredDistFromTarget * distanceMultiplier * Mathf.Cos (desiredAngle * Mathf.Deg2Rad);
		dist.y = desiredDistFromTarget * distanceMultiplier * Mathf.Sin (desiredAngle * Mathf.Deg2Rad);

		// set the desired position around the world origin
		desiredPos.z = dist.x;
		desiredPos.y = dist.y;
		desiredPos.x = 0;

		// multiply it by the rotation
		Quaternion rot = Quaternion.LookRotation(-target.forward,Vector3.up);
		desiredPos = rot * desiredPos;

		// translate it to the target position
		desiredPos += targetPos;

		// calculate the deisred rotation
		desiredRot = target.eulerAngles;
		desiredRot.x = Mathf.Lerp(thisTransform.eulerAngles.x,desiredAngle, zoomSpeed * Time.deltaTime);
		//desiredRot.y = Mathf.LerpAngle (thisTransform.eulerAngles.y, target.eulerAngles.y, rotateSpeed * Time.deltaTime);


		thisTransform.position = Vector3.Lerp(thisTransform.position, desiredPos, zoomSpeed * Time.deltaTime);

		thisTransform.eulerAngles = desiredRot;
	}
}
