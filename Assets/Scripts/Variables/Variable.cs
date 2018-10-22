using UnityEngine;
using UnityEngine.Events;

namespace EndlessRun.Variables
{
   public abstract class Variable<T> : ScriptableObject
   {
      class VariableEvent : UnityEvent<T> { };
      VariableEvent m_callbacks = new VariableEvent();

      protected T m_value;

      /////////////////////////////////////////////
      public void SetValue(T value)
      {
         if (NotEqual(value))
         {
            m_value = value;
            m_callbacks.Invoke(value);
         }
      }

      /////////////////////////////////////////////
      protected abstract bool NotEqual(T value);

      /////////////////////////////////////////////
      public T GetValue()
      {
         return m_value;
      }

      /////////////////////////////////////////////
      public void RegisterForUpdate(UnityAction<T> callback)
      {
         m_callbacks.AddListener(callback);
      }
   }
}
