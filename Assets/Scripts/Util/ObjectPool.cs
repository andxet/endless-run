//#define DEBUG_OBJECT_POOL

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndlessRun.Util
{
   /// <summary>
   /// Provide an object pool. Use this class to avoid the creation of 
   /// GameObjects during realtime. All the GO are created when a pool is created.
   /// </summary>
   public class ObjectPool
   {
      List<PoolElement> mActiveObjects;
      Queue<PoolElement> mAvailableObjects;

      Transform m_activeObjectsRoot;
#if DEBUG_OBJECT_POOL
      int mMinimumAvailableElements;
#endif

      /////////////////////////////////////////////
      public ObjectPool(int numElements, PoolElement go)
      {
         if(go == null)
         {
            Debug.LogError("ObjectPool: pool element is null!");
         }

         if (numElements < 1)
         {
            Debug.LogError("Failed to init the Object Pool");
            return;
         }

#if DEBUG_OBJECT_POOL
         mMinimumAvailableElements = numElements;
#endif

         mActiveObjects = new List<PoolElement>();
         mAvailableObjects = new Queue<PoolElement>();

         GameObject root = new GameObject("Pool_" + go.name);
         for (int i = 0; i < numElements; i++)
         {
            PoolElement component = Object.Instantiate(go);
            if (component == null)
            {
               Debug.LogError("ObjectPool: can't instantiate the PoolElement " + go + ".");
            }
            component.SetProprietaryPool(this);
            component.transform.parent = root.transform;
            component.Deactivate();
            mAvailableObjects.Enqueue(component);
         }
      }

      /////////////////////////////////////////////
      public PoolElement Create()
      {
         if (mAvailableObjects.Count == 0)
         {
            Debug.LogWarning("ObjectPool: no available objects.");
            return null;
         }
         PoolElement element = mAvailableObjects.Dequeue();
         mActiveObjects.Add(element);
         element.Reset();
#if DEBUG_OBJECT_POOL
         Debug.Log("ObjectPool: resuming object " + element.gameObject);
         if (mMinimumAvailableElements > mAvailableObjects.Count)
         {
            mMinimumAvailableElements = mAvailableObjects.Count;
            Debug.Log("ObjectPool: Minimum available elements reached: " + mMinimumAvailableElements);
         }
#endif
         return element;
      }

      //Set an object to disabled
      /////////////////////////////////////////////
      public void Destroy(PoolElement element)
      {
         if (!mActiveObjects.Contains(element))
         {
            Debug.LogWarning("ObjectPool: object is not active " + element + ".");
            return;
         }

#if DEBUG_OBJECT_POOL
         Debug.Log("ObjectPool: deactivating object " + element.gameObject);
#endif
         element.Deactivate();
         mActiveObjects.Remove(element);
         mAvailableObjects.Enqueue(element);
      }
   }
}
