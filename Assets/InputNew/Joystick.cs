using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.InputNew
{
	// Must be different from Gamepad as the standardized controls for Gamepads don't
	// work for joysticks.
	public class Joystick
		: InputDevice
	{
		#region Constructors

		public Joystick()
			: this("Joystick", null) { }

		public Joystick(string deviceName, List<InputControlData> additionalControls)
		{
			this.deviceName = deviceName;
			var controls = new List<InputControlData>();

			// TODO create default joystick buttons and axes

			if (additionalControls != null)
				controls.AddRange(additionalControls);
			
			SetControls(controls);
		}

		#endregion

		public override bool ProcessEventIntoState(InputEvent inputEvent, InputState intoState)
		{
			var consumed = false;

			var genericEvent = inputEvent as GenericControlEvent;
			if (genericEvent != null)
			{
				consumed |= intoState.SetCurrentValue(genericEvent.controlIndex, genericEvent.value);
			}

			if (consumed)
				return true;

			return base.ProcessEventIntoState(inputEvent, intoState);
		}
	}
}
