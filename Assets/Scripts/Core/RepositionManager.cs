#define DEBUG_REPOSITION_MANAGER

using EndlessRun.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRun.Core
{
   public class RepositionManager : MonoBehaviour
   {
      public FloatVariable meters;
      public FloatEvent repositionEvent;
      public float repositionAmount = 1000f;

      /////////////////////////////////////////////
      void Start()
      {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
         if (meters == null || repositionEvent == null || repositionAmount <= 0)
         {
            Debug.LogError("RepositionManager " + name + ": component not correctly initialized.");
            enabled = false;
            return;
         }
#endif
         meters.RegisterForUpdate(CheckForReposition);
      }

      void CheckForReposition(float meters)
      {
         if (meters > repositionAmount)
         {
            repositionEvent.Invoke(-repositionAmount);
#if DEBUG_REPOSITION_MANAGER
            Debug.Log("REPOSITION!!");
#endif
         }
      }
   }
}
