using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class MouseSelect : MonoBehaviour {

	public bool active1 = true;
	public bool BuildModeOn = false; 
	public GameObject objectselected = null;
	public GameObject objectHover= null;
	public GameObject newPrefab;
    public GameObject RightClickSelected;
    public GameObject LastSelected;
    public GameObject Grid;
    public GameObject HighlitenObjectPrefab;
    public GameObject HiglitenObject;
    public GameObject PrefabPlacer;

    public GameObject ContainerObject;

    GameObject newT;
    public bool InstantiateOnce;
    bool MovingCamera;
    float CountMouseMovement;
	void Start() {
        BuildModeOn = false;
		ContainerObject = GameObject.FindGameObjectWithTag("MACHINEPARENT");
	}

	void Update () {
     


        if (!MovingCamera)
        {
            objectHover = GetHoverdObject();
			//onLeftClick(objectHover);
            onRightClick(objectHover);

            if (BuildModeOn)
            {
                Rotate(objectHover);
            }
            else
            {
                Destroy(PrefabPlacer);
                newPrefab = null;
                
            }
           
        }

    }

    //RayCasts and Mouse Selection/Hover
    public GameObject GetHoverdObject(){

		GameObject newOb = null;
		RaycastHit hit;
       	Ray ray; //change this
		   
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		
		//mobile stuff
       	if (Physics.Raycast(ray, out hit) ){//*/ && ( Input.GetTouch(0).phase == TouchPhase.Began || Input.GetButtonDown("Fire2") )) {
			//if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)){

				newOb = hit.collider.gameObject;

			//}
		}

        if (Input.GetButtonDown("Fire2")){
            RightClickSelected = newOb;

        }
 
        return newOb;

	}

	void onLeftClick(GameObject ob){ //currently disabled
  
		if((Input.GetButtonUp("Fire1") || Input.touchCount > 0) && active1 && ob != null){
			if(objectselected == null){
				objectselected = ob;
				onHighlightEnter();
			}

			if(ob.GetInstanceID() != objectselected.GetInstanceID()){
					
			//if the object is different and replace the renderer stuff with a "selectable" bool
				if(objectselected != null && objectselected.GetComponent<Renderer>() != null) onHighlightExit();
				objectselected = ob;
				onHighlightEnter();	
				//Debug.Log("Sent Ray to " + ob);

				if(objectselected != null && objectselected.GetComponent<Renderer>() != null) {

						onHighlightEnter();			
				
				} else {
					//Debug.Log(objectselected.name +" is not HighLightable ");					
				}

			} else {
				//if object is the same
				
				//Debug.Log("Still highlighting");
			}

		} else if(ob == null){

			onHighlightExit();
		}
				
	}
	
	void onRightClick(GameObject ob){ //Currently Changed to FIRE1 change back to Fire2 when doing PC stuff

        if (ob != null && ob != RightClickSelected && ob.layer != 9 ){
       
            if (newPrefab != null){

                if (InstantiateOnce) { //disable scripts

                    PrefabPlacer = Instantiate(newPrefab, ob.transform.position + new Vector3(0, 1, 0), newPrefab.transform.rotation);
                    PrefabPlacer.SetActive(true);
                    
                    //disabling All Scripts in dummy object 
                    MonoBehaviour[] monoarray = PrefabPlacer.GetComponents<MonoBehaviour>();
                    foreach (var i in monoarray) i.enabled = false; 

                    PrefabPlacer.GetComponent<BoxCollider>().enabled = false;
                    PrefabPlacer.transform.GetChild(0).GetComponent<SpriteRenderer>().color =
                        new Color(PrefabPlacer.GetComponent<MeshRenderer>().material.color.r, PrefabPlacer.GetComponent<MeshRenderer>().material.color.b,
                        PrefabPlacer.GetComponent<MeshRenderer>().material.color.g, 0.4f);
                    InstantiateOnce = false;
                }
                if (PrefabPlacer != null)
                {
                    PrefabPlacer.SetActive(true);
                    PrefabPlacer.transform.position = ob.transform.position + new Vector3(0, 1, 0);
                }
            }
            else if (newPrefab = null)
            {
                Destroy(PrefabPlacer);
            }
        }
        else if(ob == null)
        {
            if (PrefabPlacer != null)
            {
                PrefabPlacer.SetActive(false);
            }
        }
        else if(ob == RightClickSelected || ob.layer == 9)
        {
          //  PrefabPlacer.SetActive(false);
        }


        if ((Input.GetButtonUp("Fire2") || Input.touchCount > 0) && active1){

            if (ob != null && (ob.layer != 9) && BuildModeOn)
            { // aka Empty 
                if (newPrefab != null)
                {
                    GameObject newT = createTower(newPrefab, ob.transform.position, newPrefab.transform.rotation);
                    SelectNewObject(newT);
                }
                Destroy(PrefabPlacer);
                newPrefab = null;

            }




            else if (ob != null && ob.layer == 9)
            { //9 is Machine Layer
                SelectNewObject(ob);
            }
        
            /*
            if(ob != null && (ob.layer == 9) ){ //change tag to LayerMask
                deleteTower(ob.gameObject);
            }  
            */

        }

	}

	public void Rotate(GameObject ob){
		if(Input.GetKeyDown(KeyCode.R) && (ob.layer == 9)){
			Vector3 n = ob.transform.rotation.eulerAngles;
			ob.transform.eulerAngles = new Vector3(0,n.y+90 ,0);
			Debug.Log(ob + " rotated ");
		}

	}

	//END OF RAYCASTS GROUP 
	//creates Obj
	public GameObject createTower(GameObject tower ,Vector3 pos,Quaternion rot){
		
		pos = new Vector3(Mathf.Round(pos.x) , 1 , Mathf.Round(pos.z));

		//add grid system later by custom rounding position
		newT = Instantiate(tower,pos,rot);
		newT.SetActive(true);
		newT.transform.SetParent(ContainerObject.transform);
        //Debug.Log("Tower Created");
		return newT;

    }

	//Change Obj from GUI
	public void ChangeCreatedObj(GameObject ob){
		newPrefab = ob;
		//Debug.Log("Changed Created Object to " + ob.ToString());
	}

	void onHighlightEnter(){
		//Ob.transform.GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Silhouetted Diffuse");
	}

	void onHighlightExit(){
		//Ob.transform.GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");

	}

    void SelectNewObject(GameObject ob){

        if (HiglitenObject != null) {

            Destroy(HiglitenObject);
        }

        if (ob != null)
        {
            RightClickSelected = ob;
            if (RightClickSelected == LastSelected)
            {
                RightClickSelected = null;
           
            }
            else if (RightClickSelected != LastSelected)
            {
                HiglitenObject = Instantiate(HighlitenObjectPrefab, new Vector3(ob.transform.position.x, 3, ob.transform.position.z), HighlitenObjectPrefab.transform.rotation);

            }
        }

        foreach (Transform child in Grid.transform){

            GameObject.Destroy(child.gameObject);
        
        }

        LastSelected = RightClickSelected;
        Camera.main.GetComponent<SelectedMenu>().Once = true;
    }

}
