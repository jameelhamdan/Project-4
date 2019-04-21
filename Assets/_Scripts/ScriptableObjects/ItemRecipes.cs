using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scriptable object for item recepies

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Recipes", order = 2)]
public class ItemRecipes : ScriptableObject {

    public List<ItemValues> Requirements = new List<ItemValues>();
    public List<int> NumofRequiremtns = new List<int>();
    public ItemValues Result;
    public float ManufactoringTime;

}
