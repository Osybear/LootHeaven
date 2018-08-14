using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loot : MonoBehaviour {
	public LootHandler m_LootHandlerScript;
	public CapacityHandler m_CapacityHandler;
	public GameObject m_InventoryHolder;
	public GameObject m_LootHolder;
	public List<GameObject> m_LootList;
	public int m_PossibleLoot;
	public bool m_Searched;
	[TextArea]
	public string m_SearchingText;

	private void Awake() {
		Vector3[] Corners = new Vector3[4];
		m_LootHolder.GetComponent<RectTransform>().GetWorldCorners(Corners);

		List<GameObject> LootList = new List<GameObject>();

		for(int i = 0; i < m_PossibleLoot; i++){
			GameObject Prefab = m_LootList[Random.Range(0, m_LootList.Count)];
			float RandomNumber = Random.Range(0f,100f);

			if(RandomNumber <= Prefab.GetComponent<Item>().m_SpawnChance)
			{
				Vector3 RandomPos = new Vector3(Random.Range(Corners[0].x + 100, Corners[3].x - 100), Random.Range(Corners[0].y + 100, Corners[2].y - 100), 0);
				GameObject LootPrefab = Instantiate(Prefab, RandomPos, Quaternion.identity, m_LootHolder.transform);
				LootPrefab.SetActive(false);
				Item ItemScript = LootPrefab.GetComponent<Item>();
				ItemScript.m_LootHolder = m_LootHolder.GetComponent<RectTransform>();
				ItemScript.m_InventoryHolder = m_InventoryHolder.GetComponent<RectTransform>();
				ItemScript.m_LootScript = this;
				ItemScript.m_LootHandlerScript = m_LootHandlerScript;
				ItemScript.m_CapacityHandler = m_CapacityHandler;
				LootList.Add(LootPrefab);
			}
		}
		m_LootList = LootList;
	}

	private void OnMouseDown() {
		if(!m_LootHandlerScript.WithinLoot())
			m_LootHandlerScript.OpenItems(this);
	}


}
