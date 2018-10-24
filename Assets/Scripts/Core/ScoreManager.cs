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
      public FloatEvent repositionEvent;
      public VoidEvent playerDieEvent;
      public VoidEvent highScoreEvent;
      public IntVariable highScoreValue;
      public float multiplier = 0.10f;

      int m_score = 0;
      const string PREFS_STRING = "score";

      /////////////////////////////////////////////
      void Awake()
      {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
         if (meters == null || score == null || powerupEvent == null || playerDieEvent == null || highScoreEvent == null || highScoreValue == null)
         {
            Debug.LogError("ScoreManager " + name + ": component not correctly initialized.");
            enabled = false;
            return;
         }
#endif
         score.SetValue(0);
         meters.RegisterForUpdate(ComputeScore);
         powerupEvent.RegisterForEvent(RegisterPowerupPoints);
         repositionEvent.RegisterForEvent(RepositionEvent);
         playerDieEvent.RegisterForEvent(SaveScore);
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
      void RepositionEvent(float meters)
      {
         //Should be a negative value...
         m_score -= (int)(meters * multiplier);
      }

      /////////////////////////////////////////////
      void SaveScore()
      {
         int currentScore = score.GetValue();
         if (PlayerPrefs.HasKey("score"))
         {
            int highScore = PlayerPrefs.GetInt(PREFS_STRING);
            if(currentScore > highScore)
            {
               highScore = currentScore;
               PlayerPrefs.SetInt(PREFS_STRING, currentScore);
               highScoreEvent.Invoke();
            }
            highScoreValue.SetValue(highScore);
         }
         else
         {
            highScoreValue.SetValue(currentScore);
            PlayerPrefs.SetInt(PREFS_STRING, currentScore);

         }

         PlayerPrefs.Save();
      }
   }
}
