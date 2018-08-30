using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InteractableObject : ScriptableObject {

	public GameObject gameObject;
	public bool isOpened;
	public List<Item> itemList = null;
	public List<GameObject> runtimeLoot = null;
}
