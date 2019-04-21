using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalElectricityMan : MonoBehaviour {

	public GameObject Ob;
	public Transform rt;
	public float range;

	public float MinPowerRequired;
	public float MaxPowerRequired;
	public float CurrentPowerUsage;

	public bool isEnabled = false;

	public List<GameObject> PolesinRange = new List<GameObject>();

	public List<GameObject> PolesAllConnected = new List<GameObject>();
	
	void Start () {
		Ob = this.gameObject;
		rt = Ob.transform;
	}
	void DisableObject(){

	}

	void FixedUpdate () {
		getNear();
		GetAllPolesfromNearPoles(PolesinRange);
	}

	void getNear(){
		PolesinRange.Clear();
		Collider[] cols = Physics.OverlapSphere(rt.position,range);

		foreach(var a in cols){
			if(a.gameObject.layer == 9 && a.gameObject.GetInstanceID() != Ob.GetInstanceID()){
				if(a.tag == "ElectricPole"){
					PolesinRange.Add(a.gameObject); 
				}
			}
		}

		foreach(var a in PolesinRange){
			Debug.DrawLine(rt.position,a.transform.position,Color.cyan,0.1f);
		}

	}

	void GetAllPolesfromNearPoles(List<GameObject> tempPoles){
		//loop through all poles objects then looping through all their child objects
		foreach(var a in tempPoles){
			
		}
	}	

}
