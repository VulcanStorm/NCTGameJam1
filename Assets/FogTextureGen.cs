using UnityEngine;
using System.Collections;

public class FogTextureGen : MonoBehaviour {
	
	public static FogTextureGen singleton;
	
	public Material fogMat;
	public Texture2D fogTex;
	public Transform fogPlane;
	// a value of 1 means 1 pixel for every 1 map tile item
	public int pixelToWorldRatio = 2;
	public int fogHeight = 5;
	
	
	void Awake () {
		singleton = this;
	}
	
	void OnDestroy(){
		singleton = null;
	}
	
	// Use this for initialization
	void Start () {
	}
	
	public void CreateNewFogTexture() {
		
		
		
		int texWidth = WorldController.singleton.worldSizeX;
		int texHeight = WorldController.singleton.worldSizeY;
		
		// position the fog plane now
		fogPlane.position = new Vector3(texWidth/2,fogHeight, texHeight/2);
		fogPlane.localScale = new Vector3(texWidth,texHeight,1);
		
		texWidth /= pixelToWorldRatio;
		texHeight /= pixelToWorldRatio;
		
		
		
		fogTex = new Texture2D(texWidth,texHeight,TextureFormat.Alpha8,false);
		fogTex.name = "RTFogTexture";
		fogTex.wrapMode = TextureWrapMode.Clamp;
		
		Color[] colors = new Color[fogTex.height];
		// create some wierd ass pattern
		for(int i=0;i<fogTex.width;i++){
			colors[0].a = Mathf.Sin (i * Mathf.Deg2Rad);
			for(int n=0;n<colors.Length;n++){
			colors[n].a = colors[0].a;
			}
			fogTex.SetPixels(i,0,1,fogTex.height-1,colors);
		}
		
		
		SetFogTexture();
		
		
	}
	
	
	void SetFogTexture () {
		// TODO, update this
		fogTex.Apply();
		fogMat.mainTexture = fogTex;
	}
	// Update is called once per frame
	void FixedUpdate () {
		ChangeFogTexture ();
	}
	
	void ChangeFogTexture () {
		Color[] colors = new Color[fogTex.height];
		for(int i=0;i<fogTex.width;i++){
			colors[0].a = Mathf.Sin ((i*Mathf.Rad2Deg + Time.time));
			for(int n=0;n<colors.Length;n++){
				colors[n].a = colors[0].a;
			}
			fogTex.SetPixels(i,0,1,fogTex.height-1,colors);
		}
		SetFogTexture();
	}
}
