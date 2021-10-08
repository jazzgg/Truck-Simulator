using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using RacingUI;

namespace RacingUI
{
[RequireComponent(typeof(RectTransform))]
[ExecuteInEditMode]
public class UiPanel : MonoBehaviour {

	public string localizedName()
	{
		return name;
	}

	public bool isOpen{ 

		get{
			return gameObject.activeSelf && mIsOpen.HasValue ? mIsOpen.Value : false;
		}
		set{
			mIsOpen = value;
		}

	}

	private bool? mIsOpen;

	public CanvasGroup canvasGroup;
	public bool hideOnClose = false;
	public bool openAdditive = false;
	public bool openOthersOnClose = false;
	private List<UiPanel> rememberedClosedPanels;

	public bool openOnAwake = false;
	private bool autoDissable = true;


	public bool autoFindAnimators = true;
	public List<UiPanelAnimator> uiAnimators = new List<UiPanelAnimator>();

	public bool isAnyAnimatorPlaying()
	{
		bool _playing = false;

		for(int i = 0; i < uiAnimators.Count; i++)
		{
			if(uiAnimators [i].isPlaying)
			{
				_playing = true;
				break;
			}
		}

		return _playing;
	}


	void Awake()
	{
		if (PanelManager.Inst != null && !PanelManager.Inst.panels.Contains (this))
			PanelManager.Inst.panels.Add (this);


		isOpen = gameObject.activeSelf;

		canvasGroup = GetComponent <CanvasGroup> ();

		if (canvasGroup == null)
			canvasGroup = gameObject.AddComponent <CanvasGroup> ();

		SetVisiable (isOpen);

		if(autoFindAnimators) {
			UiPanelAnimator[] newAnimators = GetComponentsInChildren <UiPanelAnimator> (true);

			UiPanel[] childPanels = GetComponentsInChildren <UiPanel> (true);

			for (int i = 0; i < newAnimators.Length; i++) {
				if (!uiAnimators.Contains (newAnimators [i])) {

					bool canAdd = true;

					for (int k = 0; k < childPanels.Length; k++) 
					{
						if (childPanels [k].uiAnimators.Contains (newAnimators [i]))
								canAdd = false;
					}

					if(canAdd)
						uiAnimators.Add (newAnimators [i]);
				}
			}
		}

		if (openOnAwake)
			Open ();
		else
			CloseImmediate ();
	}

	public void SetVisiable(bool value)
	{
		if (canvasGroup == null)
			canvasGroup = GetComponent <CanvasGroup> ();


		canvasGroup.alpha = value ? 1 : 0;
		canvasGroup.interactable = value;
		canvasGroup.blocksRaycasts = value;
	}



	public void Open(bool _forceAdditive = false)
	{
		if (isOpen)
			return;
		
		isOpen = true;
		gameObject.SetActive (true);

		SetVisiable (true);

		if(openOthersOnClose)
		{
			rememberedClosedPanels = PanelManager.Inst.openedPanels;

			if (rememberedClosedPanels.Contains (this))
				rememberedClosedPanels.Remove (this);
			
			PanelManager.Inst.CloseAllExcept (this);
		}



		if (_forceAdditive)
			openAdditive = _forceAdditive;




		PanelManager.Inst.OnPanelOpened (this);


		if (Application.isPlaying) {
			StopAllCoroutines ();
			StartCoroutine (OpenPanel ());
		}

		if (OnOpenEvent != null)
			OnOpenEvent.Invoke (this);
	}


	public void Close(float waitForSec = 0)
	{
		if (!isOpen)
			return;

		isOpen = false;


		if(openOthersOnClose)
			PanelManager.Inst.OpenPanels (rememberedClosedPanels);


		if (OnCloseEvent != null)
			OnCloseEvent.Invoke (this);



		if (hideOnClose) {
			SetVisiable (false);
			return;
		}

		StopAllCoroutines ();
		StartCoroutine (ClosePanel(waitForSec));
	}

	public void CloseImmediate()
	{
		isOpen = false;
		gameObject.SetActive (false);
		StopAllCoroutines ();
	}

	IEnumerator OpenPanel()
	{
		for(int i = 0; i < uiAnimators.Count; i++)
		{
			uiAnimators [i].PlayOpen ();
		}


		while(isAnyAnimatorPlaying ())
		{
			yield return null;
		}

	}

	IEnumerator ClosePanel(float waitForSec = 0)
	{
		if(waitForSec > 0)
			yield return new WaitForSeconds (waitForSec);

		for(int i = 0; i < uiAnimators.Count; i++)
		{
			uiAnimators [i].PlayClose ();
		}

		while(isAnyAnimatorPlaying ())
		{
			yield return null;
		}

		if (autoDissable)
			gameObject.SetActive (false);
	}


		
	public void OtherPanelOpened(UiPanel _panel)
	{
		if (_panel.Equals (this))
			return;

		if (!openAdditive && !_panel.openAdditive)
			Close ();
	}

	void OnDisable()
	{
		isOpen = false;
	}


	public UiPanelEvent OnOpenEvent = new UiPanelEvent();
	public UiPanelEvent OnCloseEvent = new UiPanelEvent();

}

[System.Serializable]
public class UiPanelEvent : UnityEvent<UiPanel>
{
	
}
}