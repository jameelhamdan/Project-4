using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

	public GameObject ob;

	public float speed = 50f;

	

	
	// Use this for initialization
	void Start () {
		ob = this.gameObject;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		 
		
		Movement();

    }

	private void Movement(){
		
		//get input axis
		float hor = Input.GetAxis("Horizontal"); 
		float ver = Input.GetAxis("Vertical");

        //move player
        Vector3 pos = new Vector3(hor * speed,0, ver * speed) * Time.deltaTime;
		ob.transform.Translate(pos);


		//move camera
		Vector3 tempPos = ob.transform.position;
		//Camera.main.gameObject.transform.position = new Vector3(tempPos.x,tempPos.y+10,tempPos.z);
		
	}

}