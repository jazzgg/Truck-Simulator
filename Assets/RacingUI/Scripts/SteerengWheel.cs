using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace RacingUI
{
[RequireComponent (typeof(RectTransform))]
public class SteerengWheel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public float steerRange = 100;
	public float maxAngle = 90;
	public float steeingValue {
		get;
		private set;
	}
	PointerEventData pData;
	RectTransform rect;
	private float angle;
	private float delta;
	void Awake ()
	{
		rect = GetComponent <RectTransform> ();
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		pData = eventData;
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		pData = null;
	}

	void Update ()
	{
		//STEERENG IS NOT CORRECT
		if(pData != null)
		{

			delta = pData.position.x - pData.pressPosition.x;
			steeingValue = Mathf.InverseLerp (-steerRange, steerRange, delta);
			steeingValue = Mathf.Lerp (-1, 1, steeingValue);
		}
		else{
			steeingValue = 0;
		}

		angle = Mathf.Lerp (-maxAngle, maxAngle, (steeingValue + 1) / 2);
		rect.rotation = Quaternion.Lerp (rect.rotation, Quaternion.Euler (0, 0, -angle), Time.deltaTime * 5);
	}
}
}