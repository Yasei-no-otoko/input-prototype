using System;
using System.Collections.Generic;

namespace UnityEngine.InputNew
{
    public class OVRInputToEvents : MonoBehaviour
    {
        Dictionary<OVRInput.Controller, Pose> m_LastPoseValues = new Dictionary<OVRInput.Controller, Pose>();

        void Start()
        {
            m_LastPoseValues[OVRInput.Controller.LTouch] = new Pose { position = Vector3.zero, rotation = Quaternion.identity };
            m_LastPoseValues[OVRInput.Controller.RTouch] = new Pose { position = Vector3.zero, rotation = Quaternion.identity };
        }

        void Update()
        {
            OVRInput.Update();
            QueueTouchControllerEvents();
        }

        void QueueTouchControllerEvents()
        {
            foreach (Handedness hand in Enum.GetValues(typeof(Handedness)))
            {
                var controller = hand == Handedness.Left
                    ? OVRInput.Controller.LTouch
                    : OVRInput.Controller.RTouch;
                QueueControllerTrackingEvent(controller);
            }
        }

        void QueueControllerTrackingEvent(OVRInput.Controller ovrController)
        {
            var controllerPosition = OVRInput.GetControllerPositionTracked(ovrController)
                ? OVRInput.GetLocalControllerPosition(ovrController)
                : m_LastPoseValues[ovrController].position;
            var controllerRotation = OVRInput.GetControllerOrientationTracked(ovrController)
                ? OVRInput.GetLocalControllerRotation(ovrController)
                : m_LastPoseValues[ovrController].rotation;
            if (controllerPosition != m_LastPoseValues[ovrController].position ||
                controllerRotation != m_LastPoseValues[ovrController].rotation)
            {
                m_LastPoseValues[ovrController] = new Pose { position = controllerPosition, rotation = controllerRotation };

                var inputEvent = InputSystem.CreateEvent<TrackingEvent>();
                inputEvent.deviceType = typeof(OVRTouchController);
                inputEvent.deviceIndex = GetTouchControllerIndex(ovrController);
                inputEvent.localPose = m_LastPoseValues[ovrController];

                InputSystem.QueueEvent(inputEvent);
            }
        }

        static int GetTouchControllerIndex(OVRInput.Controller ovrController)
        {
            if (ovrController != OVRInput.Controller.LTouch && ovrController != OVRInput.Controller.RTouch)
            {
                Debug.LogError("OVR controller is not LTouch or RTouch. Cannot get its device index.");
                return -1;
            }
            return ovrController == OVRInput.Controller.LTouch
                ? (int)Handedness.Left
                : (int)Handedness.Right;
        }
    }
}
