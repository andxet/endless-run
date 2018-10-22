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
      public float multiplier = 0.10f;

      /////////////////////////////////////////////
      void Awake()
      {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
         if (meters == null || score == null)
         {
            Debug.LogError("ScoreManager " + name + ": component not correctly initialized.");
            enabled = false;
            return;
         }
#endif
         score.SetValue(0);
         meters.RegisterForUpdate(ComputeScore);
      }

      /////////////////////////////////////////////
      void Start()
      {
      }

      /////////////////////////////////////////////
      void Update()
      {

      }


      /////////////////////////////////////////////
      void ComputeScore(float meters)
      {
         score.SetValue((int)(meters * multiplier));
      }
   }
}
