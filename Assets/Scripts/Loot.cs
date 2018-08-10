using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {
	public GameObject m_MainCanvas;
	public GameObject m_LootHolder;
	public List<GameObject> m_Loot;
	public int m_PossibleLoot;

	private void Awake() {
		m_LootHolder = Instantiate(m_LootHolder, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, m_MainCanvas.transform);
		m_LootHolder.SetActive(false);

		Vector3[] Corners = new Vector3[4];
		m_LootHolder.GetComponent<RectTransform>().GetWorldCorners(Corners);

		for(int i = 0; i < m_PossibleLoot; i++){
			GameObject Prefab = m_Loot[Random.Range(0, m_Loot.Count)];
			float RandomNumber = Random.Range(0f,100f);

			if(RandomNumber <= Prefab.GetComponent<Item>().m_SpawnChance)
			{
				Vector3 RandomPos = new Vector3(Random.Range(Corners[0].x + 100, Corners[3].x - 100), Random.Range(Corners[0].y + 100, Corners[2].y - 100), 0);
				GameObject LootPrefab = Instantiate(Prefab, RandomPos, Quaternion.identity, m_LootHolder.transform);

				LootPrefab.GetComponent<Item>().m_LootHolder = m_LootHolder.GetComponent<RectTransform>();
			}
		}
	}

	private void OnMouseDown() {
		foreach(Transform LootHolder in m_MainCanvas.transform){
			if(LootHolder.gameObject.activeInHierarchy)
				LootHolder.gameObject.SetActive(false);
		}
		m_LootHolder.SetActive(!m_LootHolder.activeInHierarchy);
	}
}
