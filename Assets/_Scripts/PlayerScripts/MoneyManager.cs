using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour {
	public float Money;
	// Use this for initialization
	
	void Start () {
		Money = 1000;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void moneyIn(float mon){
		Money += mon;

	}

	void moneyOut(float mon){
		Money -= mon;

	}


}
