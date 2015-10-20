using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.InputNew
{
	public class InputAction
		: ScriptableObject
	{
		#region Fields

		[SerializeField]
		InputControlData m_ControlData;

		#endregion

		#region Public Properties

		public InputControlData controlData
		{
			get { return m_ControlData; }
			set
			{
				m_ControlData = value;
				name = m_ControlData.name;
			}
		}

		// This is one entry for each control scheme (matching indices) -- except if there are no bindings for the entry.
		public List<ControlBinding> bindings = new List<ControlBinding>();

		[NonSerialized]
		public int controlIndex;

		public override string ToString()
		{
			return string.Format("({0}, bindings:{1})", controlData.name, bindings.Count);
		}

		#endregion
	}
}