using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyFisher
{
    public class HookBehavior : MonoBehaviour
    {
        [SerializeField]
        private Slider fishSlider;
        [SerializeField]
        private Slider hookPosition;
        [SerializeField]
        float fishPosition;
        [SerializeField]
        float smoothMotion;
        float fishSpeed;

        [SerializeField]
        private float timerMultiplicator;
        private float fishTimer;

        float destination;

        private float timeHook;
        private float powerLine;

        public void Init()
        {
            timeHook = 10f;
            powerLine = 10;
            smoothMotion = GameManager.Instance.GetDifficultFish();
        }

        public void MoveIndicator()
        {
            fishTimer -= Time.deltaTime;
            if (fishTimer < 0f)
            {
                fishTimer = UnityEngine.Random.value * timerMultiplicator;
                destination = Random.Range(0.0f, 1.0f);
            }
            fishPosition = Mathf.SmoothDamp(fishPosition, destination,ref fishSpeed, smoothMotion);
            fishSlider.value = Mathf.Lerp(0.0f,1.0f, fishPosition);

            if (Input.GetMouseButton(0))
            {
                hookPosition.value += Time.deltaTime;
                AudioManager.Instance.PlayLoopSfx(EnumContainer.SFXENUM.PULLSTRING);
            }
            else
            {
                hookPosition.value -= Time.deltaTime;
                AudioManager.Instance.StopSFX();
            }

            if ((fishSlider.value-0.2f) < hookPosition.value && hookPosition.value  < (fishSlider.value + 0.2f))
            {
                timeHook -= Time.deltaTime;
            }
            else
            {
                powerLine -= Time.deltaTime;
            }
        }

        public bool IsHookEnd()
        {
            return (timeHook <= 0 || powerLine <= 0);
        }

        public bool isCatched()
        {
            if (timeHook <= 0)
            {
                return true;
            }
            else if (powerLine <= 0)
            {
                return false;
            }
            return false;
        }
    }
}
