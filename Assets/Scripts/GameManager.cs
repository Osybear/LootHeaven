using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject m_LootHolder;

	private void Awake() {
		m_LootHolder.SetActive(false);
	}
}
