using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StringVariable : ScriptableObject {
	
	[TextArea]
	public string developerDescription;
	[TextArea]
	public string value;
}
