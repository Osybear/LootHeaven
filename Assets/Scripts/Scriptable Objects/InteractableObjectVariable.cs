using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InteractableObjectVariable : ScriptableObject {

	[TextArea]
	public string developerDescription;
	public InteractableObject value;
}
