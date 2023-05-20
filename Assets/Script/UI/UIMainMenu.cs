using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyFisher
{
    public class UIMainMenu : UICustomTemplate
    {
        public Button play;
        public Button credit;
        public Button exit;

        private void Start()
        {
            AssignListner();
        }

        private void OnEnable()
        {
            AssignListner();
        }

        private void OnDestroy()
        {
            DisposeListner();
        }

        private void AssignListner()
        {
            play.onClick.AddListener(OnClickPlay);
            credit.onClick.AddListener(OnClickCredit);
            exit.onClick.AddListener(OnClickExit);
        }

        private void DisposeListner()
        {
            play.onClick.RemoveListener(OnClickPlay);
            credit.onClick.RemoveListener(OnClickCredit);
            exit.onClick.RemoveListener(OnClickExit);
        }

        private void OnClickPlay()
        {
            UIRoot.Instance.GoToUiElement(EnumContainer.MENUSTATE.GAMEPLAY);
        }

        public void OnClickCredit()
        {

        }

        public void OnClickExit()
        {
#if UNITY_STANDALONE_WIN
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

}