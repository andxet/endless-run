using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using EndlessRun.UI;
using EndlessRun.Player;
using EndlessRun.Variables;

namespace EndlessRun.Core
{
   public class GameManager : MonoBehaviour
   {
      public VoidEvent playerDie;
      public VoidEvent restartGame;

      /////////////////////////////////////////////
      void Awake()
      {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
         if (playerDie == null || restartGame == null)
         {
            Debug.LogError("GameManager " + name + ": component not correctly initialized.");
            enabled = false;
            return;
         }
#endif
         playerDie.RegisterForEvent(FreezeGame);
         restartGame.RegisterForEvent(ReloadLevel);
      }

      /////////////////////////////////////////////
      void Start()
      {
      }

      /////////////////////////////////////////////
      void FreezeGame()
      {
         Time.timeScale = 0;
      }

      /////////////////////////////////////////////
      /*public void ExitApplication()
      {
         Application.Quit();
      }*/

      /////////////////////////////////////////////
      public void ReloadLevel()
      {
         Time.timeScale = 1;
         SceneManager.LoadScene("Game");
      }
   }
}
