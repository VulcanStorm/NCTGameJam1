using UnityEngine;
using System.Collections;

public class FogTextureGen : MonoBehaviour {
	
	public static FogTextureGen singleton;
	
	public Material vampireFogMat;
	public Material hunterFogMat;
	public Texture2D vampireFogTex;
	public Texture2D hunterFogTex;
	public Transform fogPlane;
	// a value of 1 means 1 pixel for every 1 map tile item
	public int pixelToWorldRatio = 2;
	public int fogHeight = 5;
	
	byte[] fogData;
	byte[] vampireFogData;
	byte[] hunterFogData;
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
		// vampires
		vampireFogTex = new Texture2D(texWidth,texHeight,TextureFormat.Alpha8,false);
		vampireFogTex.name = "VampFogTexture";
		vampireFogTex.wrapMode = TextureWrapMode.Clamp;
		// hunters
		hunterFogTex = new Texture2D(texWidth,texHeight,TextureFormat.Alpha8,false);
		hunterFogTex.name = "HuntFogTexture";
		hunterFogTex.wrapMode = TextureWrapMode.Clamp;
		
		// create the byte array for the raw texture data
		fogData = new byte[texWidth*texHeight];
		vampireFogData = new byte[texWidth*texHeight];
		hunterFogData = new byte[texWidth*texHeight];
		
		Color[] colors = new Color[vampireFogTex.height];
		// create some wierd ass pattern
		for(int i=0;i<vampireFogTex.width;i++){
			colors[0].a = Mathf.Sin (i * Mathf.Deg2Rad);
			for(int n=0;n<colors.Length;n++){
			colors[n].a = colors[0].a;
			}
			vampireFogTex.SetPixels(i,0,1,vampireFogTex.height-1,colors);
		}
		
		
		SetFogTextures();
		StopCoroutine(FogTextureUpdateRoutine());
		StartCoroutine(FogTextureUpdateRoutine());
	}
	
	
	void SetFogTextures () {
		// TODO, update this
		vampireFogTex.Apply();
		vampireFogMat.mainTexture = vampireFogTex;
		hunterFogTex.Apply();
		hunterFogMat.mainTexture = hunterFogTex;
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
			//Debug.Log ("UpdatingFogTexture");
			UpdateFogTexture();
			
		}
	}
	
	void UpdateFogTexture (){
		
		short totalPixelFog = 0;
		short totalVampPixelFog = 0;
		short totalHunterPixelFog = 0;
		byte avgPixelFog = 0;
		byte avgVampPixelFog = 0;
		byte avgHunterPixelFog = 0;
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
				totalVampPixelFog = 0;
				totalHunterPixelFog = 0;
				
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
						totalVampPixelFog += WorldController.singleton.world[i,j].vampireFog;
						totalHunterPixelFog += WorldController.singleton.world[i,j].hunterFog;
					}
				}
				
				//totalPixelFog = WorldController.singleton.world[i,j].fog;
				
				// divide this by the amount of squares we used, to get an average
				totalPixelFog /= (short)(pixelToWorldRatio*pixelToWorldRatio);
				totalVampPixelFog /= (short)(pixelToWorldRatio*pixelToWorldRatio);
				totalHunterPixelFog /= (short)(pixelToWorldRatio*pixelToWorldRatio);
				
				// set this average fog back to the pixel
				avgPixelFog = (byte)totalPixelFog;
				avgVampPixelFog = (byte)totalVampPixelFog;
				avgHunterPixelFog = (byte)totalHunterPixelFog;
				
				// determine where in the array the pixel is...
				fogDataIndex = u+(v*texHeight);
				fogData[fogDataIndex] = avgPixelFog;
				vampireFogData[fogDataIndex] = avgVampPixelFog;
				hunterFogData[fogDataIndex] = avgHunterPixelFog;
			}
		}
		
		
		
		// apply this new source data to the textures
		vampireFogTex.LoadRawTextureData(vampireFogData);
		hunterFogTex.LoadRawTextureData(hunterFogData);
		SetFogTextures();
	}
	
	static byte b;
	
	byte BoundFog (int num){
		if(num < 0){
			b=0;
		}
		else if(num > 255){
			b=255;
		}
		else{
			b=(byte)num;
		}
		return b;
	}
	
	void ChangeFogTexture () {
		Color[] colors = new Color[vampireFogTex.height];
		for(int i=0;i<vampireFogTex.width;i++){
			colors[0].a = Mathf.Sin ((i + Time.time));
			for(int n=0;n<colors.Length;n++){
				colors[n].a = colors[0].a;
			}
			vampireFogTex.SetPixels(i,0,1,vampireFogTex.height-1,colors);
		}
		SetFogTextures();
	}
}
