using EndlessRun.Variables;
using UnityEngine;

namespace EndlessRun.Player
{
   public class PlayerManager : MonoBehaviour
   {
      public FloatVariable meters;
      public VoidEvent playerDie;
      float m_startPosition;

      /////////////////////////////////////////////
      void Awake()
      {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
         if (meters == null)
         {
            Debug.LogError("PlayerManager " + name + ": component not correctly initialized.");
            enabled = false;
            return;
         }
#endif
         m_startPosition = transform.position.z;
         meters.SetValue(0);
      }
      
      /////////////////////////////////////////////
      void Update()
      {
         meters.SetValue(transform.position.z - m_startPosition);
      }

      /////////////////////////////////////////////
      private void OnTriggerEnter(Collider other)
      {
         if (other.gameObject.CompareTag("Enemy"))
            playerDie.Invoke();
      }
   }
}
