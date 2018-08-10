using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour {

	public RectTransform m_LootHolder;
	public Vector3 m_DragOffset;
	public Vector3 m_PreviousPosition;
	public float m_SpawnChance;

	private void Awake() {
		EventTrigger EventTrigger = GetComponent<EventTrigger>();

		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener( (eventData) => { PointerDown(); } );
		EventTrigger.triggers.Add(entry);

	}

	public void PointerDown(){
		Debug.Log("Move to Inventory");
	}
}
