using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;


namespace RacingUI
{
public class UiToggleGroup : MonoBehaviour {

	public string savePrefKey = "";

	public List<UiToggle> toggles;

	public Color Selected = Color.green;
	public Color Disselected = Color.white;

	void Start()
	{
		if (!string.IsNullOrEmpty (savePrefKey) && PlayerPrefs.HasKey (savePrefKey))
			SelectToggle (PlayerPrefs.GetInt (savePrefKey));
	}

	public void SelectToggle(UiToggle tog)
	{
		SelectToggle (toggles.IndexOf (tog));
	}

	public void SelectToggle(int id)
	{
		for(int i = 0; i < toggles.Count; i++)
		{
			toggles [i].SetParams (id == i, id == i ? Selected : Disselected);
		}	

		if (OnToggleEvent != null)
			OnToggleEvent.Invoke (id);

		if (!string.IsNullOrEmpty (savePrefKey))
			PlayerPrefs.SetInt (savePrefKey, id);

	}
		
	public ToggleEvent OnToggleEvent = new ToggleEvent();


}



public class ToggleEvent : UnityEvent<int> { }
}