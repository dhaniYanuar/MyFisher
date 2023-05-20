using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyFisher.EnumContainer;

namespace MyFisher
{
    public class UICustomTemplate : MonoBehaviour, UICustomInterface
    {
        public MENUSTATE menuState;
        [SerializeField]
        private Camera camera;
        public virtual void Hide()
        {
            camera.gameObject.SetActive(false);
            gameObject.GetComponent<CanvasGroup>().interactable = false;
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
        }

        public virtual void Show()
        {
            camera.gameObject.SetActive(true);
            gameObject.GetComponent<CanvasGroup>().interactable = true;
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }
    }
}
