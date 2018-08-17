using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayItem : MonoBehaviour {

	public Item item;

	public Image artworkImage;

	private void Start() {
		artworkImage.sprite = item.artwork;
		artworkImage.SetNativeSize();
	}
}
