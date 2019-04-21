using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentItemsScript : MonoBehaviour {
	public int Capacity = 10;
	public List<ItemValues> CurrentItems = new List<ItemValues>(); // these are the items currently on the Object
	
	public bool allowRenderItems = false;
	public bool ClearList = false;
	
	void LateUpdate(){
		foreach(ItemValues a in this.CurrentItems) if(a == null) this.CurrentItems.Remove(a); //remove null items from list
		
	}

	void Update(){
		if(allowRenderItems) RenderItem();
		if(ClearList){
			ClearList = false;
			Clear();
		}
	}

	void RenderItem(){
		Sprite ToRender = null;
		if(CurrentItems.Count > 0){
			ToRender = CurrentItems[0].Sprite;
		}

		this.transform.Find("ItemSprite").GetComponent<SpriteRenderer>().sprite = ToRender;
	}

	void Clear(){
		CurrentItems.Clear();
	}

}
