using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace RacingUI
{
	
[ExecuteInEditMode]
[RequireComponent(typeof(Button))]
public class UiToggle : MonoBehaviour {


	public UiToggleGroup toggleGroup;

	public Image Graphic;
	public Text Text;
	public Image Pointer;

	public bool isEnabled = false;
	public bool autoAssign = true;

	void Awake()
	{
		GetComponent <Button>().onClick.AddListener (Select);

		if (isEnabled)	Select ();
	}

	#if UNITY_EDITOR
	void Start()
	{
		if (Application.isPlaying)
			return;

		if (autoAssign) 
		{
			if (Graphic == null)
				Graphic = GetComponent <Image> ();

			if (Text == null)
				Text = GetComponentInChildren <Text> ();

			if (Pointer == null)
				Pointer = GetComponentInChildren <Image> ().transform != transform ? GetComponentInChildren <Image> () : null;
		}


		if (toggleGroup == null)
			toggleGroup = transform.parent.GetComponent <UiToggleGroup> ();

		if (toggleGroup != null && !toggleGroup.toggles.Contains (this))
			toggleGroup.toggles.Add (this);


	}
	#endif

	public void SetParams(bool _enable, Vector4 _color)
	{
		if (Graphic) Graphic.color = _color;
		if (Text) Text.color = _color;
		if(Pointer)	Pointer.enabled = _enable;

		isEnabled = _enable;
	}

	public void Select()
	{
		toggleGroup.SelectToggle (this);
	}

}

}