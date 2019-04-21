using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CurrentItemsScript))]
public class Inserter : MonoBehaviour {

    //Slider in editor for beltspeed
    [Range(0.1f, 2)]
    public float InserterSpeed;

    public bool isEnabled = true;

    //push to these
    public GameObject NextNode;
	public MachineScript NextMachineScript;
    public BeltS NextBeltScript;
    //end of push to these 

    //itemScripts
    public CurrentItemsScript PrevItemsScript; //pull items from this one     
    public CurrentItemsScript  CurrentItemsScript; //use as cache
	
	private GameObject Ob;

    public string NextType;
	/*
		----------------------------------------------------
		this should be the universal move between objects script all other object should move between eachother (belt) or not move at all (machine)
		----------------------------------------------------
	
	*/
    

	void Start () {
		Ob = this.gameObject;
        CurrentItemsScript = Ob.GetComponent<CurrentItemsScript>();
        StartCoroutine(MoveItems()); //running push item func every 2 sec using internal loop

    }

    public CurrentItemsScript GetPrevNode(){
		GameObject prev; // next belt

		Debug.DrawRay(Ob.transform.position, -Ob.transform.forward  ,Color.yellow ,0.5f);
		Debug.DrawRay(Ob.transform.position, Vector3.up ,Color.red ,0.3f);

		RaycastHit hit;
		Ray ray = new Ray(Ob.transform.position, -Ob.transform.forward);

        //IF NEXT OBJECT IS a MACHINE OF ANY TYPE
        if (Physics.Raycast(ray, out hit,1f) && hit.collider.transform.gameObject.layer == 9) { //change to include all objects or use layer
            
            //stop creating that way 
            prev = hit.collider.gameObject;
            PrevItemsScript = prev.GetComponent<CurrentItemsScript>();
        
        } else {

            PrevItemsScript = null;
            prev = null;
        }

		return PrevItemsScript;
        
    }
	public GameObject GetNextNode(){
		GameObject next; // next belt

		Debug.DrawRay(Ob.transform.position, Ob.transform.forward ,Color.blue ,0.1f);
		Debug.DrawRay(Ob.transform.position, Vector3.up ,Color.red ,0.3f);

		RaycastHit hit;
		Ray ray = new Ray(Ob.transform.position, Ob.transform.forward);

        //IF NEXT OBJECT IS BELT
        if (Physics.Raycast(ray, out hit,1f) && hit.collider.transform.tag == "Belt") { //change to include all objects or use layer //stop creating that way 
            next = hit.collider.gameObject;
            NextBeltScript = next.GetComponent<BeltS>();
            NextType = "Belt";

        } else if (Physics.Raycast(ray, out hit,1f) && hit.collider.transform.tag == "Assembly") {
            //IF NEXT OBJECT IS MACHINE 
            next = hit.collider.gameObject;
            NextMachineScript = next.GetComponent<MachineScript>();
            NextType = "Assembly";

        } else {
            NextBeltScript = null;
            next = null;
        }

		return next;
	}

    void PullItemFromPrevNode(){

        //maybe check if item has been in list for (beltspeed) time and THEN move it to next node
		CurrentItemsScript prev = GetPrevNode(); //Get the CurrentItemsScript Attached to every PrevNode

        //check stuff before pushing
        if (prev != null && this.CurrentItemsScript.CurrentItems.Count < this.CurrentItemsScript.Capacity && prev.CurrentItems.Count > 0) {
            this.CurrentItemsScript.CurrentItems.Add(prev.CurrentItems[0]);
            prev.CurrentItems.Remove(prev.CurrentItems[0]);
        }
        
    }

	void PushItemtoNextNode(){ // must run every like 0.5 sec or something anyway make a another parent function to call this on start 
		//maybe check if item has been in list for (InserterSpeed) time and THEN move it to next node
		BeltS nextB = NextBeltScript;
        MachineScript nextM = NextMachineScript;
    
		NextNode = GetNextNode(); //get nextbelt before every pushitem call

        if (NextType == "Assembly"){ //pushing from inserter to machine
            if (nextM != null && this.CurrentItemsScript.CurrentItems.Count > 0 && nextM.CurrentItemsScript.CurrentItems.Count < nextM.CurrentItemsScript.Capacity) { // && NextMachineScript.CurrentItemsScript.CurrentItems.Count < NextMachineScript.Recipe.NumofRequiremtns) {
				
                //if(CurrentItemsScript.CurrentItems[0].ID == nextM.Recipe.Requirements.ID){ 
                if(nextM.Recipe.Requirements.Contains(CurrentItemsScript.CurrentItems[0])){ //don't push if its not in requirements   
                    //push item
					nextM.InputItems.Add(this.CurrentItemsScript.CurrentItems[0]);
					this.CurrentItemsScript.CurrentItems.Remove(this.CurrentItemsScript.CurrentItems[0]);

				}
            }

        } else if (NextType == "Belt"){ //pushing from inserter to belt
            //check stuff before pushing
            if (nextB != null && this.CurrentItemsScript.CurrentItems.Count > 0 && nextB.CurrentItemsScript.CurrentItems.Count < nextB.CurrentItemsScript.Capacity) {
                nextB.CurrentItemsScript.CurrentItems.Add(this.CurrentItemsScript.CurrentItems[0]);
                this.CurrentItemsScript.CurrentItems.Remove(this.CurrentItemsScript.CurrentItems[0]);

            }
            
        }
        
    }

    IEnumerator MoveItems(){
        if(isEnabled){
            PullItemFromPrevNode(); //run pull item 
            yield return new WaitForSecondsRealtime(InserterSpeed);
            PushItemtoNextNode(); //run push item 

            //redo function
            yield return new WaitForSecondsRealtime(InserterSpeed);
            StartCoroutine(this.MoveItems());
        }
    }




}

