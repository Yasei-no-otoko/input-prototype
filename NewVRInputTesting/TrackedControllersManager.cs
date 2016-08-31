using UnityEngine;
using UnityEngine.InputNew;

namespace NewVRInputTesting
{
    public class TrackedControllersManager : MonoBehaviour
    {
        static TrackedControllersManager s_Instance;
        TrackedControllersActionMap m_TrackedControllers;

        void Start()
        {
            s_Instance = this;
            var playerInput = GetComponent<PlayerInput>();
            m_TrackedControllers = playerInput.GetActions<TrackedControllersActionMap>();
        }

        public static Vector3 GetControllerPosition(ControllerHandedness hand)
        {
            return hand == ControllerHandedness.Left
                ? s_Instance.m_TrackedControllers.leftPosition.vector3
                : s_Instance.m_TrackedControllers.rightPosition.vector3;
        }

        public static Quaternion GetControllerRotation(ControllerHandedness hand)
        {
            return hand == ControllerHandedness.Left
                ? s_Instance.m_TrackedControllers.leftRotation.quaternion
                : s_Instance.m_TrackedControllers.rightRotation.quaternion;
        }
    }
}
