using EndlessRun.Core;
using UnityEngine;

namespace EndlessRun.Player
{
   public class HorizontalMovement : MonoBehaviour
   {
      public InputManager inputManager;
      public float movementTime = 0.2f;
      public float m_lanesDistance = 2;

      float m_movementRemainingTime;
      float m_from, m_to;

      /////////////////////////////////////////////
      void Start()
      {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
         if (inputManager == null || m_lanesDistance <= 0 || movementTime <= 0)
         {
            Debug.LogError("HorizontalMovement " + name + ": component not correctly initialized.");
            enabled = false;
            return;
         }
#endif
      }

      /////////////////////////////////////////////
      void Update()
      {
         if (Time.timeScale > 0)
         {
            Vector3 newPosition = transform.position;

            if (m_movementRemainingTime > 0)
            {
               m_movementRemainingTime -= Time.deltaTime;
               newPosition.x = Mathf.Lerp(m_from, m_to, 1 - (1 / movementTime / Time.timeScale * m_movementRemainingTime));
               if (m_movementRemainingTime < 0)
                  m_from = newPosition.x;
            }
            else
            {
               if (inputManager.GetCommandState(InputManager.Command.LEFT))
                  Move(-1);//Move left

               if (inputManager.GetCommandState(InputManager.Command.RIGHT))
                  Move(1);//Move right
            }

            transform.position = newPosition;
         }
      }

      /////////////////////////////////////////////
      void Move(int direction)
      {
         m_to = m_from + m_lanesDistance * direction;
         m_movementRemainingTime = movementTime;
      }
   }
}
