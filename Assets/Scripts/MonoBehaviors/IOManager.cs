using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IOManager : MonoBehaviour {

	public InteractableObject IO;
	public GameObject itemPrefab;
	public LootManager lootManager;
	public InventoryManager inventoryManager;

	private void Awake() {
		IO.instanceItems = new List<GameObject>();
		IO.searched = false;
		InstantiateLoot();
	}

	private void InstantiateLoot(){
		for(int i = 0; i < IO.amountToSpawn; i++){
			float randomNum = Random.Range(0, 100f);
			Item randomItem = IO.itemList[Random.Range(0, IO.itemList.Count)];

			if(randomNum < randomItem.spawnChance){
				GameObject clone = Instantiate(itemPrefab, lootManager.RandomPosition(), Quaternion.identity, lootManager.holder.transform);
				clone.name = randomItem.name;
				clone.GetComponent<ItemHandler>().item = randomItem;
				clone.GetComponent<ItemHandler>().IO = IO;
				clone.GetComponent<ItemHandler>().IOManager = this;
				IO.instanceItems.Add(clone);
				clone.SetActive(false);
			}
		}
	}	

	private void OnMouseDown() {
		if(!lootManager.WithinLootWindow(Input.mousePosition))
			lootManager.OpenIOLoot(IO, Camera.main.WorldToScreenPoint(GetComponent<Renderer>().bounds.center));
	}
}
