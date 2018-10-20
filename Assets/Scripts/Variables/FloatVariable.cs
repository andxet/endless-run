using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EndlessRun.Variables
{
   [CreateAssetMenu(fileName = "var", menuName = "Variables/Float", order = 1)]
   public class FloatVariable : ScriptableObject
   {
      class FloatVariableEvent : UnityEvent<float> { };
      FloatVariableEvent m_callbacks = new FloatVariableEvent();

      float m_value;

      /////////////////////////////////////////////
      public void SetValue(float value)
      {
         m_value = value;
         m_callbacks.Invoke(value);
      }

      /////////////////////////////////////////////
      public float GetValue()
      {
         return m_value;
      }

      /////////////////////////////////////////////
      public void RegisterForUpdate(UnityAction<float> callback)
      {
         m_callbacks.AddListener(callback);
      }
   }
}
