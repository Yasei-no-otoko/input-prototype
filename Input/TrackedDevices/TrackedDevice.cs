using System.Collections.Generic;
using System.Linq;
using Assets.Utilities;

namespace UnityEngine.InputNew
{
    /**
     * Input device that supports positional and rotational tracking.
     */
    public class TrackedDevice : InputDevice
    {
        /**
         * Control supported by a tracked device.
         */
        protected enum TrackedDeviceControl
        {
            // Axes
            LocalPositionX,
            LocalPositionY,
            LocalPositionZ,
            LocalRotationX,
            LocalRotationY,
            LocalRotationZ,
            LocalRotationW,

            // Compounds
            LocalPosition,
            LocalRotation
        }

        /**
         * Default constructor.
         */
        public TrackedDevice() 
            : this("TrackedDevice", null) { }

        /**
         * Constructor given a device name and additional controls.
         * @param deviceName Name of the input device
         * @param additionalControls List of additional input control data
         */
        public TrackedDevice(string deviceName, List<InputControlData> additionalControls)
        {
            this.deviceName = deviceName;
            var controlCount = EnumHelpers.GetValueCount<TrackedDeviceControl>();
            var controls = Enumerable.Repeat(new InputControlData(), controlCount).ToList();

            // Compounds
            controls[(int)TrackedDeviceControl.LocalPosition] = new InputControlData
            {
                name = "Local Position",
                controlType = typeof(Vector3InputControl),
                componentControlIndices = new[] { (int)TrackedDeviceControl.LocalPositionX,
                    (int)TrackedDeviceControl.LocalPositionY, (int)TrackedDeviceControl.LocalPositionZ }
            };
            controls[(int)TrackedDeviceControl.LocalRotation] = new InputControlData
            {
                name = "Local Rotation",
                controlType = typeof(QuaternionInputControl),
                componentControlIndices = new[] { (int)TrackedDeviceControl.LocalRotationX, (int)TrackedDeviceControl.LocalRotationY,
                    (int)TrackedDeviceControl.LocalRotationZ, (int)TrackedDeviceControl.LocalRotationW }
            };

            // Axes
            controls[(int)TrackedDeviceControl.LocalPositionX] = new InputControlData { name = "Local Position X", controlType = typeof(AxisInputControl) };
            controls[(int)TrackedDeviceControl.LocalPositionY] = new InputControlData { name = "Local Position Y", controlType = typeof(AxisInputControl) };
            controls[(int)TrackedDeviceControl.LocalPositionZ] = new InputControlData { name = "Local Position Z", controlType = typeof(AxisInputControl) };
            controls[(int)TrackedDeviceControl.LocalRotationX] = new InputControlData { name = "Local Rotation X", controlType = typeof(AxisInputControl) };
            controls[(int)TrackedDeviceControl.LocalRotationY] = new InputControlData { name = "Local Rotation Y", controlType = typeof(AxisInputControl) };
            controls[(int)TrackedDeviceControl.LocalRotationZ] = new InputControlData { name = "Local Rotation Z", controlType = typeof(AxisInputControl) };
            controls[(int)TrackedDeviceControl.LocalRotationW] = new InputControlData { name = "Local Rotation W", controlType = typeof(AxisInputControl) };

            if (additionalControls != null)
            {
                controls.AddRange(additionalControls);
            }

            SetControls(controls);
        }

        protected int GetTotalControlCount()
        {
            return EnumHelpers.GetValueCount<TrackedDeviceControl>();
        }

        // Axis controls
        public AxisInputControl localPositionX { get { return (AxisInputControl)this[(int)TrackedDeviceControl.LocalPositionX]; } }
        public AxisInputControl localPositionY { get { return (AxisInputControl)this[(int)TrackedDeviceControl.LocalPositionY]; } }
        public AxisInputControl localPositionZ { get { return (AxisInputControl)this[(int)TrackedDeviceControl.LocalPositionZ]; } }
        public AxisInputControl localRotationX { get { return (AxisInputControl)this[(int)TrackedDeviceControl.LocalRotationX]; } }
        public AxisInputControl localRotationY { get { return (AxisInputControl)this[(int)TrackedDeviceControl.LocalRotationY]; } }
        public AxisInputControl localRotationZ { get { return (AxisInputControl)this[(int)TrackedDeviceControl.LocalRotationZ]; } }
        public AxisInputControl localRotationW { get { return (AxisInputControl)this[(int)TrackedDeviceControl.LocalRotationW]; } }

        // Compound controls
        public Vector3InputControl localPosition { get { return (Vector3InputControl)this[(int)TrackedDeviceControl.LocalPosition]; } }
        public QuaternionInputControl localRotation { get { return (QuaternionInputControl)this[(int)TrackedDeviceControl.LocalRotation]; } }

        public override bool ProcessEventIntoState(InputEvent inputEvent, InputState intoState)
        {
            if (base.ProcessEventIntoState(inputEvent, intoState))
            {
                return true;
            }

            var consumed = false;

            var trackingEvent = inputEvent as TrackingEvent;
            if (trackingEvent != null)
            {
                consumed |= intoState.SetCurrentValue((int)TrackedDeviceControl.LocalPositionX, trackingEvent.localPosition.x);
                consumed |= intoState.SetCurrentValue((int)TrackedDeviceControl.LocalPositionY, trackingEvent.localPosition.y);
                consumed |= intoState.SetCurrentValue((int)TrackedDeviceControl.LocalPositionZ, trackingEvent.localPosition.z);
                consumed |= intoState.SetCurrentValue((int)TrackedDeviceControl.LocalRotationX, trackingEvent.localRotation.x);
                consumed |= intoState.SetCurrentValue((int)TrackedDeviceControl.LocalRotationY, trackingEvent.localRotation.y);
                consumed |= intoState.SetCurrentValue((int)TrackedDeviceControl.LocalRotationZ, trackingEvent.localRotation.z);
                consumed |= intoState.SetCurrentValue((int)TrackedDeviceControl.LocalRotationW, trackingEvent.localRotation.w);
            }

            return consumed;
        }
    }
}
