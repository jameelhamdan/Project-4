using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorOB : MonoBehaviour {
	public GameObject ob;
	public Renderer rend;
	public Color col;
	// Use this for initialization
	void Start () {
		ob = this.gameObject;
		rend = ob.transform.GetComponent<Renderer>();
		rend.material.shader = Shader.Find("Unlit/Color");
		ChangeColor(col);
	}

	void CallOnClick(){

		Debug.Log("Called");
	}

	public void ChangeColor(Color col){
		rend.material.SetColor("_Color",col);
	}

	
}
