using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour {

	public GameObject info;
	public Text infoText;

	public Text timeText;
	public int timeSeconds;

	public Inventory inventory;
	public List<InteractableObject> IOs;
	public List<ItemCount> inventoryCount;
	public List<ItemCount> lootCount;

	private void Start() {
		info.SetActive(false);
		UpdateTimeText();
		StartCoroutine(Timer());
	}

	private IEnumerator Timer(){
		while(timeSeconds > 0){
			yield return new WaitForSeconds(1f);
			timeSeconds--;
			UpdateTimeText();
		}
		RoundOver();
	}

	private void UpdateTimeText(){
		timeText.text = timeSeconds.ToString();
	}

	private void RoundOver(){
		//Debug.Break();
		//Debug.LogError("Round Over");	
		foreach(InteractableObject IO in IOs)
		{
			foreach(GameObject item in IO.instanceItems){
				Item itemSO = item.GetComponent<ItemHandler>().item;
				ItemCount newItemCount = new ItemCount(itemSO);
				if(lootCount.IndexOf(newItemCount) == -1)
					lootCount.Add(newItemCount);
				else
					lootCount[lootCount	.IndexOf(newItemCount)].amount++;	
			}
		}

		foreach(GameObject item in inventory.instanceItems){
			Item itemSO = item.GetComponent<ItemHandler>().item;
			ItemCount newItemCount = new ItemCount(itemSO);
			if(inventoryCount.IndexOf(newItemCount) == -1)
				inventoryCount.Add(newItemCount);
			else
				inventoryCount[inventoryCount.IndexOf(newItemCount)].amount++;
		}

		infoText.text = "Loot Missed";
		float totalValue = 0;	
		foreach(ItemCount itemCount in lootCount){
			infoText.text += "\n"+ itemCount.amount + "x " + itemCount.item.name + "s";
			totalValue += itemCount.amount * itemCount.item.value;
		}
		infoText.text += "\nValue $" + totalValue;

		infoText.text += "\n\nLoot Looted";
		totalValue = 0;	
		foreach(ItemCount itemCount in inventoryCount){
			infoText.text += "\n"+ itemCount.amount + "x " + itemCount.item.name + "s";
			totalValue += itemCount.amount * itemCount.item.value;
		}
		infoText.text += "\nValue $" + totalValue;

		info.SetActive(true);
	}
}

[System.Serializable]
public class ItemCount{
	public Item item;
	public int amount = 1;

	public ItemCount(Item item){
		this.item = item;
	}

	public override bool Equals(object other)
	{
		//This casts the object to null if it is not a Account and calls the other Equals implementation.
		return this.Equals(other as ItemCount);
	}

	public bool Equals(ItemCount other)
	{
		if (other == null) 
				return false;

		return this.item == other.item;
	}
}

