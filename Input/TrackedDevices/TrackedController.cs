using System.Collections.Generic;

namespace UnityEngine.InputNew
{
    public class TrackedController : TrackedDevice
    {
        //=================
        // Private
        //=================

        const string k_TrackedControllerName = "Tracked Controller";
        static readonly string[] k_Tags = { "Left", "Right" };

        //=================
        // Public
        //=================

        public static string[] Tags { get { return k_Tags; } }
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
            : this(k_TrackedControllerName, null) { }

        public TrackedController(string deviceName, List<InputControlData> additionalControls)
            : base(deviceName, additionalControls) { }
    }
}
