using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loot : MonoBehaviour {
	public GameManager m_GameManager;
	public GameObject m_MainCanvas;
	public GameObject m_InventoryHolder;
	public GameObject m_LootHolder;
	public Text m_LootHolderText;
	public List<GameObject> m_Loot;
	public int m_PossibleLoot;
	public bool m_Searched = false;

	private void Awake() {
		m_LootHolder = Instantiate(m_LootHolder, Camera.main.WorldToScreenPoint(GetComponent<Renderer>().bounds.center), Quaternion.identity, m_MainCanvas.transform);
		m_LootHolderText = m_LootHolder.transform.GetChild(0).GetComponent<Text>();

		m_LootHolder.SetActive(false);
		m_LootHolderText.text = null;
		
		Vector3[] Corners = new Vector3[4];
		m_LootHolder.GetComponent<RectTransform>().GetWorldCorners(Corners);

		List<GameObject> Loot = new List<GameObject>();

		for(int i = 0; i < m_PossibleLoot; i++){
			GameObject Prefab = m_Loot[Random.Range(0, m_Loot.Count)];
			float RandomNumber = Random.Range(0f,100f);

			if(RandomNumber <= Prefab.GetComponent<Item>().m_SpawnChance)
			{
				Vector3 RandomPos = new Vector3(Random.Range(Corners[0].x + 100, Corners[3].x - 100), Random.Range(Corners[0].y + 100, Corners[2].y - 100), 0);
				GameObject LootPrefab = Instantiate(Prefab, RandomPos, Quaternion.identity, m_LootHolder.transform);
				LootPrefab.SetActive(false);
				Item ItemScript = LootPrefab.GetComponent<Item>();
				ItemScript.m_LootHolder = m_LootHolder.GetComponent<RectTransform>();
				ItemScript.m_InventoryHolder = m_InventoryHolder.GetComponent<RectTransform>();
				Loot.Add(LootPrefab);
			}
		}
		m_Loot = Loot;
	}

	private void OnMouseDown() {
		if(m_GameManager.m_OpenedLootHolder == null || !m_GameManager.WithinLoot()){
			if(m_Searched == false && m_GameManager.m_Searching == false)
			{
				m_Searched = true;
				if(m_GameManager.m_OpenedLootHolder != null)
					m_GameManager.m_OpenedLootHolder.SetActive(false);
				m_GameManager.m_OpenedLootHolder = m_LootHolder;
				StartCoroutine(SearchItems());
			}else if(m_Searched == true && m_GameManager.m_Searching == false && m_LootHolder.activeInHierarchy == true){
				m_LootHolder.SetActive(false);
				m_GameManager.m_OpenedLootHolder = null;
			}else if(m_Searched == true && m_GameManager.m_Searching == false && m_LootHolder.activeInHierarchy == false){
				m_LootHolder.SetActive(true);
				if(m_GameManager.m_OpenedLootHolder != null)
					m_GameManager.m_OpenedLootHolder.SetActive(false);
				m_GameManager.m_OpenedLootHolder = m_LootHolder;
			}
		}
	}

	private IEnumerator SearchItems(){
		m_LootHolder.SetActive(true);
		m_GameManager.m_Searching = true;
		m_LootHolderText.text = "Searching...";
		yield return new WaitForSeconds(1f);

		foreach(GameObject Loot in m_Loot){
			yield return new WaitForSeconds(1f);
			Loot.SetActive(true);
		}

		if(m_LootHolder.transform.childCount == 1)
			m_LootHolderText.text = "Empty";
		else
			m_LootHolderText.text = null;

		m_GameManager.m_Searching = false;
	}
	

}
