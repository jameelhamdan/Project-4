using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileS : MonoBehaviour {

	// Use this for initialization
	public GameObject ob;
	public Vector3 position;
	public Sprite deadsprite;
	public bool isAlive;

	void Start () {
		ob = this.gameObject;
		position = ob.transform.position;
		Invoke("changetosprite",1f);

	}

	void changetosprite(){
		if(!isAlive)
		ob.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = deadsprite;
	}
}
