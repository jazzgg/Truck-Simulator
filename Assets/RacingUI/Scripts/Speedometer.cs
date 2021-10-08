using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RacingUI
{
public class Speedometer : MonoBehaviour {

	[Space]
	public Text	speedTxt;
	public Text	gearTxt;
	public Text	brakeWarningTxt;
	public Image brakeWarningImg;

	[Header("Arrow")]
	public RectTransform arrow;
	public float minAngle;
	public float maxAngle;
	public float Value01{
		get{
			return Mathf.InverseLerp (minAngle, maxAngle, arrow.localRotation.z);
		}
		set{
			arrow.localRotation = Quaternion.Euler (arrow.localRotation.x, arrow.localRotation.y, Mathf.Lerp (minAngle, maxAngle, Mathf.Clamp01 (value)));
		}
	}
		
	public bool warningBlinking;
	[Range(.1f,10)]
	public float brakeWarningBlinkingSpeed;				//PerSecond

	public void SetValue01(float val)
	{
		Value01 = val;
	}

	public void SetSpeed(int speed)
	{
		speedTxt.text = speed.ToString ();
	}

	public void SetGear(int gear)
	{
		gearTxt.text = gear == 0 ? "N" : gear < 0 ? "R" : gear.ToString ();
	}


	public void StartBrakeWarning(){
		warningBlinking = true;
	}
	public void StopBrakeWarning(){
		warningBlinking = false;
		brakeWarningTxt.color = brakeWarningImg.color = Color.grey;
	}

	void Update ()
	{
		if(warningBlinking) {
			float pingPong = 0;
			pingPong = Mathf.PingPong (Time.time * brakeWarningBlinkingSpeed * 2, 1);
			brakeWarningTxt.color = brakeWarningImg.color = Color.Lerp (brakeWarningImg.color, Color.Lerp (Color.gray, Color.red, Mathf.Round (pingPong)), Time.deltaTime * brakeWarningBlinkingSpeed * 5);
		}
	}


}
}
