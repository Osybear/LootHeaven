using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject {

	public Sprite artwork;
	public float value;
	public float weight;
	[Range(0, 100)]
	public float spawnChance;
}
