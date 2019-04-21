using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class getPathtoEndPoint : MonoBehaviour {

	public List<GameObject> navPoints = new List<GameObject>();
	public GameObject[] tempNavpoints;
	private GameObject tempStart;
	private GameObject tempEnd;

	private GameObject tempNavPoint;

	void Start () {
		tempStart = GameObject.FindGameObjectWithTag("navPointStart");
		tempEnd = GameObject.FindGameObjectWithTag("navPointEnd");
		tempNavPoint = tempStart;
	}

	void Update () {
		indexElements();
		//draws lines
		foreach(GameObject n in navPoints){
			if(n.GetInstanceID() != tempStart.GetInstanceID()){
				Debug.DrawLine(getPos(tempNavPoint),getPos(n),Color.blue,0f);
			}
			tempNavPoint = n;
			
		}

	}

	private void indexElements(){
		//reinitializes elements
		tempNavpoints = null;
		tempNavpoints = GameObject.FindGameObjectsWithTag("navPoint");
		//initializing nav points list 
		navPoints.Clear();
		//adding start
		navPoints.Add(tempStart);
		//adding normal navpoints 
		navPoints.AddRange(sortNavArray(tempNavpoints));

		//adding end 
		navPoints.Add(tempEnd);

	
	}
	private List<GameObject> sortNavArray(GameObject[] tempArray){
		//sort by distance to end point for now
		List<GameObject> tempfloat = tempArray.ToList();
		
		tempfloat =  tempfloat.OrderByDescending(

			x=> Vector3.Distance(getPos(x),getPos(tempEnd))
		
		).ToList();

		return tempfloat;
	}

	//gets Position Vector3
	private Vector3 getPos(GameObject ob){
		return ob.transform.position;
	}


}
