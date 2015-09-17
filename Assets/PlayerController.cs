
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Transform OverviewCamera;

	//for Sensetivity scrolls = 1
	public float scaleSensitivity = 5;

	float deltaX,deltaY,deltaScale;
	float cachedScale,cachedTime;
	public float speed;
	bool isRunning = false;

	//To pass instance to StartCoroutine not directly
	Coroutine smoothHelper;

	// Use this for initialization
	void Start () {
		speed = 100f;
		smoothHelper = null;
	}
	
	// Update is called once per frame
	void Update () {

			//Reading Player input
			//Button down time
			if (isAnyKeyDown()) {
				deltaX = Input.GetAxis ("Horizontal") * Time.deltaTime*speed;
				deltaY = Input.GetAxis ("Vertical") * Time.deltaTime*speed;
				deltaScale = Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime*speed*scaleSensitivity;
				cachedScale += deltaScale;
				cachedTime += Time.deltaTime;
				OverviewCamera.Translate (deltaX,deltaY,deltaScale);
			}
			
			
			
			if (isRunning && isAnyKeyDown ()) {
			//Debug.Log ("STOP COROUTINE RIGHT THERE");
			StopCoroutine (smoothHelper);
		}
			if (isCameraControlEnded()) {
				smoothHelper = StartCoroutine (SmoothMove (deltaX, deltaY, deltaScale, 2*cachedTime));
				cachedScale = cachedTime = 0;

			}
		}


	//smooth coroutine
	//f/Timing - decresing multiplier for impacts
	IEnumerator SmoothMove(float impactX,float impactY, float impactScale,float Timing) {
		float f, m;
		//Debug.Log ("Time is " + Timing/2);
		float frameLength = Time.deltaTime;
		//100: smooth time = move time x2
		//float timeTicsCount = Timing * 100f;

		isRunning = true;
		for ( f = Timing; f >0; f -= Time.deltaTime){
			frameLength = Time.deltaTime;
			m = f/Timing;
			OverviewCamera.Translate (impactX*m,impactY*m,impactScale*m);

				yield return null;
			}
		isRunning = false;
		//Debug.Log ("ENDED");
		}
	public static bool isCameraControlEnded(){
		return Input.GetButtonUp ("Horizontal") || Input.GetButtonUp ("Vertical") || Input.GetButtonUp ("Mouse ScrollWheel");
	}
	public static bool isAnyKeyDown(){
		return Input.GetButton ("Horizontal") || Input.GetButton ("Vertical") || Input.GetAxis("Mouse ScrollWheel")!=0;			
	}
}
