using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedMenu : MonoBehaviour {

    GameObject Selected;
    MouseSelect MouseSelect;
    public GameObject Grid;
    public GameObject Button;
    public bool Once;
    GameObject NewSel;
    public Animator Anim;

	void Start ()
    {
        Once = true;
        MouseSelect = Camera.main.GetComponent<MouseSelect>();
    }
	

	void Update () {

        //ANIMATION
        if (Selected != null)
        {
            Anim.SetBool("MachineToolsOn", true);
            Anim.SetBool("MachineToolsOff", false);
        }
        else
        {
            Anim.SetBool("MachineToolsOff", true);
            Anim.SetBool("MachineToolsOn", false);
        }


        //TAKE SELECTED OBJECT FROM MouseSelect SCRIPT
        if (MouseSelect.RightClickSelected != null) {

            Selected = MouseSelect.RightClickSelected;

            
            //sprite stuff for machines
            
            Image newSprite = Grid.transform.parent.parent.Find("TooldsBackground/RecipeSprite").GetComponent<Image>();
            if(newSprite != null && Selected.transform.GetComponent<CurrentItemsScript>() != null){
            if(Selected.transform.tag == "Assembly"){
                newSprite.sprite = Selected.GetComponent<MachineScript>().Recipe.Result.Sprite;
                
                newSprite.color = new Color(1,1,1,1);
            } else if(Selected.layer == 9 && Selected.transform.GetComponent<CurrentItemsScript>().CurrentItems.Count > 0){
                newSprite.sprite = Selected.transform.GetComponent<CurrentItemsScript>().CurrentItems[0].Sprite;
                newSprite.color = new Color(1,1,1,1);
            } else {
                newSprite.sprite = null;
                newSprite.color = new Color(1,1,1,0);
            }

            Grid.transform.parent.parent.Find("TooldsBackground/RecipeSprite").GetComponent<Image>().sprite = newSprite.sprite;
            Grid.transform.parent.parent.Find("TooldsBackground/RecipeSprite").GetComponent<Image>().color = newSprite.color;
            //end sprite stuff for machines
            }
        } else if (MouseSelect.RightClickSelected == null) {

            Selected = null;
        }

        if(Selected != null) {

            NewSel = Selected;
            if (Once) {

                ///SPAWN BUTTONS FOR EACH POSSIBLE RECIPES IN SELECTED MACHINE
                if (MouseSelect.RightClickSelected != null && Selected.transform.tag == "Assembly") {

                    for (int i = 0; i < Selected.GetComponent<MachineScript>().PossibleRecipes.Count; i++){
                        GameObject isntr = Instantiate(Button, Button.transform.position, Quaternion.identity);
                        //isntr.transform.parent = Grid.transform; //replaced for editor warnings
                        isntr.transform.SetParent(Grid.transform); 
                        isntr.transform.GetChild(0).GetComponent<Text>().text = Selected.GetComponent<MachineScript>().PossibleRecipes[i].name;
                        isntr.transform.localScale = new Vector3(1, 1, 1);
                        
                        int n = i; //do reset value on every delegate create it works this way
                        isntr.GetComponent<Button>().onClick.AddListener(delegate { this.RecipeButtonsMachine(n+1); });

                        //BUTTON INSTANTIATED
                        if (i == Selected.GetComponent<MachineScript>().PossibleRecipes.Count - 1){
                            Once = false;
                        }
                    }
                } else if (MouseSelect.RightClickSelected != null && Selected.transform.tag == "Drill") {

                    for (int i = 0; i < Selected.GetComponent<MiningDrill>().PossibleRecipes.Count; i++){
                        GameObject isntr = Instantiate(Button, Button.transform.position, Quaternion.identity);
                        //isntr.transform.parent = Grid.transform; //replaced for editor warnings
                        isntr.transform.SetParent(Grid.transform); 
                        isntr.transform.GetChild(0).GetComponent<Text>().text = Selected.GetComponent<MiningDrill>().PossibleRecipes[i].name;
                        isntr.transform.localScale = new Vector3(1, 1, 1);
                        
                        int n = i; //do reset value on every delegate create it works this way
                        isntr.GetComponent<Button>().onClick.AddListener(delegate { this.RecipeButtonsMining(n+1); });

                        //BUTTON INSTANTIATED
                        if (i == Selected.GetComponent<MiningDrill>().PossibleRecipes.Count - 1){
                            Once = false;
                        }
                    }
                }

            }
        }
	}

    //DELETE
    public void DeteteButton()
    {
        if (Selected != null)
        {
            Destroy(MouseSelect.HiglitenObject);
            Destroy(Selected);
            Debug.Log("Deleted " +  Selected.name);
            Selected = null;
            MouseSelect.RightClickSelected = null;

            //Selected is Null defuse menu
            Grid.transform.parent.parent.Find("TooldsBackground/RecipeSprite").GetComponent<Image>().sprite = null;
            Grid.transform.parent.parent.Find("TooldsBackground/RecipeSprite").GetComponent<Image>().color = new Color(1,1,1,0);
            foreach (Transform child in Grid.transform){

                GameObject.Destroy(child.gameObject);
        
            }
        }

  
    }

    //ROTATION
    public void RotateButton(){
        if (Selected != null) {
            //check if item is rotatable
            //if(Selected.Script.Rotatable)
            Vector3 n = Selected.transform.rotation.eulerAngles;
            Selected.transform.eulerAngles = new Vector3(0, n.y + 90, 0);

            
        }
    }

    public void ClearButton(){
        MouseSelect.RightClickSelected.GetComponent<CurrentItemsScript>().ClearList = true;
    }


    //INSTANTIATED BUTTONS FOR RECIPES
    private void RecipeButtonsMachine(int a){
        MouseSelect.RightClickSelected.GetComponent<MachineScript>().Recipe = Selected.GetComponent<MachineScript>().PossibleRecipes[a-1];
    }

    private void RecipeButtonsMining(int a){
        MouseSelect.RightClickSelected.GetComponent<MiningDrill>().Recipe = Selected.GetComponent<MiningDrill>().PossibleRecipes[a-1];
        
    }
}
