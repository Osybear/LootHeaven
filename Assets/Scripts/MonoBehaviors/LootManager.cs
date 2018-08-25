using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootManager : MonoBehaviour {

	public Text holderText;
	public GameObject holder;
	public InteractableObject openedIO;
	public bool searching;

	private void Awake() {
		holder.SetActive(false);
		holderText.text = null;
	}

	public Vector3 RandomPosition(){
		Vector3[] corners = new Vector3[4];
		holder.GetComponent<RectTransform>().GetWorldCorners(corners);
		return new Vector3(Random.Range(corners[0].x + 50, corners[2].x - 50), Random.Range(corners[0].y + 30, corners[2].y - 30), 0);
	}

	public void OpenIOLoot(InteractableObject IO, Vector3 holderPosition){
		if(holder.activeInHierarchy == true){
			if(IO == openedIO){ // is the player trying to opne the same IO
				if(searching == true){
					StopAllCoroutines();
					holderText.text = null;
					searching = false;
				}

				foreach(GameObject item in IO.instanceItems){
					item.SetActive(false);
				}

				holder.SetActive(false);
			}else{ // it is a diff IO the player is trying to open
				if(searching == true){
					StopAllCoroutines();
					holderText.text = null;
					searching = false;	
				}

				foreach(GameObject item in openedIO.instanceItems){
						item.SetActive(false);
				}

				openedIO = IO;
				if(IO.searched == false){
					StartCoroutine(SearchIO(IO));
					holder.transform.position = holderPosition;
				}else{ // it has been searched
					foreach(GameObject item in IO.instanceItems){
						item.SetActive(true);
					}
					holder.transform.position = holderPosition;
				}
			}
		}else{
			openedIO = IO;
			if(IO.searched == false){
				StartCoroutine(SearchIO(IO));
				holder.SetActive(true);
				holder.transform.position = holderPosition;
			}else{ // it has been searched
				foreach(GameObject item in IO.instanceItems){
					item.SetActive(true);
				}
				holder.SetActive(true);
				holder.transform.position = holderPosition;
			}
		}
	}

	public IEnumerator SearchIO(InteractableObject IO){
		searching = true;
		holderText.text = IO.searchDescription;
		for(int i = IO.instanceItems.Count - 1; i >= 0; i--){
			yield return new WaitForSeconds(1f);
			IO.instanceItems[i].SetActive(true);
		}
		holderText.text = null;
		IO.searched = true;
		searching = false;
	}

	public bool WithinLootWindow(Vector3 position){
		Vector3[] corners = new Vector3[4];
		holder.GetComponent<RectTransform>().GetWorldCorners(corners);
		if(holder.activeInHierarchy)
			if(position.x > corners[0].x && position.x < corners[2].x)
				if(position.y > corners[0].y && position.y < corners[2].y)
					return true;
		return false;
	}
}
