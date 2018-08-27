using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Interactable Object")]
public class InteractableObject : ScriptableObject {

	public int amountToSpawn = 0;
	public bool searched;
	[TextArea]
	public string searchDescription = "Searching Description";
	public List<Item> itemList;
	public List<GameObject> instanceItems;

}
