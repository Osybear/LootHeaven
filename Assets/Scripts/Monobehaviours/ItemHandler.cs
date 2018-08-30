using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHandler : MonoBehaviour {

	public Item item;
	public Image image;

	private void Start() {
		image.type = Image.Type.Simple;
		image.sprite = item.sprite;
		image.SetNativeSize();
	}
}
