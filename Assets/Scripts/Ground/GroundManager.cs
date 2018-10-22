using EndlessRun.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRun.Ground
{
   public class GroundManager : MonoBehaviour
   {
      //Assumptions:
      //The center (x = 0) is the third lane. If the number of lanes change, this class must be changed
      [Header("Ground prefab config")]
      public int lanes = 5;
      public GameObject groundPrefab;
      public float lanesDistance = 2.0f, groundLength = 15.0f;
      //Just to keep things ordered
      public Transform groundParent;

      [Header("Infinite ground info")]
      public int groundBeforePlayer = 1;
      public int groundAfterPlayer = 3;

      [Header("Object to follow")]
      public FloatVariable objectToFollow;

      Queue<Transform> m_groundList;
      //Current reference to move the ground
      float z_reference = 0;

      /////////////////////////////////////////////
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

         m_groundList = new Queue<Transform>();
         float startPosition = -groundBeforePlayer * groundLength + groundLength / 2;
         for (int i = 0; i < groundBeforePlayer + groundAfterPlayer; i++)
         {
            GameObject ground = Instantiate(groundPrefab);
            Vector3 pos = ground.transform.position;
            pos.z += startPosition + i * groundLength;
            ground.transform.position = pos;
            if(groundParent != null)
               ground.transform.parent = groundParent;
            m_groundList.Enqueue(ground.transform);
         }
      }

      void Start()
      {
         z_reference = objectToFollow.GetValue();
      }

      /////////////////////////////////////////////
      void Update()
      {
         if (objectToFollow.GetValue() - z_reference > groundLength)
         {
            Transform toMove = m_groundList.Dequeue();
            toMove.Translate(new Vector3(0, 0, groundLength * (groundBeforePlayer + groundAfterPlayer)));
            m_groundList.Enqueue(toMove);
            z_reference += groundLength;
         }
      }
   }
}
