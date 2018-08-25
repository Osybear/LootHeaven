using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

	public Inventory inventory;
	public GameObject holder;
	public Text weigthText;

	private void Awake() {
		UpdateWeightText();
	}
	
	public bool WithinInventoryWindow(Vector3 position){
		Vector3[] corners = new Vector3[4];
		holder.GetComponent<RectTransform>().GetWorldCorners(corners);
		if(holder.activeInHierarchy)
			if(position.x > corners[0].x && position.x < corners[2].x)
				if(position.y >	 corners[0].y && position.y < corners[2].y)
					return true;
		return false;
	}

	public bool CheckWeight(float weight){
		if(weight + inventory.currentWeight <= inventory.maxWeight){
			inventory.currentWeight += weight;
			UpdateWeightText();
			return true;
		}
		return false;
	}

	public void RemoveWeight(float weight){
		inventory.currentWeight -= weight;
		UpdateWeightText();
	}

	public void UpdateWeightText(){
		weigthText.text = inventory.currentWeight + "/" + inventory.maxWeight;
	}
}
