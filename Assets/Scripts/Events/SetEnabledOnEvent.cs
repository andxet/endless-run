using EndlessRun.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRun.Events
{
   public class SetEnabledOnEvent : MonoBehaviour
   {
      public VoidEvent eventToSubscribe;
      public Transform objectToActivate;
      public bool enableOnEvent = true;

      /////////////////////////////////////////////
      void Awake()
      {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
         if (eventToSubscribe == null)
         {
            Debug.LogError("SetEnabledOnEvent " + name + ": component not correctly initialized.");
            enabled = false;
            return;
         }
#endif
         eventToSubscribe.RegisterForEvent(EventReceived);
      }

      private void EventReceived()
      {
         if (objectToActivate == null)
            gameObject.SetActive(enableOnEvent);
         else
            objectToActivate.gameObject.SetActive(enableOnEvent);
      }
   }
}
