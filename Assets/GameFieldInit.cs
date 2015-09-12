using UnityEngine;
using System.Collections;

public class GameFieldInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		FieldActivator playerFieldObj = new FieldActivator ();
		FieldActivator enemyFieldObj = new FieldActivator ();
		playerFieldObj.initFieldsVal ();
		enemyFieldObj.initFieldsVal ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}

public class ShipArmy{
	int[] Coord;
	int axis;
	bool isVertical;
}