using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DrawGrid : MonoBehaviour {
	public GameObject ob;
	private Vector3 pos;

	public bool active;

	public int row = 10; //object width in meters

	void Start(){
		ob = this.gameObject;	
		
	}
	void Update() {
		
		draw(row);
	}
	void draw(int rows) { //draw grids
		pos = ob.transform.localPosition;
		Vector3 offset = new Vector3(0.5f,2,0.5f);
		pos += offset; 
		for(int i = -(rows/2); i <= (rows/2);i++){
			//horizontal
			Debug.DrawLine(pos + new Vector3(-(rows/2),0,i) , pos + new Vector3((rows/2),0,i),Color.red );

			//vertical
			Debug.DrawLine(pos + new Vector3(i,0,-(rows/2)) , pos + new Vector3(i ,0,(rows/2)),Color.green);
		}
	}

}