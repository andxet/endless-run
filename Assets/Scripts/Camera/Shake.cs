using EndlessRun.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRun.Util
{
   public class Shake : MonoBehaviour
   {
      public VoidEvent eventToFollow;
      public float duration;
      public float amount;

      /////////////////////////////////////////////
      void Awake()
      {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
         if (eventToFollow == null)
         {
            Debug.LogError("Shake " + name + ": component not correctly initialized.");
            enabled = false;
            return;
         }
#endif
         eventToFollow.RegisterForEvent(ShakeObject);
      }

      /////////////////////////////////////////////
      void ShakeObject()
      {
         StartCoroutine("ShakeCoroutine");
      }

      /////////////////////////////////////////////
      IEnumerator ShakeCoroutine()
      {
         Vector3 origin = transform.position;
         float elapsed = 0;

         while (elapsed < duration)
         {
            float x, y;
            x = Random.Range(-1, 1) * amount;
            y = Random.Range(-1, 1) * amount;

            transform.position = new Vector3(x, y, origin.z);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
         }

         transform.position = origin;
      }
   }
}
