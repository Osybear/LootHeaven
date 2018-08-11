using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject m_InventoryHolder;
	public GameObject m_OpenedLootHolder;
	public bool m_Searching = false;

	public bool WithinLoot(){
		Vector3[] Corners = new Vector3[4];
		m_OpenedLootHolder.GetComponent<RectTransform>().GetWorldCorners(Corners);
		Vector3 MousePos = Input.mousePosition;

		if(MousePos.x > Corners[0].x && MousePos.y > Corners[0].y)
			if(MousePos.x < Corners[2].x && MousePos.y < Corners[2].y)
				if(m_OpenedLootHolder.gameObject.activeInHierarchy)
					return true;
		return false;
	}
}
