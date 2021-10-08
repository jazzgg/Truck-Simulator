using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingUI
{
[RequireComponent (typeof(RectTransform))]
public class RectControll : MonoBehaviour {

	private RectTransform rect;

	void Awake ()
	{
		rect = GetComponent <RectTransform> ();
	}

	public void SetX(float x)
	{
		if (rect == null)
			Awake ();
		
		rect.anchoredPosition = new Vector2 (x, rect.anchoredPosition.y);
	}

	public void SetY(float y)
	{
		if (rect == null)
			Awake ();
		
		rect.anchoredPosition = new Vector2 (rect.anchoredPosition.x, y);
	}

}
}