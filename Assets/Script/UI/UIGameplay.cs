using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyFisher
{
    public class UIGameplay : UICustomTemplate
    {
        public Slider IndPowerSlider;
        public override void Show()
        {
            base.Show();
            IndPowerSlider.value = 0;
        }
    }
}