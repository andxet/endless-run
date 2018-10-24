using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EndlessRun.Variables
{
   [CreateAssetMenu(fileName = "event", menuName = "Variables/Event", order = 1)]
   public class Event<T> : ScriptableObject
   {
      class DynamicEvent : UnityEvent<T> { };
      DynamicEvent m_callbacks = new DynamicEvent();

      /////////////////////////////////////////////
      public void Invoke(T value)
      {
         m_callbacks.Invoke(value);
      }

      /////////////////////////////////////////////
      public void RegisterForEvent(UnityAction<T> callback)
      {
         m_callbacks.AddListener(callback);
      }
   }
}
