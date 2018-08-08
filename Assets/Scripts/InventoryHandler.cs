using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour {
	
	public int m_Capacity;
	public int m_MaxCapacity;
	public Text m_CapacityText;
	public RectTransform m_Canvas;
	public RectTransform m_InventoryHolder;
	public RectTransform m_LootHolder;

	private void Awake() {
		SetCapacityText();
	}

	public void RemoveCapacity(int amount){
		m_Capacity = m_Capacity - amount;
		SetCapacityText();
	}
	
	public void AddCapacity(int amount){
		m_Capacity = m_Capacity + amount;
		SetCapacityText();
	}

	public void SetCapacityText(){
		m_CapacityText.text = m_Capacity + "/" + m_MaxCapacity;
	}
}
