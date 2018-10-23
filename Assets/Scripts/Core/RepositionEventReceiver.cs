using EndlessRun.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRun.Core
{
   public class RepositionEventReceiver : MonoBehaviour
   {
      public FloatEvent repositionEvent;

      /////////////////////////////////////////////
      void Start()
      {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
         if (repositionEvent == null)
         {
            Debug.LogError("RepositionManager " + name + ": component not correctly initialized.");
            enabled = false;
            return;
         }
#endif
         repositionEvent.RegisterForEvent(Reposition);
      }

      void Reposition(float meters)
      {
         transform.Translate(0,0, meters);
      }
   }
}
