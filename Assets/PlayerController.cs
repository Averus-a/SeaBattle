
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Transform OverviewCamera;

	//for Sensetivity scrolls = 1
	public float scaleSensitivity = 5;

	float deltaX, deltaY, deltaScale;
	float cachedTime, cachedScroll;
	public float speed;
	bool isRunning = false;

	//To pass instance to StartCoroutine not directly
	Coroutine smoothHelper;

	// Use this for initialization
	void Start () {
		smoothHelper = null;
	}
	
	// Update is called once per frame
	void Update () {

		//Reading Player input and any key down;
		//Button down time;
		//Test delta in condition block or in Update (directly) count performance;

		ReadAxeInput (ref deltaX, ref deltaY,ref deltaScale);

		if(isAnyInputed()) {
					cachedTime += Time.deltaTime;					
					OverviewCamera.Translate (deltaX,deltaY,deltaScale);
			}
			
			if (isRunning && isAnyInputed()) {
				Debug.Log ("STOP COROUTINE RIGHT THERE");
				StopCoroutine (smoothHelper);
		}
			if (isCameraControlEnded()) {
				smoothHelper = StartCoroutine (SmoothMove (deltaX, deltaY, deltaScale, 2*cachedTime));
				cachedScale = cachedTime = 0;

			}
		cachedScroll = deltaScale;
	}


	//smooth coroutine
	//f/Timing - decresing multiplier for impacts
	IEnumerator SmoothMove(float impactX,float impactY, float impactScale,float Timing) {

		Debug.Log ("Scroll impact is " + impactScale);

		float f, m;
		float frameLength = Time.deltaTime;
		isRunning = true;

		//better use Math.SmoothDamp ?
		for ( f = Timing; f >0; f -= frameLength){
			frameLength = Time.deltaTime;
			m = f/Timing;
			OverviewCamera.Translate (impactX*m,impactY*m,impactScale*m);

				yield return null;
			}
		isRunning = false;

		//Debug.Log ("ENDED");
	}
	public static bool isAnyKeyDown(){
		return Input.GetButton ("Horizontal") || Input.GetButton ("Vertical") || Input.GetAxis("Mouse ScrollWheel")!=0;			

	public bool isCameraControlEnded(){

		return Input.GetButtonUp ("Horizontal") || Input.GetButtonUp ("Vertical") || ScrollEnded ();
	
	}

	public bool isAnyInputed(){

		return deltaX!=0 || deltaY!=0 || deltaScale!=0;	

	}
	private bool ScrollEnded(){

		if ((Input.GetAxis ("Mouse ScrollWheel") == 0) && (cachedScroll != 0))
			return true;
		return false;
	}
	public void ReadAxeInput(ref float x, ref float y, ref float z){

		x = Input.GetAxis ("Horizontal") * Time.deltaTime*speed;
		y = Input.GetAxis ("Vertical") * Time.deltaTime*speed;
		z = Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime*speed*scaleSensitivity;	
	}
}