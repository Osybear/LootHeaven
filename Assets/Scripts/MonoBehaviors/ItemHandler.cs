using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemHandler : MonoBehaviour {

	public InteractableObject IO;
	public IOManager IOManager;
	public Vector3 previousPosition;

	public Item item;
	public Image artworkImage;

	private void Start() {
		artworkImage.sprite = item.artwork;
		artworkImage.SetNativeSize();
	}

	public void BeginDrag(){
		previousPosition = transform.position;
	}

	public void Drag(){
		transform.position = Input.mousePosition;
	}

	public void EndDrag(){
		if(IOManager.lootManager.WithinLootWindow(transform.position)){
			if(IOManager.lootManager.openedIO != IO){
				IO = IOManager.lootManager.openedIO;
				IOManager.inventoryManager.inventory.instanceItems.Remove(gameObject);
				IO.instanceItems.Add(gameObject);
				transform.SetParent(IOManager.lootManager.holder.transform);
				IOManager.inventoryManager.RemoveWeight(item.weight);
			}	
		}else if(IOManager.inventoryManager.WithinInventoryWindow(transform.position)){
			if(IOManager.inventoryManager.inventory.instanceItems.IndexOf(gameObject) == -1){
				if(IOManager.inventoryManager.CheckWeight(item.weight)){
					IOManager.lootManager.openedIO.instanceItems.Remove(gameObject);
					IOManager.inventoryManager.inventory.instanceItems.Add(gameObject);
					transform.SetParent(IOManager.inventoryManager.holder.transform);
					IO = null;
				}else{
					transform.position = previousPosition;
				}
			}
		}else{
			transform.position = previousPosition;
		}
	}
}
