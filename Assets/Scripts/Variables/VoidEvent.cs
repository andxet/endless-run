using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EndlessRun.Variables
{
   [CreateAssetMenu(fileName = "event", menuName = "Variables/Void Event", order = 1)]
   public class VoidEvent : ScriptableObject
   {
      class VoidEventEvent : UnityEvent { };
      VoidEventEvent m_callbacks = new VoidEventEvent();

      /////////////////////////////////////////////
      public void Invoke()
      {
         m_callbacks.Invoke();
      }

      /////////////////////////////////////////////
      public void RegisterForEvent(UnityAction callback)
      {
         m_callbacks.AddListener(callback);
      }
   }
}
