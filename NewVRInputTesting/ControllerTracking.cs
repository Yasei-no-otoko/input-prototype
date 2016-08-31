using UnityEngine;

namespace NewVRInputTesting
{
    public class ControllerTracking : MonoBehaviour
    {
        [SerializeField]
        ControllerHandedness m_Handedness;

        Quaternion m_InitialModeRotation;

        void Awake()
        {
            m_InitialModeRotation = transform.localRotation;
        }

        void Update()
        {
            transform.localPosition = TrackedControllersManager.GetControllerPosition(m_Handedness);
            transform.localRotation = TrackedControllersManager.GetControllerRotation(m_Handedness) * m_InitialModeRotation;
        }
    }
}
