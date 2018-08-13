using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour {

	public Loot m_Loot;
	public RectTransform m_LootHolder;
	public RectTransform m_InventoryHolder;
	public Vector3 m_DragOffset;
	public Vector3 m_PreviousPosition;
	public float m_SpawnChance;

	private void Awake() {
		EventTrigger EventTrigger = GetComponent<EventTrigger>();

		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener( (eventData) => { PointerDown(); } );
		EventTrigger.triggers.Add(entry);

		EventTrigger.Entry entry1 = new EventTrigger.Entry();
		entry1.eventID = EventTriggerType.Drag;
		entry1.callback.AddListener( (eventData) => { Drag(); } );
		EventTrigger.triggers.Add(entry1);

		EventTrigger.Entry entry2 = new EventTrigger.Entry();
		entry2.eventID = EventTriggerType.EndDrag;
		entry2.callback.AddListener( (eventData) => { EndDrag(); }  );
		EventTrigger.triggers.Add(entry2);

	}

	public void PointerDown(){
		m_DragOffset = transform.position - Input.mousePosition;
		m_PreviousPosition = transform.position;
	}
	
	public void Drag(){
		transform.position = Input.mousePosition + m_DragOffset;
	}

	public void EndDrag(){
		if(WithinInventory()){
			transform.SetParent(m_InventoryHolder);
			m_Loot.m_LootList.RemoveAt(m_Loot.m_LootList.IndexOf(this.gameObject));
		}
		if(WithinLoot()){{
			transform.SetParent(m_LootHolder);
			m_Loot.m_LootList.Add(gameObject);
		}
		}else if(!WithinLoot() && !WithinInventory()){
			transform.position = m_PreviousPosition;
		}
	}

	private bool WithinLoot(){
		Vector3[] Corners = new Vector3[4];
		m_LootHolder.GetWorldCorners(Corners);
		Vector3 MousePos = Input.mousePosition;

		if(MousePos.x > Corners[0].x && MousePos.y > Corners[0].y)
			if(MousePos.x < Corners[2].x && MousePos.y < Corners[2].y)
				if(m_LootHolder.gameObject.activeInHierarchy)
					return true;
		return false;
	}

	private bool WithinInventory(){
		Vector3[] Corners = new Vector3[4];
		m_InventoryHolder.GetWorldCorners(Corners);
		Vector3 MousePos = Input.mousePosition;

		if(MousePos.x > Corners[0].x && MousePos.y > Corners[0].y)
			if(MousePos.x < Corners[2].x && MousePos.y < Corners[2].y)
				if(m_InventoryHolder.gameObject.activeInHierarchy)
					return true;
		return false;
	}
}
