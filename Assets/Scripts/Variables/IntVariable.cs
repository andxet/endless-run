using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EndlessRun.Variables
{
   [CreateAssetMenu(fileName = "int var", menuName = "Variables/Int", order = 1)]
   public class IntVariable : ScriptableObject
   {
      class FloatVariableEvent : UnityEvent<int> { };
      FloatVariableEvent m_callbacks = new FloatVariableEvent();

      int m_value;

      /////////////////////////////////////////////
      public void SetValue(int value)
      {
         if(m_value != value)
         {
            m_value = value;
            m_callbacks.Invoke(value);
         }
      }

      /////////////////////////////////////////////
      public float GetValue()
      {
         return m_value;
      }

      /////////////////////////////////////////////
      public void RegisterForUpdate(UnityAction<int> callback)
      {
         m_callbacks.AddListener(callback);
      }
   }
}
