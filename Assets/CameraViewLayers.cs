using UnityEngine;
using System.Collections;

public class CameraViewLayers : MonoBehaviour {
	
	// layers to view
	public LayerMask vampireViewLayers;
	public LayerMask hunterViewLayers;
	// layers for fog
	public LayerMask vampireFogLayers;
	public LayerMask hunterFogLayers;
	
	public Camera viewCam = null;
	public Camera fogCam = null;
	
	
	// Use this for initialization
	public void SetLayerMaskToCameras (Team viewTeam) {
		
		// check which team this camera is for
		if(viewTeam == Team.Vampires){
			viewCam.cullingMask = vampireViewLayers;
			fogCam.cullingMask = vampireFogLayers;
		}
		else if(viewTeam == Team.Hunters){
			viewCam.cullingMask = hunterViewLayers;
			fogCam.cullingMask = hunterFogLayers;
		}
	}
}
