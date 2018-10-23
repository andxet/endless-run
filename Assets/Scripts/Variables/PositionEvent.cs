using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EndlessRun.Variables
{
   [CreateAssetMenu(fileName = "event", menuName = "Variables/Position Event", order = 1)]
   public class PositionEvent : Event<Vector3>
   {   }
}
