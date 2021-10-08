using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace RacingUI
{
[RequireComponent (typeof(Toggle))]
public class ToggleEventTrigger : MonoBehaviour {

	public bool checkOnAwake = true;
	public UnityEvent onTrueValue;
	public UnityEvent onFalseValue;

	void Awake () {
		GetComponent <Toggle> ().onValueChanged.AddListener (
			v =>
			{
				if(v && onTrueValue != null)
					onTrueValue.Invoke ();
				if(!v && onFalseValue != null)
					onFalseValue.Invoke ();	
			});

		if(checkOnAwake)
		{
			if(GetComponent <Toggle> ().isOn)
				onTrueValue.Invoke ();
			else
				onFalseValue.Invoke ();
		}
	}

}
}