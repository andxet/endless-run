using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EndlessRun.Variables
{
   [CreateAssetMenu(fileName = "int var", menuName = "Variables/Int", order = 1)]
   public class IntVariable : Variable<int>
   {
      protected override bool NotEqual(int value)
      {
         return value != m_value;
      }
   }
}
