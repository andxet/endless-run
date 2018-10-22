using EndlessRun.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessRun.UI
{
   [RequireComponent(typeof(Text))]
   public class VariablePrinter : MonoBehaviour
   {
      public IntVariable variableToPrint;
      public string prefix;
      Text m_text;


      /////////////////////////////////////////////
      void Start()
      {
#if DEBUG //Let's assume that when the release is built, theese checks are passed
         if (variableToPrint == null)
         {
            Debug.LogError("VariablePrinter " + name + ": component not correctly initialized.");
            enabled = false;
            return;
         }
#endif
         variableToPrint.RegisterForUpdate(UpdateText);
         m_text = GetComponent<Text>();
      }

      /////////////////////////////////////////////
      void UpdateText(int meters)
      {
         m_text.text = prefix + meters;
      }
   }
}
