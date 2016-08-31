namespace UnityEngine.InputNew
{
    /**
     * Event for tracking input.
     */
    public class TrackingEvent : InputEvent
    {
        /**
         * Local position
         */
        public Vector3 localPosition { get; set; }
        /**
         * Local rotation
         */
        public Quaternion localRotation { get; set; }
    }
}
