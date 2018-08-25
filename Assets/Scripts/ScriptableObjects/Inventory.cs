using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory")]
public class Inventory : ScriptableObject {

	public float maxWeight;
	public float currentWeight;
	public List<GameObject> instanceItems;
	
	private void OnEnable() {
		currentWeight = 0;
		instanceItems = new List<GameObject>();
	}
}
