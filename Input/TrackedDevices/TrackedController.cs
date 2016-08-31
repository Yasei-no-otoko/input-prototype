using System.Collections.Generic;
using System.Linq;
using Assets.Utilities;

namespace UnityEngine.InputNew
{
    public class TrackedController : TrackedDevice
    {
        protected enum TrackedControllerControl
        {
            ThumbStickX,
            ThumbStickY,
            ThumbStickButton,

            Action1,
            Action2,
            Action3,
            Action4,

            Trigger1,
            Trigger2,

            ThumbStick
        }

        int m_ControlIndexOffset;

        static readonly string[] k_Tags = { "Left", "Right" };
        public static string[] Tags { get { return k_Tags; } }

        public enum Handedness { Left, Right }

        public Handedness? Hand { get; set; }

        public override int tagIndex
        {
            get
            {
                if (Hand.HasValue)
                    return (int)Hand.Value;
                return -1;
            }
        }

        public TrackedController()
            : this("TrackedController", null) { }

        public TrackedController(string deviceName, List<InputControlData> additionalControls)
            : base(deviceName, InitializeControls(additionalControls))
        {
            m_ControlIndexOffset = base.GetTotalControlCount();
        }

        static List<InputControlData> InitializeControls(List<InputControlData> additionalControls)
        {
            var controlCount = EnumHelpers.GetValueCount<TrackedControllerControl>();
            var controls = Enumerable.Repeat(new InputControlData(), controlCount).ToList();

            controls[(int)TrackedControllerControl.ThumbStick] = new InputControlData
            {
              name = "Thumb Stick",
              controlType = typeof(Vector2InputControl),
              componentControlIndices = new [] { (int)TrackedControllerControl.ThumbStickX, (int)TrackedControllerControl.ThumbStickY }
            };

            // Buttons
            controls[(int)TrackedControllerControl.ThumbStickButton] = new InputControlData { name = "Thumb Stick Button", controlType = typeof(ButtonInputControl) };
            controls[(int)TrackedControllerControl.Action1] = new InputControlData { name = "Action 1", controlType = typeof(ButtonInputControl) };
            controls[(int)TrackedControllerControl.Action2] = new InputControlData { name = "Action 2", controlType = typeof(ButtonInputControl) };
            controls[(int)TrackedControllerControl.Action3] = new InputControlData { name = "Action 3", controlType = typeof(ButtonInputControl) };
            controls[(int)TrackedControllerControl.Action4] = new InputControlData { name = "Action 4", controlType = typeof(ButtonInputControl) };

            // Axes
            controls[(int)TrackedControllerControl.ThumbStickX] = new InputControlData { name = "Thumb Stick X", controlType = typeof(AxisInputControl) };
            controls[(int)TrackedControllerControl.ThumbStickY] = new InputControlData { name = "Thumb Stick Y", controlType = typeof(AxisInputControl) };
            controls[(int)TrackedControllerControl.Trigger1] = new InputControlData { name = "Trigger 1", controlType = typeof(AxisInputControl) };
            controls[(int)TrackedControllerControl.Trigger2] = new InputControlData { name = "Trigger 2", controlType = typeof(AxisInputControl) };

            if (additionalControls != null)
            {
                controls.AddRange(additionalControls);
            }

            return controls;
        }

        protected new int GetTotalControlCount()
        {
            return base.GetTotalControlCount() + EnumHelpers.GetValueCount<TrackedControllerControl>();
        }

        public AxisInputControl thumbStickX
        {
            get
            {
                return (AxisInputControl)this[m_ControlIndexOffset + (int)TrackedControllerControl.ThumbStickX];
            }
        }

        public AxisInputControl thumbStickY
        {
            get
            {
                return (AxisInputControl)this[m_ControlIndexOffset + (int)TrackedControllerControl.ThumbStickY];
            }
        }

        public ButtonInputControl thumbStickButton
        {
            get
            {
                return (ButtonInputControl)this[m_ControlIndexOffset + (int)TrackedControllerControl.ThumbStickButton];
            }
        }

        public ButtonInputControl action1
        {
            get
            {
                return (ButtonInputControl)this[m_ControlIndexOffset + (int)TrackedControllerControl.Action1];
            }
        }

        public ButtonInputControl action2
        {
            get
            {
                return (ButtonInputControl)this[m_ControlIndexOffset + (int)TrackedControllerControl.Action2];
            }
        }

        public ButtonInputControl action3
        {
            get
            {
                return (ButtonInputControl)this[m_ControlIndexOffset + (int)TrackedControllerControl.Action3];
            }
        }

        public ButtonInputControl action4
        {
            get
            {
                return (ButtonInputControl)this[m_ControlIndexOffset + (int)TrackedControllerControl.Action4];
            }
        }

        public AxisInputControl trigger1
        {
            get
            {
                return (AxisInputControl)this[m_ControlIndexOffset + (int)TrackedControllerControl.Trigger1];
            }
        }

        public AxisInputControl trigger2
        {
            get
            {
                return (AxisInputControl)this[m_ControlIndexOffset + (int)TrackedControllerControl.Trigger2];
            }
        }

        public Vector2InputControl thumbStick
        {
            get
            {
                return (Vector2InputControl)this[m_ControlIndexOffset + (int)TrackedControllerControl.ThumbStick];
            }
        }
    }
}
