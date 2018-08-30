using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour {

	public Text toolTipText;
	public StringVariable stringVariable;

	private void Awake() {
		toolTipText.text = null;
	}

	private void Update() {
		toolTipText.text = stringVariable.value;
		if(toolTipText != null)
			toolTipText.transform.position = Input.mousePosition;	
	}
}
