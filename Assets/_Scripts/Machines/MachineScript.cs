using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(CurrentItemsScript))]
public class MachineScript : MonoBehaviour {

    public List<ItemRecipes> PossibleRecipes; //later get dyncamicly from Files
    public GameObject Ob;
    public ItemRecipes Recipe;
    public List<ItemValues> InputItems;
    public CurrentItemsScript CurrentItemsScript; //is completedITEMS

    //Timer when building new item
    float BuildTimer;
    public bool isBuilding = false;
    public bool RequirementsFilled = false;
    public bool isEnabled = true;
    private List<int> ReqIn = new List<int>();
    private List<int> ReqOut = new List<int>(); //change layta

	void Start () {
        Ob = this.gameObject;
        CurrentItemsScript = Ob.GetComponent<CurrentItemsScript>();
        
        
    }
	
    
	void FixedUpdate () {

        //get ReqIn
        ReqIn = Recipe.NumofRequiremtns;

        //Clear ReqOut
        ReqOut.Clear();

        //zero out ReqOut List
        for(int j = 0; j < ReqIn.Count; j++){
            ReqOut.Add(0);
        }

        //check if item exists in other array then increment int by index
        foreach(ItemValues a in InputItems){
            if(Recipe.Requirements.Contains(a)){
                int i = Recipe.Requirements.IndexOf(a);
                ReqOut[i] += 1;   
            }
        }
        RequirementsFilled = DoListsMatch(ReqIn,ReqOut);
         


        //CREATING ANOTHER ITEM
        if (RequirementsFilled && isBuilding == false && isEnabled) {
            isBuilding = true;
            StartCoroutine(Manufacture());
        }

    }

    IEnumerator Manufacture(){ //don't TOUCH THIS IT WORKS IF YOU WANT TO EDIT MAKE ANOTHER SCRIPT CALLED MACHINESCRIPTV2
        //remove items from InputItems
        for(int i = 0 ; i < ReqOut.Count ; i++){

            for(int j = 0; j < InputItems.Count; j++){ //loop through all items
                int NumtoDelete = ReqOut[i];
                int NumDeleted = 0;

                if(InputItems[j].ID == Recipe.Requirements[i].ID && NumDeleted < NumtoDelete){
                    InputItems.RemoveAt(j);
                    NumDeleted++;
                }

            }
        }

        //Remove Items

        //wait till ManufactureTime has Passed 
        yield return new WaitForSecondsRealtime(this.Recipe.ManufactoringTime);

        //Add result of craft to completed items
        CurrentItemsScript.CurrentItems.Add(this.Recipe.Result);
        isBuilding = false;
    }


    //dont ask me about this one i just stole it 
    private bool DoListsMatch(List<int> list1, List<int> list2){ //list2 can be bigger than list1
        var areListsEqual = true;

        if (list1.Count != list2.Count)
            return false;

        for (var i = 0; i < list1.Count; i++){

            if (list1[i] > list2[i]){ //edited != to >
                areListsEqual = false;
            }
        }

        return areListsEqual;
    }

}
