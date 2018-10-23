using EndlessRun.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRun.Core
{
   public class DifficultyManager : MonoBehaviour
   {
      public IntVariable score;
      public float velocityToAdd = 0.1f;
      public int pointDelta = 100;

      float m_previousVelocity;

      /////////////////////////////////////////////
      void Start()
      {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
         if (score == null)
         {
            Debug.LogError("CameraManager " + name + ": component not correctly initialized.");
            enabled = false;
            return;
         }
#endif
         score.RegisterForUpdate(UpdateVelocity);
      }

      private void UpdateVelocity(int score)
      {
         float newVelocity = 1 + score / pointDelta * velocityToAdd;
         if(newVelocity != m_previousVelocity)
         {
            Time.timeScale = newVelocity;
            m_previousVelocity = newVelocity;
         }
      }
   }
}
