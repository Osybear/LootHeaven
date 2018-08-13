using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootHandler : MonoBehaviour {

	public GameObject m_LootHolder;
	public Text m_LootHolderText;
	public bool m_Searching = false;
	public List<GameObject> m_OpenedLoot = new List<GameObject>();

	private void Awake() {
		m_LootHolder.SetActive(false);
	}

	public void OpenItems(Loot LootScript){
		if(m_LootHolder.activeInHierarchy == true && m_Searching == false){ 
			if(LootScript.m_LootList == m_OpenedLoot){
				m_LootHolder.SetActive(false);
				foreach(GameObject Item in LootScript.m_LootList)
					Item.SetActive(false);
			}else if(LootScript.m_Searched == false && m_Searching == false){
				m_LootHolder.transform.position = Camera.main.WorldToScreenPoint(LootScript.GetComponent<Renderer>().bounds.center);
				foreach(GameObject Item in m_OpenedLoot)
					Item.SetActive(false);
				m_OpenedLoot = LootScript.m_LootList;
				StartCoroutine(SearchItems(LootScript));
			}else if(m_Searching == false){
				m_LootHolder.transform.position = Camera.main.WorldToScreenPoint(LootScript.GetComponent<Renderer>().bounds.center);
				foreach(GameObject Item in m_OpenedLoot)
					Item.SetActive(false);
				m_OpenedLoot = LootScript.m_LootList;
				foreach(GameObject Item in m_OpenedLoot)
					Item.SetActive(true);		
			}
		}else if(m_LootHolder.activeInHierarchy == false){
			if(LootScript.m_Searched == false && m_Searching == false){
				m_LootHolder.SetActive(true);
				m_OpenedLoot = LootScript.m_LootList;
				m_LootHolder.transform.position = Camera.main.WorldToScreenPoint(LootScript.GetComponent<Renderer>().bounds.center);
				StartCoroutine(SearchItems(LootScript));
			}else if(m_Searching == false){
				m_LootHolder.SetActive(true);
				m_OpenedLoot = LootScript.m_LootList;
				foreach(GameObject Item in m_OpenedLoot)
					Item.SetActive(true);	
				m_LootHolder.transform.position = Camera.main.WorldToScreenPoint(LootScript.GetComponent<Renderer>().bounds.center);	
			}
		}

	}

	public IEnumerator SearchItems(Loot LootScript){
		m_Searching = true;
		m_LootHolderText.text = "Searching...";
		yield return new WaitForSeconds(1f);

		foreach(GameObject Item in LootScript.m_LootList){
			yield return new WaitForSeconds(1f);
			Item.SetActive(true);
		}

		if(m_LootHolder.transform.childCount == 1)
			m_LootHolderText.text = "Empty";
		else
			m_LootHolderText.text = null;

		m_Searching = false;
		LootScript.m_Searched = true;
	}
	
	public bool WithinLoot(){
		Vector3[] Corners = new Vector3[4];
		m_LootHolder.GetComponent<RectTransform>().GetWorldCorners(Corners);
		Vector3 MousePos = Input.mousePosition;

		if(MousePos.x > Corners[0].x && MousePos.y > Corners[0].y)
			if(MousePos.x < Corners[2].x && MousePos.y < Corners[2].y)
				if(m_LootHolder.gameObject.activeInHierarchy)
					return true;
		return false;
	}
}
