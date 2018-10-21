using EndlessRun.Core;
using UnityEngine;

namespace EndlessRun.Player
{
   public class HorizontalMovement : MonoBehaviour
   {
      [Header("Movement info")]
      public InputManager inputManager;
      public float movementTime = 0.2f;

      [Header("Ground info")]
      public float lanesDistance = 2;
      public int lanesNumber = 5;
      public int startLane = 2;

      float m_movementRemainingTime;
      float m_from, m_to;
      int m_currentLane;

      /////////////////////////////////////////////
      void Start()
      {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
         if (inputManager == null || lanesDistance <= 0 || movementTime <= 0 || lanesNumber <= 0 || startLane < 0 || startLane >= lanesNumber)
         {
            Debug.LogError("HorizontalMovement " + name + ": component not correctly initialized.");
            enabled = false;
            return;
         }
#endif
         m_currentLane = startLane;
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
         int newLane = m_currentLane + direction;
         if (newLane >= lanesNumber || newLane < 0)
            return;
         m_currentLane = newLane;
         m_to = m_from + lanesDistance * direction;
         m_movementRemainingTime = movementTime;
      }
   }
}
