using EndlessRun.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRun.Core
{
   public class ScoreManager : MonoBehaviour
   {
      public FloatVariable meters;
      public IntVariable score;
      public IntEvent powerupEvent;
      public IntEvent repositionEvent;
      public float multiplier = 0.10f;

      int m_score = 0;

      /////////////////////////////////////////////
      void Awake()
      {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
         if (meters == null || score == null || powerupEvent == null)
         {
            Debug.LogError("ScoreManager " + name + ": component not correctly initialized.");
            enabled = false;
            return;
         }
#endif
         score.SetValue(0);
         meters.RegisterForUpdate(ComputeScore);
         powerupEvent.RegisterForEvent(RegisterPowerupPoints);
      }

      /////////////////////////////////////////////
      void ComputeScore(float meters)
      {
         score.SetValue(m_score + (int)(meters * multiplier));
      }

      /////////////////////////////////////////////
      void RegisterPowerupPoints(int points)
      {
         m_score += points;
      }

      /////////////////////////////////////////////
      void RepositionEvent(int meters)
      {
         //Should be a negative value...
         m_score -= meters;
      }
   }
}
