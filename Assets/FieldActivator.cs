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
				Cell = new Cartesian(Random.Range(0,9),Random.Range(0,9));
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

public class BoolMatrix{

	private bool[,] logicMatrix;

	public void initMatrix(int size)
	{
		this.logicMatrix = new bool[size,size];
	}

	public void initMatrix(int sizex,int sizey){
		this.logicMatrix = new bool[sizex,sizey];
	}

	public bool isEmptyCell(Cartesian C){
		if(!this.logicMatrix[C.X,C.Y]){
			return true;
		}
		return false;
	}

	//Cell and area around
	public bool isFreeCell(Cartesian C, int size){

		int dir;
		Cartesian beg, end,send;
		bool[,] tempColl;
		if(!this.logicMatrix[C.X,C.Y]){
					
			//Check spare place for ship
			//Attention! Magic number further (9 - maximum matrix index)
			if((9-C.X)<size &&(9-C.Y)<size) return false;
				else if((9-C.X)<size) dir = 0;
					else if((9-C.Y)<size) dir = 1;
						else dir = Random.Range(0,1);
			//Debug info
			//Debug.Log(size+"ship attemps, coord - ("+ C.X+","+C.Y+") in dir "+ dir);

			// Transform origin ship coord into ship extreme positions;
			beg = new Cartesian(C.X-1,C.Y-1);

			if(dir == 0) {end = new Cartesian(C.X+1,C.Y+size);
				send = new Cartesian(C.X, C.Y+size);
			}
			else {end = new Cartesian(C.X+size,C.Y+1);
				  send = new Cartesian(C.X+size, C.Y);
			}

			// Prevent contact collisions
			tempColl = this.SelectArea(beg,end);

		for(int i=0; i<tempColl.GetLength(0);i++){
			for(int j=0; j<tempColl.GetLength(1);j++){
					if(tempColl[i,j]) return false;
			}
		}
			this.SetArea(C,send);
			Debug.Log(size+"ship placed, coord - ("+ C.X+","+C.Y+") in dir" + dir);
			return true;
		}
		else return false;
	}

	private bool[,] SelectArea(Cartesian areaB, Cartesian areaE){
		int cX, cY;
		cX = areaB.X;
		cY = areaB.Y;

		//Debug info
		//Debug.Log("Begin coordinates - ("+ cX+","+cY+")");

		int colNum = areaE.X - areaB.X+1;
		int strNum = areaE.Y - areaB.Y+1;

		//Debug info
		//Debug.Log("Selected Area - ("+ strNum+"x"+colNum+")");

		bool[,] findedCells = new bool[strNum,colNum];
		for(int i = 0; i<strNum; i++){
			if(i!=0) cY++;
			cX=areaB.X;
			for(int j=0; j<colNum; j++){
				if(j!=0) cX++;
				findedCells[i,j] = this.logicMatrix[cX,cY];

				//Debug info
				//Debug.Log("Readed from Matrix("+ cX+","+cY+")");
			}
	}
		return findedCells;
	}

	private void SetArea(Cartesian setAreaB, Cartesian setAreaE){
		int cX, cY;
		cX = setAreaB.X;
		cY = setAreaB.Y;
		int ScolNum = setAreaE.X - setAreaB.X+1;
		int SstrNum = setAreaE.Y - setAreaB.Y+1;
		for(int i = 0; i<SstrNum; i++){
			if(i!=0) cY++;
			cX=setAreaB.X;
			for(int j=0; j<ScolNum; j++){
				if(j!=0) cX++;
				this.logicMatrix[cX,cY] = true;
			}
		}
	}
}