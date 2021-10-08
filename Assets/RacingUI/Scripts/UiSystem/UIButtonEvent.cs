using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RacingUI;

namespace RacingUI
{
[RequireComponent (typeof(Button))]
public class UIButtonEvent : MonoBehaviour {

	public List<UiPanel> panelsToOpen;
	public List<UiPanel> panelsToClose;

	void Awake ()
	{
		if(panelsToOpen != null && panelsToOpen.Count > 0)
			GetComponent <Button> ().onClick.AddListener (() => PanelManager.Inst.OpenPanels (panelsToOpen));

		if(panelsToClose != null && panelsToClose.Count > 0)
			GetComponent <Button> ().onClick.AddListener (() => PanelManager.Inst.ClosePanels (panelsToClose));
	}


	void Start ()
	{
		//Destroy (this);
	}
}
}