using UnityEngine;
using System.Collections;

public class TDCameraTarget : MonoBehaviour {

	Transform thisTransform;


	// mouse input variables
	public int horizontalScrollWidth = 10;
	public int verticalScrollWidth = 10;
	public float scrollSpeed = 30;
	public float rotSpeed = 90;
	public Vector3 positionOffset = new Vector3(0,0.1f,0);
	float doRotate = 0;
	float rotInput = 0;
	public bool isRotating = false;

	// screen dimension variables
	Vector2 screenDimension;
	float[] boundaries;

	public int worldFloor = -1;
	public int worldCeiling = 100;
	public LayerMask rayLayers;
	int rayDist = 0;
	Vector3 movePos;
	Vector3 raycastPos;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
		thisTransform = this.transform;
		CalcBoundaries ();
	}
	
	// Update is called once per frame
	void Update () {
		CalcBoundaries();
	}

	void CalcBoundaries () {

		// create the boundaries away
		boundaries = new float[4];

		// get the screen dimensions
		screenDimension.x = Screen.width;
		screenDimension.y = Screen.height;

		// left, right, up down
		boundaries [0] = horizontalScrollWidth;
		boundaries [1] = screenDimension.x - horizontalScrollWidth;
		boundaries [2] = verticalScrollWidth;
		boundaries [3] = screenDimension.y - verticalScrollWidth;


	}

	void CheckMouseInput () {

		doRotate = Input.GetAxis("Fire2");
		rotInput = Input.GetAxis ("Mouse X");
		movePos = Vector3.zero;
		if (doRotate == 1) {
			isRotating = true;
		} else {
			isRotating = false;
		}



		if (isRotating == true) {
			thisTransform.Rotate(Vector3.up * rotInput * rotSpeed * Time.deltaTime, Space.Self);
		}
		else{
			movePos.x  = Input.GetAxis("Horizontal");
			movePos.z  = Input.GetAxis("Vertical");
			
			
			Vector2 mousePos = Input.mousePosition;

			if (mousePos.x < boundaries [0]) {
				movePos.x = -1;
			} else if (mousePos.x > boundaries [1]) {
				movePos.x = 1;
			}

			if (mousePos.y < boundaries [2]) {
				movePos.z = -1;
			} else if (mousePos.y > boundaries [3]) {
				movePos.z = 1;
			}

			movePos *= scrollSpeed * Time.deltaTime;
		}
	}

	void FixedUpdate () {
		CheckMouseInput ();
		movePos = thisTransform.TransformDirection (movePos);
		raycastPos = thisTransform.position + movePos;
		raycastPos.y = worldCeiling;
		rayDist = worldCeiling - worldFloor;
		if (Physics.Raycast (raycastPos, Vector3.down, out hit, rayDist, rayLayers)) {
			thisTransform.position = hit.point+positionOffset;
		}
	}
}
