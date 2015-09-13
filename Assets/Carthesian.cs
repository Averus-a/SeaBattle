using UnityEngine;
using System.Collections;

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
	public void ToUnits(out float ab, out float ord){
		ab = this.x*20;
		ord = this.y*20;
	}
	public static Cartesian FromUnits(float ab, float ord){
		return new Cartesian ((int) ab / 20, (int) ord / 20);
	}
}