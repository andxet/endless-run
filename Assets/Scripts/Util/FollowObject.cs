using UnityEngine;

namespace EndlessRun.Util
{
   public class FollowObject : MonoBehaviour
   {
      public GameObject objectToFollow;
      public Vector3 potisionMultiplier = Vector3.one;
      public bool useCurrentPositionAsOffset = true;

      Vector3 m_offset;

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

         m_offset = transform.position - objectToFollow.transform.position;
      }

      /////////////////////////////////////////////
      void Update()
      {
         transform.position = Vector3.Scale(potisionMultiplier, objectToFollow.transform.position);
         if (useCurrentPositionAsOffset)
            transform.position += m_offset;
      }
   }
}
