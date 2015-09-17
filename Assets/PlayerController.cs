
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Transform OverviewCamera;

	//for Sensetivity scrolls = 1
	public float scaleSensitivity = 10f;

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

		if(isAnyKeyInputed()) {
			ReadAxeInput (ref deltaX, ref deltaY,ref deltaScale);
			cachedTime += Time.deltaTime;					
			OverviewCamera.Translate (deltaX,deltaY,deltaScale);
			if (isRunning) {
				Debug.Log ("STOP COROUTINE RIGHT THERE");
				StopCoroutine (smoothHelper);
				isRunning = false;
			}
		}

		if (isCameraControlEnded()) {

			smoothHelper = StartCoroutine (SmoothMove (deltaX, deltaY, cachedScroll*scaleSensitivity, cachedTime));
			cachedTime = 0;
		}
		cachedScroll = deltaScale;
		deltaScale = 0;
	}


	//smooth coroutine
	//f/Timing - decresing multiplier for impacts
	IEnumerator SmoothMove(float impactX,float impactY, float impactScale,float Timing) {

		Debug.Log (Timing);
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

		Debug.Log ("ENDED Correctly");
	}
	public static bool isAnyKeyInputed(){
		return Input.GetButton ("Horizontal") || Input.GetButton ("Vertical") || Input.GetAxis ("Mouse ScrollWheel") != 0;			
	}

	public bool isCameraControlEnded(){

		return Input.GetButtonUp ("Horizontal") || Input.GetButtonUp ("Vertical") || ScrollEnded ();
	}

	private bool ScrollEnded(){

		if ((Input.GetAxis ("Mouse ScrollWheel") == 0) && (cachedScroll != 0))
			return true;
		return false;
	}

	public void ReadAxeInput(ref float x, ref float y, ref float z){

		x = Input.GetAxis ("Horizontal") * Time.deltaTime*speed;
		y = Input.GetAxis ("Vertical") * Time.deltaTime*speed;
		z = Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime*speed;	
	}
}