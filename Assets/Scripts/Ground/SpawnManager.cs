//#define DEBUG_SPAWN_MANAGER

using EndlessRun.Util;
using EndlessRun.Variables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EndlessRun.Ground
{
   public class SpawnManager : MonoBehaviour
   {
      public FloatVariable objectToFollow;
      public GroundManager groundManager;
      
      [Header("Infinite ground info")]
      public float spawnZDistance = 10.0f;
      public int spawnDelay = 3;
      public int spawnAfterPlayer = 8;
      public int destroyBeforPlayer = 4;

      public List<PoolElement> availableObjectToSpawn = new List<PoolElement>();

      List<ObjectPool> m_objectPools = new List<ObjectPool>();
      Queue<List<PoolElement>> m_spawnedElements;
      float m_xOffset;

      //Current reference to create and delete things
      float z_reference;

      // Use this for initialization
      void Start()
      {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
         if (objectToFollow == null ||
            groundManager == null ||
            spawnZDistance <= 0 ||
            spawnDelay < 0 ||
            spawnAfterPlayer < 0 ||
            availableObjectToSpawn == null ||
            availableObjectToSpawn.Count == 0)
         {
            Debug.LogError("SpawnManager " + name + ": component not correctly initialized.");
            enabled = false;
            return;
         }
#endif
         m_spawnedElements = new Queue<List<PoolElement>>();

         z_reference = objectToFollow.GetValue();

         //since the x=0 is located on the third line, an offset to every object is needed
         m_xOffset = groundManager.lanes / 2 * -groundManager.lanesDistance;

         int maxElements = (spawnAfterPlayer + 1) * groundManager.lanes;
         foreach (PoolElement element in availableObjectToSpawn)
         {
            m_objectPools.Add(new ObjectPool(maxElements, element));
         }


         for (int i = spawnDelay; i < spawnAfterPlayer + 1; i++)
         {
            SpawnRow(z_reference + spawnZDistance * i);
         }
      }

      // Update is called once per frame
      void Update()
      {
         if(objectToFollow.GetValue() - z_reference > spawnZDistance)
         {
            z_reference += spawnZDistance;
            SpawnRow(z_reference + spawnZDistance * spawnAfterPlayer);

            //destroy older elements
            if(m_spawnedElements.Count > destroyBeforPlayer + spawnAfterPlayer)
            {
               List<PoolElement> objectToDestroy = m_spawnedElements.Dequeue();
               foreach (PoolElement element in objectToDestroy)
                  element.Destroy();
            }
         }
      }

      /////////////////////////////////////////////
      void SpawnRow(float zPosition)
      {
         List<PoolElement> spawnedElements = new List<PoolElement>();
         m_spawnedElements.Enqueue(spawnedElements);

         //How much obstacles generate in this line?
         int numObstacles = Random.Range((int)0, groundManager.lanes - 1);
#if DEBUG_SPAWN_MANAGER
         Debug.Log("line " + zPosition / spawnZDistance + " " + numObstacles);
#endif
         if (numObstacles != 0)
         {
            //List with all lanes indexes
            List<int> numberList = Enumerable.Range(0, groundManager.lanes).ToList();

            //Extract one index for every obstacle
            for (int k = 0; k < numObstacles; k++)
            {
               //Where generate?
               int xPosition = numberList[Random.Range((int)0, numberList.Count)];
               numberList.Remove(xPosition);
               //What obstacle generate?
               int obstacleType = Random.Range((int)0, m_objectPools.Count);
               PoolElement newObstacle = m_objectPools[obstacleType].Create();
               if(newObstacle == null)
               {
                  Debug.LogWarning("Failed to create a new object!");
               }
               Vector3 finalPosition = newObstacle.transform.position;
               finalPosition.x = groundManager.lanesDistance * xPosition + m_xOffset;
               finalPosition.z = zPosition;
               newObstacle.transform.position = finalPosition;
               spawnedElements.Add(newObstacle);
            }
         }
      }
   }
}
