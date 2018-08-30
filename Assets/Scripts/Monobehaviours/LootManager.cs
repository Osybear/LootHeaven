using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootManager : MonoBehaviour {

	public List<InteractableObject> IOs;
	public GameObject holder;
	public GameObject itemPrefab;
	public Text holderText;

	public InteractableObjectVariable tempIO;
	public InteractableObject openedIO;

	private void Awake() {
		foreach(InteractableObject IO in IOs){
			IO.runtimeLoot = new List<GameObject>();
		}
		holder.SetActive(false);
	}

	private void Start() {
		foreach(InteractableObject IO in IOs){
			foreach(Item item in IO.itemList){
				float randomNum = Random.Range(0, 100f);
				if(randomNum < item.spawnChance){
					GameObject clone = Instantiate(itemPrefab, GetHolderRandomPosition(), Quaternion.identity, holder.transform);
					ItemHandler itemHandler = clone.GetComponent<ItemHandler>();
					itemHandler.item = item;
					IO.runtimeLoot.Add(clone);
					clone.SetActive(false);
				}
			}
		}
	}

	public Vector3 GetHolderRandomPosition(){
		Vector3[] corners = new Vector3[4];
		holder.GetComponent<RectTransform>().GetWorldCorners(corners);
		return new Vector3(Random.Range(corners[0].x, corners[2].x), Random.Range(corners[0].y, corners[2].y), 0);
	}

	public void OpenIO(){
		InteractableObject IO = tempIO.value;

		if(openedIO == null)
		{
			foreach(GameObject item in IO.runtimeLoot)
				item.SetActive(true);
			Vector3 worldPosition = IO.gameObject.GetComponent<Renderer>().bounds.center;
			holder.transform.position = Camera.main.WorldToScreenPoint(worldPosition);
			holder.SetActive(true);
			IO.isOpened = true;
			openedIO = IO;
		}else if(openedIO == IO){
			foreach(GameObject item in IO.runtimeLoot)
				item.SetActive(false);
			holder.SetActive(false);
			IO.isOpened = false;
			openedIO = null;
		}else if(openedIO != IO){
			foreach(GameObject item in openedIO.runtimeLoot)
				item.SetActive(false);
			openedIO.isOpened = false;

			foreach(GameObject item in IO.runtimeLoot)
				item.SetActive(true);
			Vector3 worldPosition = IO.gameObject.GetComponent<Renderer>().bounds.center;
			holder.transform.position = Camera.main.WorldToScreenPoint(worldPosition);
			IO.isOpened = true;
			openedIO = IO;
		}

		/* 
		if(IO.isOpened){
			foreach(GameObject item in IO.runtimeLoot){
				item.SetActive(false);
			}
			holder.SetActive(false);
			IO.isOpened = false;
		}else{
			foreach(GameObject item in IO.runtimeLoot){
				item.SetActive(true);
			}
			holder.SetActive(true);
			Vector3 worldPosition = IO.gameObject.GetComponent<Renderer>().bounds.center;
			holder.transform.position = Camera.main.WorldToScreenPoint(worldPosition);
			IO.isOpened = true;
		}
		*/
	}
}
