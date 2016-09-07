using System.Collections.Generic;
using System.Linq;
using Assets.Utilities;

namespace UnityEngine.InputNew
{
    ///
    /// Input device that supports positional and rotational tracking.
    ///
    public class TrackedDevice : InputDevice
    {
        //=================
        // Private
        //=================

        const string k_TrackedDeviceName    = "Tracked Device";
        const string k_LocalPositionXName   = "Local Position X";
        const string k_LocalPositionYName   = "Local Position Y";
        const string k_LocalPositionZName   = "Local Position Z";
        const string k_LocalRotationXName   = "Local Rotation X";
        const string k_LocalRotationYName   = "Local Rotation Y";
        const string k_LocalRotationZName   = "Local Rotation Z";
        const string k_LocalRotationWName   = "Local Rotation W";
        const string k_LocalPositionName    = "Local Position";
        const string k_LocalRotationName    = "Local Rotation";

        //=================
        // Protected
        //=================

        ///
        /// Control supported by a tracked device.
        ///
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

        //=================
        // Public
        //=================

        ///
        /// Default constructor.
        ///
        public TrackedDevice() 
            : this(k_TrackedDeviceName, null) { }

        ///
        /// Constructor given a device name and additional controls.
        ///
        /// @param deviceName Name of the input device
        /// @param additionalControls List of additional input control data
        ///
        public TrackedDevice(string deviceName, List<InputControlData> additionalControls)
        {
            this.deviceName = deviceName;
            var controlCount = EnumHelpers.GetValueCount<TrackedDeviceControl>();
            var controls = Enumerable.Repeat(new InputControlData(), controlCount).ToList();
            
            // Axes
            controls[(int)TrackedDeviceControl.LocalPositionX] = new InputControlData { name = k_LocalPositionXName, controlType = typeof(AxisInputControl) };
            controls[(int)TrackedDeviceControl.LocalPositionY] = new InputControlData { name = k_LocalPositionYName, controlType = typeof(AxisInputControl) };
            controls[(int)TrackedDeviceControl.LocalPositionZ] = new InputControlData { name = k_LocalPositionZName, controlType = typeof(AxisInputControl) };
            controls[(int)TrackedDeviceControl.LocalRotationX] = new InputControlData { name = k_LocalRotationXName, controlType = typeof(AxisInputControl) };
            controls[(int)TrackedDeviceControl.LocalRotationY] = new InputControlData { name = k_LocalRotationYName, controlType = typeof(AxisInputControl) };
            controls[(int)TrackedDeviceControl.LocalRotationZ] = new InputControlData { name = k_LocalRotationZName, controlType = typeof(AxisInputControl) };
            controls[(int)TrackedDeviceControl.LocalRotationW] = new InputControlData { name = k_LocalRotationWName, controlType = typeof(AxisInputControl) };

            // Compounds
            controls[(int)TrackedDeviceControl.LocalPosition] = new InputControlData
            {
                name = k_LocalPositionName,
                controlType = typeof(Vector3InputControl),
                componentControlIndices = new[] { (int)TrackedDeviceControl.LocalPositionX,
                    (int)TrackedDeviceControl.LocalPositionY, (int)TrackedDeviceControl.LocalPositionZ }
            };
            controls[(int)TrackedDeviceControl.LocalRotation] = new InputControlData
            {
                name = k_LocalRotationName,
                controlType = typeof(QuaternionInputControl),
                componentControlIndices = new[] { (int)TrackedDeviceControl.LocalRotationX, (int)TrackedDeviceControl.LocalRotationY,
                    (int)TrackedDeviceControl.LocalRotationZ, (int)TrackedDeviceControl.LocalRotationW }
            };

            if (additionalControls != null)
            {
                controls.AddRange(additionalControls);
            }

            SetControls(controls);
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
                consumed |= intoState.SetCurrentValue((int)TrackedDeviceControl.LocalPositionX, trackingEvent.localPose.position.x);
                consumed |= intoState.SetCurrentValue((int)TrackedDeviceControl.LocalPositionY, trackingEvent.localPose.position.y);
                consumed |= intoState.SetCurrentValue((int)TrackedDeviceControl.LocalPositionZ, trackingEvent.localPose.position.z);
                consumed |= intoState.SetCurrentValue((int)TrackedDeviceControl.LocalRotationX, trackingEvent.localPose.rotation.x);
                consumed |= intoState.SetCurrentValue((int)TrackedDeviceControl.LocalRotationY, trackingEvent.localPose.rotation.y);
                consumed |= intoState.SetCurrentValue((int)TrackedDeviceControl.LocalRotationZ, trackingEvent.localPose.rotation.z);
                consumed |= intoState.SetCurrentValue((int)TrackedDeviceControl.LocalRotationW, trackingEvent.localPose.rotation.w);
            }

            return consumed;
        }
    }
}
