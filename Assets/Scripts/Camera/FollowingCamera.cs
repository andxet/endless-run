using UnityEngine;

namespace EndlessRun.Camera
{
   public class FollowingCamera : MonoBehaviour
   {
      public GameObject objectToFollow;

      /////////////////////////////////////////////
      void Start()
      {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
         if (objectToFollow == null)
         {
            Debug.LogError("CameraManager " + name + ": component not correctly initialized.");
            enabled = false;
            return;
         }
#endif
      }

      /////////////////////////////////////////////
      void Update()
      {
         Vector3 position = transform.position;
         position.z = objectToFollow.transform.position.z;
         transform.position = position;
      }
   }
}
