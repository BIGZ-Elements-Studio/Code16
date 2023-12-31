using CombatSystem;
using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
namespace UnityEngine.InputSystem.OnScreen
{
    public class AtkButton : OnScreenControl, IPointerDownHandler, IPointerUpHandler
    {

        
        public void OnPointerUp(PointerEventData eventData)
        { 
            if (eventData.button == PointerEventData.InputButton.Left) {

                SendValueToControl(0.0f);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                SendValueToControl(1.0f);
            }
        }

        ////TODO: pressure support
        /*
        /// <summary>
        /// If true, the button's value is driven from the pressure value of touch or pen input.
        /// </summary>
        /// <remarks>
        /// This essentially allows having trigger-like buttons as on-screen controls.
        /// </remarks>
        [SerializeField] private bool m_UsePressure;
        */

        [InputControl(layout = "Button")]
        [SerializeField]
        private string m_ControlPath;

        protected override string controlPathInternal
        {
            get => m_ControlPath;
            set => m_ControlPath = value;
        }
    }


}