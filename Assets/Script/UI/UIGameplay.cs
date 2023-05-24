using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyFisher
{
    public class UIGameplay : UICustomTemplate
    {
        [SerializeField]
        private Slider IndPowerSlider;
        [SerializeField]
        private TextMeshProUGUI txtFish;
        [SerializeField]
        private TextMeshProUGUI txtFailed;
        [SerializeField]
        private TextMeshProUGUI txtTime;
        [SerializeField]
        private GameObject popUpResult;
        [SerializeField]
        private TextMeshProUGUI txtResultFish;
        [SerializeField]
        private TextMeshProUGUI txtResultFailed;
        [SerializeField]
        private Button btnResultRestart;
        [SerializeField]
        private Button btnResultBackToMainMenu;
        [SerializeField]
        private GameObject popUpPause;
        [SerializeField]
        private Button btnResume;
        [SerializeField]
        private Button btnBackToMainMenu;

        private void Start()
        {
            menuState = EnumContainer.MENUSTATE.GAMEPLAY;
        }

        private void OnEnable()
        {
            btnResume.onClick.AddListener(OnClickResume);
            btnBackToMainMenu.onClick.AddListener(OnClickMainMenu);
            btnResultRestart.onClick.AddListener(OnClickRestart);
            btnResultBackToMainMenu.onClick.AddListener(OnClickMainMenu);
        }

        private void OnClickResume()
        {
            popUpPause.SetActive(false);
            GameManager.Instance.Resume();
        }

        private void OnClickRestart()
        {
            UIRoot.Instance.GoToUiElement(EnumContainer.MENUSTATE.GAMEPLAY);
            GameManager.Instance.Init();
        }

        private void OnClickMainMenu()
        {
            UIRoot.Instance.GoToUiElement(EnumContainer.MENUSTATE.MAINMENU);
        }

        public void ChangeFishText(int _fish)
        {
            txtFish.text = $"Fish : {_fish}";
        }

        public void ChangeFailedText(int _failed)
        {
            txtFailed.text = $"Failed : {_failed}";
        }

        public void ChangeTimeText(float _time)
        {
            if (_time <=0 )
            {
                txtTime.text = $"Time : TIMEOUT";
                return;
            }
            float minutes = Mathf.FloorToInt(_time / 60);
            float seconds = Mathf.FloorToInt(_time % 60);
            var timeFormat = string.Format("{0:00}:{1:00}", minutes, seconds);
            txtTime.text = $"Time : {timeFormat}";
        }

        public void ResetIndicator()
        {
            IndPowerSlider.value = 0;
        }

        public void AddPowerValue(float _power)
        {
            IndPowerSlider.value += _power;
        }

        public void ShowResult(int _fish, int _failed)
        {
            popUpResult.SetActive(true);
            txtResultFish.text = $": {_fish}";
            txtResultFailed.text = $": {_failed}";
        }

        public void ShowPause()
        {
            popUpPause.SetActive(true);
        }

        public override void Show()
        {
            base.Show();
            IndPowerSlider.value = 0;
            txtFish.text = $"Fish : 0";
            txtFailed.text = $"Failed : 0";
            txtTime.text = $"Time : 00:00";
            popUpResult.SetActive(false);
            popUpPause.SetActive(false);
        }
    }
}