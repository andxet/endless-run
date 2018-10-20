using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRun.Core
{
   public class InputManager : MonoBehaviour
   {
      public enum MouseState
      {
         X_DELTA,
         Y_DELTA
      }

      public enum Command
      {
         LEFT,
         RIGHT,
         PAUSE
      }

      public float deltaLimit = 5;

      protected Dictionary<Command, bool> currentCommandState = new Dictionary<Command, bool>();

      bool m_mousePressed;
      float m_mousePressedPosition;
      bool m_commandSent;

      /////////////////////////////////////////////
      private void Awake()
      {
         currentCommandState.Add(Command.LEFT, false);
         currentCommandState.Add(Command.RIGHT, false);
         currentCommandState.Add(Command.PAUSE, false);
         m_mousePressed = false;
         m_commandSent = false;
      }

      /////////////////////////////////////////////
      void Start()
      {

      }

      /////////////////////////////////////////////
      void Update()
      {
         currentCommandState[Command.PAUSE] = Input.GetButtonDown("Pause");
         currentCommandState[Command.LEFT] = false;
         currentCommandState[Command.RIGHT] = false;

         //The command is triggered when the delta movement is greater than a certain value.
         //This way, instead than trigger when the mouse button is up, the ship is more reactive.
         //To trigger again, the user must release the mouse button then press it again.
         if (m_mousePressed)
         {
            if (Input.GetMouseButtonUp(0))
            {
               m_mousePressed = false;
               m_commandSent = false;
            }
            if (Input.GetMouseButtonUp(0))
            {
               m_mousePressed = false;
               m_commandSent = false;
            }
            else
            {
               if (!m_commandSent)
               {
                  float delta = Input.mousePosition.x - m_mousePressedPosition;
                  if (Mathf.Abs(delta) >= deltaLimit)
                  {
                     m_commandSent = true;
                     if (delta < 0)
                        currentCommandState[Command.LEFT] = true;
                     else
                        currentCommandState[Command.RIGHT] = true;
                  }
               }
            }
         }
         else if (Input.GetMouseButtonDown(0))
         {
            m_mousePressed = true;
            m_mousePressedPosition = Input.mousePosition.x;
         }

#if DEBUG
         if (currentCommandState[Command.LEFT])
            Debug.Log("Left");
         if (currentCommandState[Command.RIGHT])
            Debug.Log("Right");
         if (currentCommandState[Command.PAUSE])
            Debug.Log("Pause");
#endif
      }

      /////////////////////////////////////////////
      public bool GetCommandState(Command state)
      {
         return currentCommandState[state];
      }
   }
}
