using UnityEngine;
using System.Collections;

namespace UnityEngine.InputNew
{
    public class OVRInputToEvents : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            OVRInput.Update();
            SendTouchControllerEvents();
        }

        void SendTouchControllerEvents()
        {
            for (TrackedController.Handedness hand = TrackedController.Handedness.Left;
                (int)hand <= (int)TrackedController.Handedness.Right;
                hand++)
            {
                
            }
        }
    }
}
