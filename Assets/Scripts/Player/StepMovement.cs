using UnityEngine;

namespace EndlessRun.Player
{
   public class StepMovement : MonoBehaviour
   {
      public float moveVelocity = 5.0f;

      /////////////////////////////////////////////
      void Start()
      {

      }

      /////////////////////////////////////////////
      void Update()
      {
         if (Time.timeScale > 0)
         {
            Vector3 newPosition = transform.position;
            newPosition.z += moveVelocity * Time.deltaTime * Time.timeScale;
            transform.position = newPosition;
         }
      }

   }
}
