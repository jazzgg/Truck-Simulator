using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RacingUI;

namespace RacingUI
{
[ExecuteInEditMode]
public class PanelManager : MonoBehaviour {


	public List<UiPanel> panels = new List<UiPanel> ();

	public List<UiPanel> openedPanels {
		get{
			List<UiPanel> openedOnes = new List<UiPanel>();

			for(int i = 0; i < panels.Count; i ++)
			{
				if ((panels [i].isOpen) && (!openedOnes.Contains (panels [i])))
					openedOnes.Add (panels [i]);
			}

			return openedOnes;
		}
	}

	public UiPanel GetByName(string _name)
	{
		UiPanel _result = panels.Find (a => a.name == _name);
			
		if (_result == null)
			Debug.Log ("No panel found by name : " + _name);

		return _result;
	}

	#region Initializing

	void Awake()
	{		
		#if UNITY_EDITOR
			FindAllPanels ();
		#endif

	}
	void Start()
	{
		#if UNITY_EDITOR
			FindAllPanels ();
		#endif
	}
	void OnEnable()
	{
		#if UNITY_EDITOR
			FindAllPanels ();
		#endif
	}
	#endregion


	public static PanelManager Inst {
		get {
			if (!m_instance) {
				m_instance = FindObjectOfType (typeof(PanelManager)) as PanelManager;
				if (!m_instance)
					Debug.LogError ("There needs to be one active PanelManager script on a GameObject in your scene.");
			}
		   
			return m_instance;
		}
	}

	private static PanelManager m_instance;



	public void CloseAll()
	{
		for(int i = 0; i < panels.Count; i++)
			panels [i].Close ();
		
	}
	public void CloseAllExcept(params UiPanel[] except)
	{
		List<UiPanel> exceptList = new List<UiPanel> (except);

		for(int i = 0; i < panels.Count; i++)
		{
			if (!exceptList.Contains (panels [i]))
				panels [i].Close ();
		}
	}



	public void JustOpenPanel(UiPanel _panel)
	{
		OpenPanel (_panel, false);
	}

	public void OpenPanel(UiPanel _panel, bool _forceAdditive = false)
	{
		if (!_panel.isOpen) {
			_panel.gameObject.SetActive (true);
			_panel.Open (_forceAdditive);
		}
	}
		
	public void OpenPanelFromComponent(Component _fromComponent, bool _forceAdditive = false)
	{
		if (_fromComponent.gameObject.activeSelf == false)
			_fromComponent.gameObject.SetActive (true);

		UiPanel _panel = _fromComponent.gameObject.GetComponent <UiPanel> ();


		if (_panel != null)
			OpenPanel (_panel, _forceAdditive);
		else
			Debug.Log ("No UiPanel attached");
	}

	public void OpenPanels(List<UiPanel> listOfPanels)
	{
		for(int i = 0; i < listOfPanels.Count; i++)
		{
			OpenPanel (listOfPanels[i], false);
		}
	}

	public void ClosePanels(List<UiPanel> listOfPanels)
	{
		for(int i = 0; i < listOfPanels.Count; i++)
		{
			ClosePanel (listOfPanels[i]);
		}
	}

	public void ClosePanel(UiPanel _panel)
	{
		_panel.Close ();
	}

	public void ClosePanel(Component _fromComponent)
	{
		UiPanel _panel = _fromComponent.gameObject.GetComponent <UiPanel> ();

		if (_panel != null)
			ClosePanel (_panel);
		else
			Debug.Log ("No UiPanel attached");
	}



	public void OnPanelOpened(UiPanel _panel)
	{
		for(int i = 0; i < panels.Count; i++)
		{
			panels [i].OtherPanelOpened (_panel);
		}

	}

	public void FindAllPanels()
	{
		UiPanel[] newPanels = (UiPanel[])GameObject.FindObjectsOfType (typeof(UiPanel));


		for (int i = 0; i < newPanels.Length; i++) {
			if (!panels.Contains (newPanels [i]))
				panels.Add (newPanels [i]);

		}


		for (int i = 0; i < panels.Count; i++) {
			if (panels [i] == null)
				panels.RemoveAt (i);
		}

		for (int i = 0; i < panels.Count; i++) {
			for (int k = panels.Count - 1; k > 0; k--) {
				if (k == i)
					continue;

				if (panels [k].name.Equals (panels[i].name)) {
					panels.RemoveAt (k);
				}
			}

		}
	}
}
}