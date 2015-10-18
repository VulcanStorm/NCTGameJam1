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
	
	byte[] fogData;
	int texWidth;
	int texHeight;
	
	public float fogUpdateTime = 0.1f;
	bool doUpdateFog = true;
	
	
	
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
		
		
		// get the size for the texture
		texWidth = WorldController.singleton.worldSizeX;
		texHeight = WorldController.singleton.worldSizeY;
		
		// position the fog plane now
		// so it is stretched over the whole area
		fogPlane.position = new Vector3(texWidth/2,fogHeight, texHeight/2);
		fogPlane.localScale = new Vector3(texWidth,texHeight,1);
		
		// set the texture size based on the ratio
		texWidth /= pixelToWorldRatio;
		texHeight /= pixelToWorldRatio;
		
		
		// create the texture
		fogTex = new Texture2D(texWidth,texHeight,TextureFormat.Alpha8,false);
		fogTex.name = "RTFogTexture";
		fogTex.wrapMode = TextureWrapMode.Clamp;
		
		// create the byte array for the raw texture data
		fogData = new byte[texWidth*texHeight];
		
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
		StopCoroutine(FogTextureUpdateRoutine());
		StartCoroutine(FogTextureUpdateRoutine());
	}
	
	
	void SetFogTexture () {
		// TODO, update this
		fogTex.Apply();
		fogMat.mainTexture = fogTex;
	}
	// Update is called once per frame
	void FixedUpdate () {
		//UpdateFogTexture();
	}
	
	IEnumerator FogTextureUpdateRoutine () {
		while(doUpdateFog == true){
			yield return new WaitForSeconds(fogUpdateTime);
			// TODO REMOVE THIS
			WorldController.singleton.UpdateFog();
			Debug.Log ("UpdatingFogTexture");
			UpdateFogTexture();
			
		}
	}
	
	void UpdateFogTexture (){
		
		short totalPixelFog = 0;
		byte avgPixelFog = 0;
		int fogDataIndex = 0;
		int u=0;
		int v=0;
		int i=0;
		int j=0;
		int endi=0;
		int endj=0;
		// iterate over every pixel in the texture
		for(u=0;u<texWidth;u++){
			for(v=0;v<texHeight;v++){
				// get the all the world data that makes up this pixel
				// reset the variables
				totalPixelFog = 0;
				
				// get the starting world array location
				i= u*pixelToWorldRatio;
				j= v*pixelToWorldRatio;
				
				// determine the end of this square of locations
				endi = (u+1) * pixelToWorldRatio;
				endj = (v+1) * pixelToWorldRatio;
				
				// iterate over this square, and add up all the fog
				for(;i<endi;i++){
					for(;j<endj;j++){
						totalPixelFog += WorldController.singleton.world[i,j].fog;
					}
				}
				
				//totalPixelFog = WorldController.singleton.world[i,j].fog;
				
				// divide this by the amount of squares we used, to get an average
				totalPixelFog /= (short)(pixelToWorldRatio*pixelToWorldRatio);
				
				// set this average fog back to the pixel
				avgPixelFog = (byte)totalPixelFog;
				
				// determine where in the array the pixel is...
				fogDataIndex = u+(v*texHeight);
				fogData[fogDataIndex] = avgPixelFog;
			}
		}
		// apply this new source data to the texture
		fogTex.LoadRawTextureData(fogData);
		SetFogTexture();
	}
	
	void ChangeFogTexture () {
		Color[] colors = new Color[fogTex.height];
		for(int i=0;i<fogTex.width;i++){
			colors[0].a = Mathf.Sin ((i + Time.time));
			for(int n=0;n<colors.Length;n++){
				colors[n].a = colors[0].a;
			}
			fogTex.SetPixels(i,0,1,fogTex.height-1,colors);
		}
		SetFogTexture();
	}
}
