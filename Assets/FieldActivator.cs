using UnityEngine;
using System.Collections;

public class FieldActivator{

	private int[,] PlayerField;
	private BoolMatrix Vision;

	public void initFieldsVal(){

		Cartesian Cell;
		this.PlayerField = new int[10, 10];

		//Initialize visible and collision matrix
		this.Vision = new BoolMatrix ();
		Vision.initMatrix (10, 10);

		BoolMatrix OccupPositions = new BoolMatrix ();
		OccupPositions.initMatrix (10, 10);

		for (int size=4; size>0; size--) {
			int num = 5 - size;
			for (int i=0; i<num; i++) {
				bool isPlaced = true;
				do {
				Cell = new Cartesian(Random.Range(0,10),Random.Range(0,10));
				if (OccupPositions.isFreeCell (Cell, size)) {
						isPlaced = false;
					}	
				}while(isPlaced);
			}
		}
	}

	// Use this for initialization
	void Start () {

    }
	// Update is called once per frame
	void Update () {
		
	}
}