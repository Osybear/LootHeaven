using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RoundHandler : MonoBehaviour {

	public GameObject m_Info;
	public Text m_InfoText;
	public GameObject m_LootHolder;
	public GameObject m_InventoryHolder;
	public int m_TimeSeconds;
	public Text m_TimerText;
	public List<ItemCount> m_MissedItemsList;
	public List<ItemCount> m_LootedItemsList;

	private void Awake() {
		m_TimerText.text = m_TimeSeconds.ToString();
		m_Info.SetActive(false);
		m_InfoText.text = null;
		InvokeRepeating("Timer", 1, 1);
	}

	public void Timer(){
		m_TimeSeconds--;
		m_TimerText.text = m_TimeSeconds.ToString();
		if(m_TimeSeconds == 0){
			EndScreenStuff();
			Debug.Break();
			Debug.LogError("round over");
		}
	}

	public void EndScreenStuff(){
		foreach(Transform Item in m_LootHolder.transform){
			Item ItemScript = Item.GetComponent<Item>();
			if(ItemScript != null){
				if(m_MissedItemsList.IndexOf(new ItemCount(Item.name, ItemScript.m_Value)) == -1)
					m_MissedItemsList.Add(new ItemCount(Item.name, ItemScript.m_Value));
				else{
					ItemCount ItemCount = m_MissedItemsList[m_MissedItemsList.IndexOf(new ItemCount(Item.name, ItemScript.m_Value))];
					ItemCount.m_Count++;
					ItemCount.m_Value += ItemScript.m_Value;
				}
			}
		}

		foreach(Transform Item in m_InventoryHolder.transform){
			Item ItemScript = Item.GetComponent<Item>();
			if(ItemScript != null){
				if(m_LootedItemsList.IndexOf(new ItemCount(Item.name, ItemScript.m_Value)) == -1)
					m_LootedItemsList.Add(new ItemCount(Item.name, ItemScript.m_Value));
				else{
					m_LootedItemsList[m_LootedItemsList.IndexOf(new ItemCount(Item.name, ItemScript.m_Value))].m_Count++;
				}
			}
		}
		int TotalValue = 0;
		m_InfoText.text += "You Missed";
		foreach(ItemCount Info in m_MissedItemsList)
		{
			m_InfoText.text += "\nx" + Info.m_Count + " " + Info.m_ItemName;
			TotalValue += Info.m_Value;
		}
		m_InfoText.text += "\nValue";
		m_InfoText.text += "\n$" + TotalValue;

		TotalValue = 0;
		m_InfoText.text += "\n\nYou Looted";
		foreach(ItemCount Info in m_LootedItemsList)
		{
			m_InfoText.text += "\nx" + Info.m_Count + " " + Info.m_ItemName;
			TotalValue += Info.m_Value;
		}
		m_InfoText.text += "\nValue";
		m_InfoText.text += "\n$" + TotalValue;

		m_Info.SetActive(true);
	}
	/* END SCREEEN
		Loot mised
		Loot aquired
		value of loot aquired 
	*/
}

[System.Serializable]
public class ItemCount : IEquatable<ItemCount>{
	public string m_ItemName;
	public int m_Count = 1;
	public int m_Value = 0;

	public ItemCount(string name, int value){
		m_ItemName = name;
		m_Value = value;
	}

	public bool Equals(ItemCount other)
    {
		//Choose what you want to consider as "equal" between Account objects  
		//for example, assuming newInfo is what you want to consider a match
		//(regardless of case)
		if (other == null) 
				return false;

		return String.Equals(this.m_ItemName, other.m_ItemName, StringComparison.OrdinalIgnoreCase);
    } 	
}

