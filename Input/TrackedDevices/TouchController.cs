using System.Collections.Generic;

namespace UnityEngine.InputNew
{
    public class TouchController : TrackedController
    {
        public TouchController()
            : this("TouchController", null) { }

        public TouchController(string deviceName, List<InputControlData> additionalControls)
            : base(deviceName, additionalControls) { }

        public new static string[] Tags { get { return TrackedController.Tags; } }
    }
}
