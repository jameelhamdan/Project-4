using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPole : MonoBehaviour {
	public GameObject Ob;
	public Transform rt;
	public float range;
	public bool Connected; //to do list stuff
	public List<GameObject> MachinesinRange = new List<GameObject>();
	public List<GameObject> GeneratorsinRange = new List<GameObject>();
	public List<GameObject> PolesinRange = new List<GameObject>();
	
	void Start () {
		Ob = this.gameObject;
		rt = Ob.transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		getNear();
	}

	void getNear(){
		MachinesinRange.Clear();
		PolesinRange.Clear();
		Collider[] cols = Physics.OverlapSphere(rt.position,range);

		foreach(var a in cols){
			if(a.gameObject.layer == 9 && a.gameObject.GetInstanceID() != Ob.GetInstanceID()){
				if(a.tag == "ElectricPole"){
					PolesinRange.Add(a.gameObject); 

				} else {
					MachinesinRange.Add(a.gameObject);
				}
			}
		}

		foreach(var a in PolesinRange){
			Debug.DrawLine(rt.position,a.transform.position,Color.cyan,0.1f);
		}

	}	
}
