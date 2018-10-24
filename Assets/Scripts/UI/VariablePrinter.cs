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
         m_text = GetComponent<Text>();
         variableToPrint.RegisterForUpdate(UpdateText);
         UpdateText(variableToPrint.GetValue());
      }

      /////////////////////////////////////////////
      void UpdateText(int meters)
      {
         m_text.text = prefix + meters;
      }
   }
}
