using UnityEngine;
using System.Collections;

public class CameraViewLayers : MonoBehaviour {
	
	// layers to view
	public LayerMask vampireViewLayers;
	public LayerMask hunterViewLayers;
	// layers for fog
	public LayerMask vampireFogLayers;
	public LayerMask hunterFogLayers;
	
	public Rect viewRect;
	public int viewDepth;
	public int fogDepth;
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
	
	public void SetViewRect(Rect vRect, int vDepth, int fDepth){
		// store the values
		viewRect = vRect;
		viewDepth = vDepth;
		fogDepth = fDepth;
		
		// set the values
		viewCam.rect = viewRect;
		fogCam.rect = viewRect;
		viewCam.depth = vDepth;
		fogCam.depth = fDepth;
	}
}
