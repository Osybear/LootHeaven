using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject {

	public Sprite sprite;
	public float value;
	public float weight;
	[Range(0, 100)]
	public float spawnChance;
}