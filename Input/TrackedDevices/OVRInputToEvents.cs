using System;

namespace UnityEngine.InputNew
{
    public class OVRInputToEvents : MonoBehaviour
    {
        const uint k_ControllerCount = 2;
        Vector3[] m_LastPositionValues = new Vector3[k_ControllerCount];
        Quaternion[] m_LastRotationValues = new Quaternion[k_ControllerCount];

        void Update()
        {
            OVRInput.Update();
            QueueTouchControllerEvents();
        }

        void QueueTouchControllerEvents()
        {
            for (var hand = TrackedController.Handedness.Left;
                (int)hand <= (int)TrackedController.Handedness.Right;
                hand++)
            {
                var controller = hand == TrackedController.Handedness.Left
                    ? OVRInput.Controller.LTouch
                    : OVRInput.Controller.RTouch;
                var controllerIndex = controller == OVRInput.Controller.LTouch
                    ? 0
                    : 1;
                QueueControllerTrackingEvent(controller, controllerIndex);
            }
        }

        void QueueControllerButtonEvents(OVRInput.Controller ovrController, int controllerIndex)
        {
            foreach (OVRInput.Button button in Enum.GetValues(typeof(OVRInput.Button)))
            {
                var isDown = OVRInput.GetDown(button, ovrController);
                var isUp = OVRInput.GetUp(button, ovrController);
                
            }
        }

        void QueueControllerTrackingEvent(OVRInput.Controller ovrController, int controllerIndex)
        {
            var position = OVRInput.GetControllerPositionTracked(ovrController)
                ? OVRInput.GetLocalControllerPosition(ovrController)
                : m_LastPositionValues[controllerIndex];
            var rotation = OVRInput.GetControllerOrientationTracked(ovrController)
                ? OVRInput.GetLocalControllerRotation(ovrController)
                : m_LastRotationValues[controllerIndex];
            if (position != m_LastPositionValues[controllerIndex] ||
                rotation != m_LastRotationValues[controllerIndex])
            {
                m_LastPositionValues[controllerIndex] = position;
                m_LastRotationValues[controllerIndex] = rotation;

                var inputEvent = InputSystem.CreateEvent<TrackingEvent>();
                inputEvent.deviceType = typeof(TouchController);
                inputEvent.deviceIndex = controllerIndex;
                inputEvent.localPosition = position;
                inputEvent.localRotation = rotation;

                InputSystem.QueueEvent(inputEvent);
            }
        }
    }
}
