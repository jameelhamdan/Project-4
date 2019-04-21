using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savegame2 : MonoBehaviour {

	// this code should just save Buildables GameObject Data and reload it 
	
	public GameObject BuiltObjectsParent;
	public MouseSelect MouseSelectScript; 
	public List<GameObject> ListofMachineGameObjects = new List<GameObject>();

	//values and stuff
    public List<GameObject> AllTowersGameObject;
    public List<float> AllTowersXPos;
    public List<float> AllTowersZPos;
    public List<float> AllTowersYRot;
    public List<string> AllTowersType;
    public List<int> AllTowersRecipe;
    public GameObject ChangedObject;
    public GameObject MachinePrefab;
    public GameObject BeltPrefab;
    public GameObject DrillPrefab;
    public GameObject InserterPrefab;

    GameObject CurInstObj;
	void Start () {
		BuiltObjectsParent = GameObject.FindGameObjectWithTag("MACHINEPARENT");
		MouseSelectScript = Camera.main.GetComponent<MouseSelect>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SaveGameEvent(){
		UpdateLists(0);
		SaveGame.SaveGameButton();

	}

	public void LoadGameEvent(){
		UpdateLists(1);
		SaveGame.LoadGameButton();
		for (int i = 0; i < AllTowersXPos.Count; i++)
        {
            if (AllTowersType[i] == "Machine")
            {
                CurInstObj = MouseSelectScript.createTower(MachinePrefab, new Vector3(AllTowersXPos[i], 3, AllTowersZPos[i]), Quaternion.Euler(0, AllTowersYRot[i], 0));
                CurInstObj.GetComponent<MachineScript>().Recipe = CurInstObj.GetComponent<MachineScript>().PossibleRecipes[AllTowersRecipe[i]];

            }
            else if (AllTowersType[i] == "Belt")
            {
                CurInstObj = MouseSelectScript.createTower(BeltPrefab, new Vector3(AllTowersXPos[i], 3, AllTowersZPos[i]), Quaternion.Euler(0, AllTowersYRot[i], 0));
            }
            else if (AllTowersType[i] == "Drill")
            {
                CurInstObj = MouseSelectScript.createTower(DrillPrefab, new Vector3(AllTowersXPos[i], 3, AllTowersZPos[i]), Quaternion.Euler(0, AllTowersYRot[i], 0));
               // CurInstObj.GetComponent<MiningDrill>().Recipe = CurInstObj.GetComponent<MiningDrill>().PossibleRecipes[AllTowersRecipe[i]];
            }
            else if (AllTowersType[i] == "Inserter")
            {
                CurInstObj = MouseSelectScript.createTower(InserterPrefab, new Vector3(AllTowersXPos[i], 3, AllTowersZPos[i]), Quaternion.Euler(0, AllTowersYRot[i], 0));
            }
        }

	}
	void UpdateLists(int state){ //0 = saving // 1 == loading
		
		//delete existing objects in Parent obj if they exist
		if(state == 1)
		for(int i = 0; i < BuiltObjectsParent.transform.childCount;i++){
			Destroy(BuiltObjectsParent.transform.GetChild(i).gameObject);
		}  

		//Clear all lists
		ListofMachineGameObjects.Clear();
		AllTowersXPos.Clear();
		AllTowersZPos.Clear();
		AllTowersYRot.Clear();
		AllTowersType.Clear();
		AllTowersRecipe.Clear();

		foreach(Transform child in BuiltObjectsParent.transform){
			ListofMachineGameObjects.Add(child.gameObject);
		}

		foreach(GameObject a in ListofMachineGameObjects){
			AddObjectToLists(a);
		}
	}

	//must be looped through
	void AddObjectToLists(GameObject newT){
		AllTowersXPos.Add(newT.transform.position.x);
        AllTowersZPos.Add(newT.transform.position.z);
        AllTowersYRot.Add(newT.transform.rotation.eulerAngles.y);
        AllTowersGameObject.Add(newT);
        if(newT.tag == "Belt")
        {
            AllTowersType.Add("Belt");
            AllTowersRecipe.Add(0);
        }
        else if (newT.tag == "Machine")
        {
            AllTowersType.Add("Machine");


            for (int i = 0; i < newT.GetComponent<MachineScript>().PossibleRecipes.Count; i++)
            {
                if (newT.GetComponent<MachineScript>().PossibleRecipes[i] == newT.GetComponent<MachineScript>().Recipe)
                {
                    AllTowersRecipe.Add(i);
                }
            }
        }
        else if (newT.tag == "Drill")
        {
            AllTowersType.Add("Drill");
            for (int i = 0; i < newT.GetComponent<MiningDrill>().PossibleRecipes.Count; i++)
            {
                if (newT.GetComponent<MiningDrill>().PossibleRecipes[i] == newT.GetComponent<MiningDrill>().Recipe)
                {
                    AllTowersRecipe.Add(i);
                }
            }
           
        }
        else if (newT.tag == "Inserter")
        {
            AllTowersType.Add("Inserter");
            AllTowersRecipe.Add(0);
        }



	}

}
