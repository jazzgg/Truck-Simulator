using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RacingUI {
[RequireComponent (typeof(Toggle))]
public class ToggleSlider : MonoBehaviour {

	public Vector2 disabledPosition;
	public Vector2 enabledPosition;
	public RectTransform graphic;
	Toggle toggle;
	void Awake ()
	{
		toggle = GetComponent <Toggle> ();
		toggle.onValueChanged.AddListener (OnValueChanged);
	}


	void OnValueChanged(bool value)
	{
		StopCoroutine ("MoveTo");
		StartCoroutine ("MoveTo");
	}

	IEnumerator MoveTo()
	{
		Vector2 desiredPos = toggle.isOn ? enabledPosition : disabledPosition;
		while (Vector3.Distance (graphic.anchoredPosition, desiredPos) > 0.1f) {
			graphic.anchoredPosition = Vector2.Lerp (graphic.anchoredPosition, desiredPos, Time.deltaTime * 15);
			yield return null;
		}
		graphic.anchoredPosition = desiredPos;
	}




}
}