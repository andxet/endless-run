using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EndlessRun.Variables
{
   [CreateAssetMenu(fileName = "float var", menuName = "Variables/Float", order = 1)]
   public class FloatVariable : Variable<float>
   {
      protected override bool NotEqual(float value)
      {
         return Math.Abs(m_value - value) > 0.00001;
      }
   }
}
