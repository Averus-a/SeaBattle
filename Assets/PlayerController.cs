
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Transform OverviewCamera;

	//for Sensetivity scrolls = 1
	public float scaleSensitivity = 5;

	float deltaX,deltaY,deltaScale;
	float cachedX,cachedY,cachedScale,cachedTime;
	public float speed;

	// Use this for initialization
	void Start () {
		speed = 100f;
	}
	
	// Update is called once per frame
	void Update () {

			//Reading Player input
			deltaX = Input.GetAxis ("Horizontal") * Time.deltaTime*speed;
			deltaY = Input.GetAxis ("Vertical") * Time.deltaTime*speed;
			deltaScale = Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime*speed*scaleSensitivity;
			cachedX += deltaX;
			cachedY += deltaY;
			cachedScale += deltaScale;
			if (isAnyKeyDown()) {
				cachedTime += Time.deltaTime;
			Debug.Log(cachedTime);
			}
			OverviewCamera.Translate (deltaX,deltaY,deltaScale);
			if (isStatic ()) {
				StartCoroutine (SmoothMove (deltaX, deltaY, deltaScale, cachedTime));
				cachedX = cachedY = cachedScale = cachedTime = 0;
		}
	}

	//smooth coroutine
	//last for 2 seconds(50 tics)
	//total smooth moving is impact from pressing button divide by 5;
	//divider count from arithmetical progression N*(N+1)/2, N=tics;
	IEnumerator SmoothMove(float impactX,float impactY, float impactScale,float Timing) {
		float f;
		Debug.Log ("Time is " + Timing);
		for ( f = Timing*50f; f >0; f -= 1f){
				OverviewCamera.Translate (impactX*f/10,impactY*f/10,impactScale*f/10);

				//Debug
				//Debug.Log("X:"+ impactX/divider*f*Timing);
				yield return new WaitForSeconds(0.02f); //so slow. Need to find other solution;
			}
		}
	public static bool isStatic(){
		return Input.GetButtonUp ("Horizontal") || Input.GetButtonUp ("Vertical") || Input.GetButtonUp ("Mouse ScrollWheel");
	}
	public static bool isAnyKeyDown(){
		return Input.GetButton("Horizontal") || Input.GetButton("Vertical") || Input.GetButton("Mouse ScrollWheel");
	}
}
