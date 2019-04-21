using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityGenerator : MonoBehaviour {
	public bool isEnabled = true; 
	public GameObject Ob;

	//Electricity stuff
	public float MaxElectricityOutPut;
	public float MinElectricityOutput;
	public float CurrentElectricityOutput;

	public ElectricityRecipe Recipe;
	public CurrentItemsScript CurrentItemsScript; //is completedITEMS
	
	
	//crafting stuff
	private int ReqIn;
	public bool RequirementsFilled = false;
	public bool isGenerating = false;
	void Start () {
		Ob = this.gameObject;
		CurrentItemsScript = Ob.GetComponent<CurrentItemsScript>();
	}
	
	void FixedUpdate(){
		//get ReqIn
        ReqIn = Recipe.NumofRequiremtns;
		
		//check if requirements are met
        if(ReqIn <= CurrentItemsScript.CurrentItems.Count){
		RequirementsFilled = true;
		}
         


        //CREATING ANOTHER ITEM
        if (RequirementsFilled && !isGenerating && isEnabled) {
            isGenerating = true;
            StartCoroutine(Generate());
        }
	}

	void DoStuff(){

	}

	IEnumerator Generate(){


		yield return new WaitForSecondsRealtime(this.Recipe.GenerateTime); 
		//add stuff here to make transistion smoother
	}

	


}
