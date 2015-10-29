using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	
	public static Spawner singleton;
	
	void Awake () {
		singleton = this;
	}
	
	void OnDestroy () {
		singleton = null;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
