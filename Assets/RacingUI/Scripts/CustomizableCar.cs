using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacingUI
{
	public class CustomizableCar : MonoBehaviour {

		public Vector3 rotateAxis;
		public List<MeshRenderer> bodys;
		public int bodyMatID;
		public List<MeshRenderer> glasses;
		public int glassMatID;
		public List<MeshRenderer> wheels;
		public int wheelMatID;


		public void SetBodyColor(Color color)
		{
			bodys.ForEach (o => o.materials[bodyMatID].SetColor ("_Color", color));
		}
		public void SetGlassColor(Color color)
		{
			glasses.ForEach (o => o.materials[glassMatID].SetColor ("_Color", color));
		}
		public void SetWheelsColor(Color color)
		{
			wheels.ForEach (o => o.materials[wheelMatID].SetColor ("_Color", color));
		}

		void Update ()
		{
			transform.Rotate (rotateAxis);
		}

	}

}

