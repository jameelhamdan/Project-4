using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpriteRandomizer : MonoBehaviour {

	public List<Sprite> tiles = new List<Sprite>();
	public SpriteRenderer Target;
	private int randomInt;
	// Use this for initialization
	void Start () {
		int count = tiles.Count;
		
		
		if(count != 0){
			randomInt = Random.Range(0,tiles.Count);
			ChangeSpriteTo(Target,tiles[randomInt]);
		} else {
			Debug.LogError("TILE LIST MUST ATLEAST HAVE 1 SPRITE");
			randomInt = 0;
		}
		
		
	}
	

	void ChangeSpriteTo(SpriteRenderer to ,Sprite dis){
		to.sprite = dis;
	}


}
