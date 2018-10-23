using EndlessRun.Util;
using EndlessRun.Variables;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRun.Spawnable
{
   public class EmitPositionOnCollision : MonoBehaviour
   {
      public List<string> activeOnCollisionWithTag = new List<string>();
      public PositionEvent collisionEvent;

      /////////////////////////////////////////////
      private void OnTriggerEnter(Collider other)
      {
         if (collisionEvent != null)
            foreach (string tag in activeOnCollisionWithTag)
            {
               if (other.gameObject.CompareTag(tag))
                  collisionEvent.Invoke(transform.position);
            }
      }
   }
}
