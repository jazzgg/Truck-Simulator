using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

namespace RacingUI
{
[RequireComponent (typeof(CanvasGroup))]
public class PopUp : MonoBehaviour {

	public static PopUp Inst {
		get {
			if (!m_instance) {
				m_instance = FindObjectOfType (typeof(PopUp)) as PopUp;
				if (!m_instance)
					Debug.LogError ("There needs to be one active PopUp script on a GameObject in your scene.");
			}
		   
			return m_instance;
		}
	}
	private static PopUp m_instance;

	public float fadeSpeed = 5;
	[Header("UI Elements")]
	public Image[] avatars;
	public Text messageText;
	public Button yesButtonEtalon;
	public Button noButtonEtalon;
	public Button okButtonEtalon;
	public Button defaultButtonEtalon;
	public Button closeButton;

	public static Button YES{
		get{
			return Inst.yesButtonEtalon;
		}
	}
	public static Button NO{
		get{
			return Inst.noButtonEtalon;
		}
	}
	public static Button OK{
		get{
			return Inst.okButtonEtalon;
		}
	}
	public static Button DEFAULT{
		get{
			return Inst.defaultButtonEtalon;
		}
	}

	private CanvasGroup cGroup;
	private List<Button> allButtons = new List<Button>();

	void Awake ()
	{
		if (Inst != null && Inst != this) {
			DestroyImmediate (this.transform.root.gameObject);
			return;
		} else
			DontDestroyOnLoad (this.transform.root.gameObject);


		YES.gameObject.SetActive (false);
		NO.gameObject.SetActive (false);
		OK.gameObject.SetActive (false);
		DEFAULT.gameObject.SetActive (false);
		closeButton.onClick.AddListener (Close);

		cGroup = GetComponent <CanvasGroup> ();
		cGroup.alpha = 0;
		cGroup.interactable = false;
		cGroup.blocksRaycasts = false;
		transform.SetAsLastSibling ();
	}

	public void NewCoice(string _message, int _avatarIndex = 0, params	pButt[] _buttons)
	{
		Clear ();
		messageText.text = _message;
		if(avatars != null) {
			for (int a = 0; a < avatars.Length; a++) {
				avatars [a].gameObject.SetActive (a == _avatarIndex);
			}
		}

		for (int i = 0; i < _buttons.Length; i++) {

			Button newButt = (Button) Instantiate (_buttons[i].etalon, _buttons[i].etalon.transform.position, _buttons[i].etalon.transform.rotation);
			newButt.transform.SetParent (_buttons[i].etalon.transform.parent, false);
		
			if(_buttons [i].action != null)
				newButt.onClick.AddListener (_buttons [i].action);
			if(!string.IsNullOrEmpty (_buttons[i].text))
				newButt.GetComponentInChildren<Text> ().text = _buttons[i].text;

			newButt.onClick.AddListener (Close);
			newButt.gameObject.SetActive (true);
			allButtons.Add (newButt);
		}

		Open ();
	}

	public void Close()
	{
		StopAllCoroutines ();
		StartCoroutine (FadeIn (false));
	}

	public void Open()
	{
		StopAllCoroutines ();
		StartCoroutine (FadeIn (true));
	}

	IEnumerator FadeIn(bool show)
	{
		float desiredAlpha = show ? 1f : 0f;
		cGroup.interactable = show;
		cGroup.blocksRaycasts = show;

		while(Mathf.Abs (desiredAlpha - cGroup.alpha) > 0.05f)
		{
			cGroup.alpha = Mathf.Lerp (cGroup.alpha, desiredAlpha, Time.deltaTime * fadeSpeed);
			yield return null;
		}
		cGroup.alpha = desiredAlpha;
		yield return null;
	}

	public void Clear()
	{
		for (int i = 0; i < allButtons.Count; i++) {
			Destroy (allButtons[i].gameObject);
		}
		allButtons.Clear ();
		messageText.text = string.Empty;
	}
		

}


public class pButt 
{
	public Button etalon = null;
	public UnityAction action = null;
	public string text;

	public pButt ()
	{
		etalon = PopUp.OK;
	}

	public pButt (Button _etalon)
	{
		etalon = _etalon;
		if (etalon == null)
			etalon = PopUp.OK;
	}

	public pButt (Button _etalon, UnityAction _action)
	{
		etalon = _etalon;
		action = _action;

		if (etalon == null)
			etalon = PopUp.OK;
	}

	public pButt (Button _etalon, UnityAction _action, string _text)
	{
		etalon = _etalon;
		action = _action;
		text = _text;

		if (etalon == null)
			etalon = PopUp.OK;
	}

}
}