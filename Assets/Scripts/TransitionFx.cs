// PoorMansGUIFX by UnityCoder.com

using UnityEngine;
using System.Collections;
using EasingLibrary;

namespace PoormansGUIFX
{

	public class TransitionFx : MonoBehaviour {

		public bool x = false;
		public bool y = true;

		public float startDelaySecs=0;

		public int steps = 32;

		public float acceleration = 1.0f;

		public bool moveNegative = true;

		public float offsetX=0;
		public float offsetY=0;

		public EasingType easingType;


		void Start()
		{
			StartCoroutine("DoTransition");
		}


		IEnumerator DoTransition () 
		{


		
			if (!x && !y) x=true;

			Vector3 startPos = transform.position;

			int stepsMinus1 = steps-1;


			// delay
			if (startDelaySecs>0)
			{
				if (x)
				{
					if (moveNegative)
					{
						transform.position = new Vector3(startPos.x+offsetX,startPos.y+offsetY,startPos.z);
					}else{
						transform.position = new Vector3(startPos.x+offsetX,startPos.y+offsetY,startPos.z);
					}
				}else{
					if (moveNegative)
					{
						transform.position = new Vector3(startPos.x-offsetX,startPos.y+offsetY,startPos.z);
					}else{
						transform.position = new Vector3(startPos.x+offsetX,startPos.y+offsetY,startPos.z);
					}
				}
				
				float startTime = Time.time;
				while(Time.time < startTime + startDelaySecs)
				{
					yield return null;
				}
			}


			if (x)
			{
				// X
				for (int i = 1; i < steps; i++)
				{
					float e = Easing.Ease((double)i/stepsMinus1,acceleration, easingType);
					if (moveNegative)
					{
						transform.position = new Vector3(startPos.x-e+offsetX,startPos.y+offsetY,startPos.z);
					}else{
						transform.position = new Vector3(startPos.x+e+offsetX,startPos.y+offsetY,startPos.z);
					}
					yield return null;
				}

			}else{
				// Y
				for (int i = 1; i < steps; i++)
				{
					float e = Easing.Ease((double)i/stepsMinus1,acceleration, easingType);
					if (moveNegative)
					{
						transform.position = new Vector3(startPos.x-offsetX,startPos.y-e+offsetY,startPos.z);
					}else{
						transform.position = new Vector3(startPos.x+offsetX,startPos.y+e+offsetY,startPos.z);
					}
					yield return null;
				}

			}

		} // DoTransition()

	} // class
} // namespace