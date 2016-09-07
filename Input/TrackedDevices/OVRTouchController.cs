using System.Collections.Generic;

namespace UnityEngine.InputNew
{
    public class OVRTouchController : TrackedController
    {
        //=================
        // Private
        //=================

        const string k_OVRTouchControllerName = "OVR Touch Controller";

        //=================
        // Public
        //=================

        public new static string[] Tags { get { return TrackedController.Tags; } }

        public OVRTouchController()
            : this(k_OVRTouchControllerName, null) { }

        public OVRTouchController(string deviceName, List<InputControlData> additionalControls)
            : base(deviceName, additionalControls) { }
    }
}
