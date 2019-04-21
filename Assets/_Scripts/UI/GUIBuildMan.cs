using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIBuildMan : MonoBehaviour {

	// Use this for initialization
 
	public MouseSelect mouseSelect;
	public List<GameObject> Buildables = new List<GameObject>();
    public GameObject ItemsButton;
    public GameObject Grid;
    public Animator Anim;


    void Start()
    {
        mouseSelect = Camera.main.GetComponent<MouseSelect>();
        Transform Container = GameObject.FindGameObjectWithTag("BuildablesContainers").transform;
        for (int i = 0; i < Container.childCount; i++)
        {
            Buildables.Add(Container.GetChild(i).gameObject);
        }

        for (int i = 0; i < Buildables.Count; i++)
        {
            GameObject Butinst = Instantiate(ItemsButton, ItemsButton.transform.position, Quaternion.identity);
            Butinst.transform.localScale = new Vector3(0.75f,0.75f,0.75f); 
            Butinst.GetComponent<Image>().sprite = Buildables[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
            Butinst.transform.SetParent(Grid.transform);
            int a = i;
            Butinst.GetComponent<Button>().onClick.AddListener(delegate { this.ChangeBuildableTo(a+1); });

        }
   
    }
    
    private void Update()
    {
        if(mouseSelect.BuildModeOn)
        {
            Anim.SetBool("BuildModeToggleOn",true);
            Anim.SetBool("BuildModeToggleOff", false);
        }
        else
        {
            Anim.SetBool("BuildModeToggleOff", true);
            Anim.SetBool("BuildModeToggleOn", false);
        }
    }

    public void ChangeBuildableTo(int item){
        //Debug.Log(item);
		mouseSelect.newPrefab = Buildables[item - 1];
        Destroy(mouseSelect.PrefabPlacer);
        mouseSelect.InstantiateOnce = true;
	}

	public void TriggerBuildMode(bool boolean){
		
		mouseSelect.BuildModeOn = boolean;
	}
	
	


}
