using oct.cameraControl;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace oct.misc
{
    public class Tutorial : MonoBehaviour
    {
       public List<Sprite> sprites;
        static PlayerInput action;
       public Image display;
        public Button Button;
       public Transform pivot;
       public GameObject buttonGameObject;
        private void Start()
        {

            action = new PlayerInput();
            action.Disable();
            action.dialogue.next.started += ctx => { Move(); };
            Button.onClick.AddListener(startDisplay);
        }

        public void startDisplay()
        {
            InputController.allow2dInput = false;
            MainCameraController.setToDialogueMode(pivot);
            Debug.Log("!!");
            action.Enable();
            display.enabled = true;
            display.sprite = sprites[0];
            currentIndex = 0;
            buttonGameObject.SetActive(false);
        }
        public int currentIndex;
        void Move()
        {
            currentIndex += 1;
            if (currentIndex>= sprites.Count)
            {
                end();
                return;
            }
            display.sprite = sprites[currentIndex];
        }

        void end()
        {
            InputController.allow2dInput = true;
            display.enabled=false;
            currentIndex = 0;
            display.sprite = null;
            action.Disable();
            buttonGameObject.SetActive(true);
            MainCameraController.EndDialogueMode();
        }
    }
}