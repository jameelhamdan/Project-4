using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(CurrentItemsScript))]
public class BeltS : MonoBehaviour {

    //Slider in editor for beltspeed
    [Range(0.1f, 2)]
    public float BeltSpeed;
		
	public GameObject NextBelt;
	public CurrentItemsScript CurrentItemsScript; // currentItems Script
	
	private GameObject Ob;

	void Start () {
		Ob = this.gameObject;
		CurrentItemsScript = Ob.GetComponent<CurrentItemsScript>();
        StartCoroutine(PushItemtoNextNode()); //running push item func every 2 sec using internal loop
    }



	private BeltS GetNextBelt(){
		GameObject next; // next belt
		BeltS NextBeltScript;

		Debug.DrawRay(Ob.transform.position, Ob.transform.forward ,Color.blue ,0.1f);
		Debug.DrawRay(Ob.transform.position, Vector3.up ,Color.red ,0.3f);

		RaycastHit hit;
		Ray ray = new Ray(Ob.transform.position, Ob.transform.forward);

        //IF NEXT OBJECT IS BELT
        if (Physics.Raycast(ray, out hit,1f) && hit.collider.transform.tag == "Belt") { //change to include all objects or use layer
            //stop creating that way 
            next = hit.collider.gameObject;
            NextBelt = next;
			NextBeltScript = next.GetComponent<BeltS>();

        } else {

            NextBeltScript = null;
            next = null;
        }

		return NextBeltScript;

	}

	IEnumerator PushItemtoNextNode(){ // must run every like 0.5 sec or something anyway make a another parent function to call this on start 

		//maybe check if item has been in list for (beltspeed) time and THEN move it to next node
		BeltS next = GetNextBelt();  //get nextbeltscript before every pushitem 
        
        //check stuff before pushing

		//GLITCHES APPEAR HERE ITEM THAT ARE NOT SUPPOSED TO BE THERE ARE SUDDENLY THERE

        if (next != null && this.CurrentItemsScript.CurrentItems.Count > 0 && next.CurrentItemsScript.CurrentItems.Count < CurrentItemsScript.Capacity) {
			
			ItemValues tempITEM = this.CurrentItemsScript.CurrentItems[0]; //removing THEN checking if removed then adding
			//removing item from this node 
			this.CurrentItemsScript.CurrentItems.Remove(this.CurrentItemsScript.CurrentItems[0]);
            //adding to next node currentItems list
			next.CurrentItemsScript.CurrentItems.Add(tempITEM);

        }
    
            
		//redo function //sometimes glitches and stops all current StartCourountines
		yield return new WaitForSecondsRealtime(BeltSpeed);
		StartCoroutine(this.PushItemtoNextNode());

	}

	




}
