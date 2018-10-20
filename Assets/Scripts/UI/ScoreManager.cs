using EndlessRun.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessRun.UI
{
   [RequireComponent(typeof(Text))]
   public class ScoreManager : MonoBehaviour
   {
      public FloatVariable meters;
      Text m_text;


      /////////////////////////////////////////////
      void Start()
      {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
         if (meters == null)
         {
            Debug.LogError("ScoreManager " + name + ": component not correctly initialized.");
            enabled = false;
            return;
         }
#endif
         meters.RegisterForUpdate(UpdateScore);
         m_text = GetComponent<Text>();
      }

      /////////////////////////////////////////////
      void UpdateScore(float meters)
      {
         m_text.text = "Score: " + (int)ComputeScore(meters);
      }

      /////////////////////////////////////////////
      float ComputeScore(float meters)
      {
         return meters / 10;
      }
   }
}
