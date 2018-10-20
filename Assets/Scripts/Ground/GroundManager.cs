using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRun.Ground
{
   public class GroundManager : MonoBehaviour
   {
      //Assumptions:
      //The center (x = 0) is the third lane. If the number of lanes change, this clast must be changed
      public const int lanes = 5;
      [Header("Ground prefab config")]
      public GameObject groundPrefab;
      public float lanesDistance = 2.0f, groundLength = 15.0f;

      [Header("Infinite ground info")]
      public int groundBeforePlayer = 1;
      public int groundAfterPlayer = 2;

      [Header("Object to follow")]
      public GameObject objectToFollow;

      Queue<GameObject> m_groundList;

      // Use this for initialization
      void Awake()
      {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
         if (groundPrefab == null || groundBeforePlayer < 0 || groundAfterPlayer < 0)
         {
            Debug.LogError("GroundManager " + name + ": component not correctly initialized.");
            enabled = false;
            return;
         }
#endif

         m_groundList = new Queue<GameObject>();
         float startPosition = -groundBeforePlayer * groundLength + groundLength / 2;
         for (int i = 0; i < groundBeforePlayer + groundAfterPlayer + 1; i++)
         {
            GameObject ground = Instantiate(groundPrefab);
            Vector3 pos = transform.position;
            pos.z += startPosition + i * groundLength;
            ground.transform.position = pos;
            ground.transform.parent = transform;
            m_groundList.Enqueue(ground);
         }
      }

      // Update is called once per frame
      void Update()
      {
         if (objectToFollow.transform.position.z - transform.position.z > groundLength)
         {
            transform.Translate(new Vector3(0, 0, groundLength));
         }
      }
   }
}
