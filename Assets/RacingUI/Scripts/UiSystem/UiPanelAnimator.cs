using UnityEngine;
using System.Collections;


namespace RacingUI {
	
	public class UiPanelAnimator : MonoBehaviour {

	[HideInInspector]
	public Animation uiAnimation;


	public AnimationClip onOpenClip;
	public AnimationClip onCloseClip;

	/// <summary>
	/// Opening animation lenght in seconds.
	/// </summary>
	/// <value>Animation lenght.</value>
	public float OpenTimeLenght{
		get{
			return onOpenClip != null ? onOpenClip.length : -1;
		}
	}

	/// <summary>
	/// Closing animation lenght in seconds.
	/// </summary>
	/// <value>Animation lenght.</value>
	public float CloseTimeLenght{
		get{
			return onCloseClip != null ? onCloseClip.length : -1;
		}
	}


	public bool isPlaying{
		get{
			return inited && uiAnimation.isPlaying;
		}
	}

	public bool isOpened { get; private set;}

	private bool inited{
		get{
			return uiAnimation != null;
		}
	}

	void Awake()
	{
		Init ();
	}


	public void PlayOpen()
	{
		if (!inited)
			return;
		isOpened = true;
		
		uiAnimation.Play (onOpenClip.name);
	}

	public void PlayClose()
	{
		if (!inited)
			return;
		isOpened = false;

		uiAnimation.Play (onCloseClip.name);

	}

	public void PlayToggle()
	{
		if (isOpened)
			PlayClose ();
		else
			PlayOpen ();
	}

	private void Init()
	{
			uiAnimation = GetComponent <Animation> ();

			if (uiAnimation == null)
				uiAnimation = gameObject.AddComponent <Animation> ();

			uiAnimation.playAutomatically = false;

		if (onOpenClip != null && (uiAnimation.GetClip (onOpenClip.name) == null))
			uiAnimation.AddClip (onOpenClip, onOpenClip.name);

		if (onCloseClip != null  && (uiAnimation.GetClip (onCloseClip.name) == null))
			uiAnimation.AddClip (onCloseClip, onCloseClip.name);
	
	}

}
}