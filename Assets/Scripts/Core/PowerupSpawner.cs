using EndlessRun.Util;
using EndlessRun.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRun.Core
{
   public class PowerupSpawner : MonoBehaviour
   {
      public PositionEvent listenEvent;
      public PoolElement powerupPrefab;
      public int poolSize = 20;
      ObjectPool m_powerups;

      /////////////////////////////////////////////
      void Awake()
      {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
         if (listenEvent == null || powerupPrefab == null)
         {
            Debug.LogError("PowerupManager " + name + ": component not correctly initialized.");
            enabled = false;
            return;
         }
#endif
         m_powerups = new ObjectPool(poolSize, powerupPrefab);
         listenEvent.RegisterForEvent(SpawnPowerup);
      }

      /////////////////////////////////////////////
      void SpawnPowerup(Vector3 position)
      {
         PoolElement powerup = m_powerups.Create();
         powerup.transform.position = position;
      }
   }
}
