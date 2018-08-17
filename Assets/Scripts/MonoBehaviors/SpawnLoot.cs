using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLoot : MonoBehaviour {

	public List<Item> itemList;
	public List<Item> spawnedItemList;
	public int possibleLoot;
	
	private void Start() {
		for(int i = 0 ; i < possibleLoot; i++){
			Item RandomItem = itemList[Random.Range(0, itemList.Count)];
			float RandomNumber = Random.Range(0, 100f);

			if(RandomNumber <= RandomItem.spawnChance){
				spawnedItemList.Add(Instantiate(RandomItem));
			}
		}
	}
}
