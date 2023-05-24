using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyFisher.EnumContainer;

namespace MyFisher
{
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private GameObject bait;
        [SerializeField]
        private GameObject catchIndicator;

        private void Start()
        {
            bait.SetActive(false);
            catchIndicator.SetActive(false);
        }

        public void RequestSFXCastingRod()
        {
            AudioManager.Instance.PlaySFX(SFXENUM.CASTINGROD);
        }

        public void RequestBait()
        {
            AudioManager.Instance.PlaySFX(SFXENUM.CASTBAIT);
            bait.SetActive(true);
        }

        public void RequestBaitCatch()
        {
            bait.GetComponent<Animator>().SetTrigger("FishBaited");
            catchIndicator.SetActive(true);
            AudioManager.Instance.PlaySFX(SFXENUM.HIT);
        }

        public void HideCatchIndicator()
        {
            catchIndicator.SetActive(false);
        }

        public void RequestResetBait()
        {
            bait.SetActive(false);
        }

    }

}