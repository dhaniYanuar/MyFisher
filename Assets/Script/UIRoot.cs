using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static MyFisher.EnumContainer;

namespace MyFisher
{
    public class UIRoot : MonoBehaviour
    {
        #region Singleton
        private static UIRoot instance;
        public static UIRoot Instance
        {
            get
            {
                return instance;
            }
        }
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                DontDestroyOnLoad(this);
            }
        }
        #endregion
        [SerializeField]
        public List<UICustomTemplate> UiList;
        
        public void Start()
        {
            InitUI();
        }

        private void InitUI()
        {
            FindAllUiElement();
            GoToUiElement(MENUSTATE.MAINMENU);
        }

        private void FindAllUiElement()
        {
            UiList = new List<UICustomTemplate>();
            var objList = FindObjectsOfType<UICustomTemplate>();
            foreach (var item in objList)
            {
                UiList.Add(item);
            }
        }

        public void GoToUiElement(MENUSTATE menuState)
        {
            if (!UiList.Exists(x => x.menuState == menuState))
            {
                return;
            }
            CloseAllUI();
            UiList.Find(x => x.menuState == menuState).Show();
        } 

        public void CloseAllUI()
        {
            foreach(var ui in UiList)
            {
                ui.Hide();
            }
        }
    }
}
