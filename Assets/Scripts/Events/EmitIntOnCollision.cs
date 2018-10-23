using EndlessRun.Util;
using EndlessRun.Variables;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRun.Events
{
   public class EmitIntOnCollision : MonoBehaviour
   {
      public List<string> activeOnCollisionWithTag = new List<string>();
      public int score;
      public IntEvent collisionEvent;

      /////////////////////////////////////////////
      private void OnTriggerEnter(Collider other)
      {
         if (collisionEvent != null)
            foreach (string tag in activeOnCollisionWithTag)
            {
               if (other.gameObject.CompareTag(tag))
                  collisionEvent.Invoke(score);
            }
      }
   }
}
