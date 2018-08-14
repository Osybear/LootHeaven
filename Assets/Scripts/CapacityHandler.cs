using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapacityHandler : MonoBehaviour {

	public Text m_CapacityText; 
	public int m_Capacity;
	public int m_MaxCapacity;

	private void Awake() {
		SetText();
	}

	public void SetText(){
		m_CapacityText.text = m_Capacity + "/" + m_MaxCapacity;
	}

	public bool CheckCapacity(int Amount){
		return m_Capacity + Amount > m_MaxCapacity;
	}

	public void AddCapacity(int Amount){
		m_Capacity = m_Capacity + Amount;
		SetText();
	}

	public void RemoveCapacity(int Amount){
		m_Capacity = m_Capacity - Amount;
		SetText();
	}
}
