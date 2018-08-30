using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IOHandler : MonoBehaviour {

	public InteractableObject IO;
	public InteractableObjectVariable tempIO;
	public StringVariable toolTipText;
	public UnityEvent onMouseDown;

	private void Awake() {
		IO.isOpened = false;
		IO.gameObject = gameObject;
	}

	private void OnMouseDown() {	
		tempIO.value = IO;
		onMouseDown.Invoke();
	}

	private void OnMouseOver() {
		if(IO.isOpened)
			toolTipText.value = "Click to \nClose";
		else
			toolTipText.value = "Click to \nSearch";
	}

	private void OnMouseExit() {
		toolTipText.value = null;
	}
}
