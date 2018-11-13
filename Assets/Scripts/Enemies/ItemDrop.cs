using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour {

	[System.Serializable]
	public class DroppableItem
	{
		public Item item;
		[Range(1,100)]
		public int dropChance;
	}

	public List<DroppableItem> droppableItems;

	void Awake(){
		
	}

	public void DropItem(){
		for (int i = 0; i < droppableItems.Count; ++i) {
			int randomChance = 100 - Random.Range (1, 100);
			if (randomChance <= droppableItems [i].dropChance) {
				Vector3 pos = new Vector3 (transform.position.x, transform.position.y + 2, transform.position.z);
				Instantiate (droppableItems [i].item, pos, transform.rotation);
				Debug.Log (droppableItems[i].item.name + " instantiated");
			}
		}
	}
}
