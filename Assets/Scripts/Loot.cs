using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {
	public RectTransform m_LootHolder;
	public List<GameObject> m_Loot;
	public int m_PossibleLoot;

	private void Awake() {
		Vector3[] Corners = new Vector3[4];
		m_LootHolder.GetWorldCorners(Corners);

		List<GameObject> Loot = new List<GameObject>();

		for(int i = 0; i < m_PossibleLoot; i++){
			GameObject Prefab = m_Loot[Random.Range(0, m_Loot.Count)];
			float RandomNumber = Random.Range(0f,100f);

			if(RandomNumber <= Prefab.GetComponent<Item>().m_SpawnChance)
			{
				Vector3 RandomPos = new Vector3(Random.Range(Corners[0].x + 100, Corners[3].x - 100), Random.Range(Corners[0].y + 100, Corners[2].y - 100), 0);
				GameObject LootPrefab = Instantiate(Prefab, RandomPos, Quaternion.identity, m_LootHolder.transform);

				LootPrefab.GetComponent<Item>().m_LootHolder = m_LootHolder;
				Loot.Add(LootPrefab);
			}
		}
		m_Loot = Loot;
	}

	private void OnMouseDown() {
		if(m_LootHolder.gameObject.activeInHierarchy == false)
		{
			m_LootHolder.gameObject.SetActive(true);
			m_LootHolder.position = Camera.main.WorldToScreenPoint(transform.position);
			foreach(GameObject Loot in  m_Loot){
				Loot.SetActive(true);
			}
		}else{
			m_LootHolder.gameObject.SetActive(false);
			m_LootHolder.gameObject.SetActive(false);
			foreach(GameObject Loot in  m_Loot){
				Loot.SetActive(false);
			}
		}
	}

}
