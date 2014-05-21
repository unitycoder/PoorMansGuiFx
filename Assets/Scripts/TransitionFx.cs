// PoorMansGUIFX by UnityCoder.com

using UnityEngine;
using System.Collections;
using EasingLibrary;

namespace PoormansGUIFX
{

	public enum Transition
	{
		FromCurrentPosition,
		ToCurrentPosition
	}


	public enum Direction
	{
		Up,
		Right,
		Down,
		Left
	}



	public class TransitionFx : MonoBehaviour {

		public Transition transition;
		public Direction direction;
		public float startDelaySecs=0;
		public int steps = 32;
		public float acceleration = 1.0f;
		public EasingType easingType;

		private float offsetX=0;
		private float offsetY=0;

		// TODO: enum for startup type (Start(), OnEnable(), Custom()..


		// TODO: support for OnEnable and other events also
		void Start()
		{
			StartCoroutine("DoTransition");
		}



		// actual transition happens here
		IEnumerator DoTransition () 
		{
			//if (!x && !y) x=true;

			Vector3 startPos = transform.position;

			int stepsMinus1 = steps-1;

			// calculate automatic offsets
			switch (transition)
			{
			case Transition.FromCurrentPosition:
				offsetX = -Easing.Ease(0,acceleration, easingType);
				offsetY = -Easing.Ease(0,acceleration, easingType);
				break;
			case Transition.ToCurrentPosition:
				offsetX = -Easing.Ease(1,acceleration, easingType);
				offsetY = -Easing.Ease(1,acceleration, easingType);
				break;
			}

			// fix offsets
			switch (direction)
			{
				case Direction.Up:
					offsetX = 0;
					break;
				case Direction.Down:
					offsetX = 0;
					offsetY = -offsetY;
					break;
				case Direction.Left:
					offsetX = -offsetX;
					offsetY = 0;
					break;
				case Direction.Right:
					offsetY = 0;
					break;
			}

			// set initial object position
			transform.position = new Vector3(startPos.x+offsetX,startPos.y+offsetY,startPos.z);


			// startup delay
			if (startDelaySecs>0)
			{
				
				float startTime = Time.time;
				while(Time.time < startTime + startDelaySecs)
				{
					yield return null;
				}
			} // startupdelay



			// main transition/easing loop
			for (int i = 1; i < steps; i++)
			{
				// TODO: remap i from 0-steps into 0-1
				float e = Easing.Ease((double)i/stepsMinus1,acceleration, easingType);

				Vector3 newPos = new Vector3(startPos.x + offsetX, startPos.y + offsetY , startPos.z);

				switch (direction)
				{
				case Direction.Up:
					newPos+= new Vector3(0,e,0);
					break;
				case Direction.Down:
					newPos+= new Vector3(0,-e,0);
					break;
				case Direction.Left:
					newPos+= new Vector3(-e,0,0);
					break;
				case Direction.Right:
					newPos+= new Vector3(e,0,0);
					break;
				}

				transform.position = newPos;

				yield return null;

			} // transition for loop

		} // DoTransition()

	} // class
} // namespace