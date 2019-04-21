using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SCRIPTABLE OBJECT FOR ITEM VALUES

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item" , order = 1)]
public class ItemValues : ScriptableObject {

    public int ID;
    // string name is new because it alredy exist in every object values
    public new string name;
    public Sprite Sprite;
    public float Weight;
    public float Value;



}
