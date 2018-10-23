using EndlessRun.Util;
using EndlessRun.Variables;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRun.Spawnable
{
   public class DestroyOnCollision : PoolElement
   {
      public List<string> activeOnCollisionWithTag = new List<string>();

      /////////////////////////////////////////////
      private void OnTriggerEnter(Collider other)
      {
         foreach(string tag in activeOnCollisionWithTag)
         {
            if (other.gameObject.CompareTag(tag))
               Destroy();
         }
      }
   }
}
