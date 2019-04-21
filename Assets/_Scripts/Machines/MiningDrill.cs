using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CurrentItemsScript))]
public class MiningDrill : MonoBehaviour {

	public List<MiningRecipes> PossibleRecipes; //later get dyncamicly from Files
    public GameObject Ob;
    public MiningRecipes Recipe;
    public CurrentItemsScript CurrentItemsScript; //is completedITEMS

	public int DrillCapacity = 5;
	public bool isEnabled = true;

	void Start () {
        Ob = this.gameObject;
        CurrentItemsScript = Ob.GetComponent<CurrentItemsScript>();
		StartCoroutine(Mine());
    }

    IEnumerator Mine(){

		//check if drill is at full capacity
		if(CurrentItemsScript.CurrentItems.Count <= DrillCapacity){ 
			//Add result of craft to completed items
			CurrentItemsScript.CurrentItems.Add(this.Recipe.Result);

		}

		//repeat
		yield return new WaitForSecondsRealtime(this.Recipe.MiningTime);
		StartCoroutine(Mine());
	
	}


}
