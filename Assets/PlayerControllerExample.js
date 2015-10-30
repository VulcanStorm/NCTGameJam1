#pragma strict
/*
 * 
 * FOR AARON TO USE
 * WILL NEED TO BE REWRITTEN IN C#
 */

// reference to rigidbody component
private var thisRigidbody : Rigidbody = null;

// WILL BE MOVED TO ANOTHER INPUT CLASS
// names of axes used for input
public var walkAxis : String = "Vertical";
public var strafeAxis : String = "Horizontal";

public var moveSpeed : float = 4;

// variables to store the result of the input
private var walkInput : float = 0;
private var strafeInput : float = 0;

// vector to store movement input
private var moveVector : Vector3;

// called when the object first starts
function Start () {
	// get a reference to the rigidbody
	thisRigidbody = this.GetComponent(Rigidbody);
}

// called every frame
function Update () {
	// get the input
	walkInput = Input.GetAxis(walkAxis);
	strafeInput = Input.GetAxis(strafeAxis);
	// set the input to the movement vector
	moveVector.x = strafeInput;
	moveVector.z = walkInput;
}

// called a fixed number of times per second
function FixedUpdate () {
	// normalise the movement vector, so you dont run faster diagonally
	// makes it have a length of 1
	moveVector.Normalize();
	
	// now make it move at the required speed per second
	moveVector = (moveVector * moveSpeed * Time.deltaTime);
	
	// move the rigidbody by the movement Vector
	thisRigidbody.MovePosition(thisRigidbody.position + moveVector);
}