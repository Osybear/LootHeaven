using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour {
	public InventoryHandler m_InventoryHandler;
	public string m_ItemName;
	public int m_Capacity;
	public int m_Value;
	public Vector3 m_DragOffset;
	public Vector3 m_PreviousPosition;
	private EventTrigger m_EventTrigger;

	private void Awake() {
		m_EventTrigger = GetComponent<EventTrigger>();

		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener( (eventData) => { PointerDown(); } );
		m_EventTrigger.triggers.Add(entry);

		EventTrigger.Entry entry1 = new EventTrigger.Entry();
		entry1.eventID = EventTriggerType.Drag;
		entry1.callback.AddListener( (eventData) => { Drag(); } );
		m_EventTrigger.triggers.Add(entry1);

		EventTrigger.Entry entry2 = new EventTrigger.Entry();
		entry2.eventID = EventTriggerType.EndDrag;
		entry2.callback.AddListener( (eventData) => { EndDrag(); } );
		m_EventTrigger.triggers.Add(entry2);
	}

	public void PointerDown(){
		m_DragOffset = transform.position - Input.mousePosition;
		m_PreviousPosition = transform.position;
		transform.SetParent(m_InventoryHandler.m_Canvas);
	}

	public void Drag(){
		transform.position = Input.mousePosition + m_DragOffset;
	}

	public void EndDrag(){
		if(WithinInventory()){
			m_InventoryHandler.AddCapacity(m_Capacity);
			transform.SetParent(m_InventoryHandler.m_InventoryHolder);
		}
		else if(WithinLoot()){
			m_InventoryHandler.RemoveCapacity(m_Capacity);
			transform.SetParent(m_InventoryHandler.m_LootHolder);
		}else{
			transform.position = m_PreviousPosition;
		}
	}

	private bool WithinLoot(){
		Vector3[] Corners = new Vector3[4];
		GetComponent<Item>().m_InventoryHandler.m_LootHolder.GetWorldCorners(Corners);
		Vector3 MousePos = Input.mousePosition;

		if(MousePos.x > Corners[0].x && MousePos.y > Corners[0].y)
			if(MousePos.x < Corners[2].x && MousePos.y < Corners[2].y)
				return true;
		return false;
	}

	private bool WithinInventory(){
		Vector3[] Corners = new Vector3[4];
		GetComponent<Item>().m_InventoryHandler.m_InventoryHolder.GetWorldCorners(Corners);
		Vector3 MousePos = Input.mousePosition;

		if(MousePos.x > Corners[0].x && MousePos.y > Corners[0].y)
			if(MousePos.x < Corners[2].x && MousePos.y < Corners[2].y)
				return true;
		return false;
	}
}
