using UnityEngine;
using BzKovSoft.ObjectSlicer;
using System.Diagnostics;
using System;
using System.Collections;

namespace BzKovSoft.ObjectSlicerSamples
{
	/// <summary>
	/// Test class for demonstration purpose
	/// </summary>
	public class SampleKnifeSlicer : MonoBehaviour
	{
#pragma warning disable 0649
		[SerializeField]
		private GameObject _blade;
#pragma warning restore 0649

        bool attack = false;
        GameObject knife_object;

        void Update()
		{
            var knife = _blade.GetComponentInChildren<BzKnife>();
            knife_object = knife.gameObject;

            if (Input.GetMouseButtonDown(0))
			{
				knife.BeginNewSlice();
                attack = true;
                knife_object.GetComponent<MeshRenderer>().enabled = true;

                UnityEngine.Debug.Log(knife.gameObject);
				StartCoroutine(SwingSword());
			}
            else
            {
                attack = false;
                knife_object.GetComponent<MeshRenderer>().enabled = false;
            }
        }

		IEnumerator SwingSword()
		{
			var transformB = _blade.transform;
			transformB.position = gameObject.transform.position;
			transformB.rotation = gameObject.transform.rotation;

			const float seconds = 0.5f;
			for (float f = 0f; f < seconds; f += Time.deltaTime)
			{
				float aY = (f / seconds) * 180 - 90;
				float aX = (f / seconds) * 60 - 30;
				//float aX = 0;

				var r = Quaternion.Euler(aX, -aY, 0);

				transformB.rotation = gameObject.transform.rotation * r;
				yield return null;
			}
		}
	}
}