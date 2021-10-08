using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RacingUI;

namespace RacingUI
{
public class DemoController : MonoBehaviour {

	public Toggle brakeWarningToggle;
	public Slider brakeWarningSlider;
	public Slider rpmTestSider;
	public Slider speedTestSider;
	public Slider gearTestSider;
	public Speedometer speedometer;
	public Text carName;

	public List<CustomizableCar> customCars;

	void Awake ()
	{
		brakeWarningSlider.onValueChanged.AddListener (v => speedometer.brakeWarningBlinkingSpeed = v);
		rpmTestSider.onValueChanged.AddListener (speedometer.SetValue01);
		speedTestSider.onValueChanged.AddListener (v => speedometer.SetSpeed((int)v));
		gearTestSider.onValueChanged.AddListener (v => speedometer.SetGear((int)v));
		brakeWarningToggle.onValueChanged.AddListener (b => 
			{	if(b)
					speedometer.StartBrakeWarning();
				else
					speedometer.StopBrakeWarning();
			}
		);
		ChangeCar (0);
	}
		
	public void SetBodyColor(Image img)
	{
		customCars.ForEach (o => {
			if(o.gameObject.activeSelf){
					o.SetBodyColor (img.color);
				}
		});
	}
	public void SetGlassColor(Image img)
	{
		customCars.ForEach (o => {
			if(o.gameObject.activeSelf)
				o.SetGlassColor (img.color);
		});	
	}
	public void SetWheelsColor(Image img)
	{
		customCars.ForEach (o => {
			if(o.gameObject.activeSelf)
				o.SetWheelsColor (img.color);
		});	
	}

	public void ChangeCar(int increment)
	{
		int index = customCars.FindIndex (c => c.gameObject.activeSelf);
		index += increment;

		while (index >= customCars.Count) {
			index -= customCars.Count;
		}
		while (index < 0) {
			index += customCars.Count;
		}

		customCars.ForEach (c => c.gameObject.SetActive (false));
		customCars[index].gameObject.SetActive (true);
		carName.text = customCars [index].name;
	}

	public void SelectCar(int index)
	{
		customCars.ForEach (c => c.gameObject.SetActive (false));
		customCars[index].gameObject.SetActive (true);
		carName.text = customCars [index].name;
	}

}
}