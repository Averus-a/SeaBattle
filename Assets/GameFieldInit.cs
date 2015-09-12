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
public class Cartesian{
	int x;
	int y;
	public Cartesian(int a, int b)
	{
			this.X = a;
			this.Y = b;

	}
	public int X {
		get{ return x; }
		set
		{ if(value<0) x=0;
			else if(value>9) x=9;
			else x = value;
		}
	}
	public int Y {
		get{ return y; }
		set
		{ if(value<0) y=0;
			else if(value>9) y=9;
			else y = value;
		}
	}
}