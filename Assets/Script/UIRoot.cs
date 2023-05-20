using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public UICustomElement MainMenu;
        public UICustomElement Gameplay;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
