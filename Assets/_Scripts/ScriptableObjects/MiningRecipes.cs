using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/MiningRecipe" , order = 3)]
public class MiningRecipes : ScriptableObject {

	public ItemValues Result;
	
	[Range (0,5)]
	public float MiningTime;

}
