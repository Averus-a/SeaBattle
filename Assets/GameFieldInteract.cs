using UnityEngine;
using System.Collections;

public class GameFieldInteract : MonoBehaviour {
	// Use this for initialization
	public Collider coll;
	void Start () {
		coll = GetComponent<Collider>();
		FieldActivator playerFieldObj = new FieldActivator ();
		FieldActivator enemyFieldObj = new FieldActivator ();
		playerFieldObj.initFieldsVal ();
		enemyFieldObj.initFieldsVal ();
	}

	// Update is called once per frame
	void Update () { 
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
		if (coll.Raycast(ray, out hit, 1000.0F))
				Debug.Log (hit.point.x +" "+ hit.point.z);
		}

	}
	void OnMouseDown(){
		Vector3 clickedPosition =  Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
		Debug.Log ("Is clicked" + clickedPosition);
	}
}

public class ShipArmy {
	int[] Coord;
	int axis;
	bool isVertical;
}