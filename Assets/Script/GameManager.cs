using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static MyFisher.EnumContainer;

namespace MyFisher
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        private static GameManager instance;
        public static GameManager Instance
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
        public UIGameplay uIGameplay;
        public Animator characterAnim;
        public GAMESTATE gameState;
        private int waitingTime = 3;
        private int failed;
        private int fish;

        // Start is called before the first frame update
        void Start()
        {

        }

        public void Init()
        {
            StartCoroutine(GettingReadyGameplay());
        }

        private void ResetScore()
        {
            failed = 0;
            fish = 0;
        }

        IEnumerator GettingReadyGameplay()
        {
            yield return new WaitForSeconds(0.5f);
            GameManager.Instance.gameState = EnumContainer.GAMESTATE.IDLE;
        }

        // Update is called once per frame
        void Update()
        {
            if (gameState != GAMESTATE.PAUSE)
            {
                CastingInput();
                GetTheFish();
            }
        }

        private void CastingInput()
        {
            if (gameState == GAMESTATE.IDLE)
            {
                if (Input.GetMouseButton(0))
                {
                    uIGameplay.IndPowerSlider.value += 0.4f * Time.deltaTime;

                }
                if (Input.GetMouseButtonUp(0))
                {
                    StartCoroutine(WaitingForFish());
                }
            }
        }

        private void GetTheFish()
        {
            if (gameState == GAMESTATE.WAITNGFORFISH)
            {
                if (Input.GetMouseButton(0))
                {
                    StartCoroutine(DefeatSequence());
                }
            }
            if (gameState == GAMESTATE.CATCH)
            {
                if (Input.GetMouseButton(0))
                {
                    StartCoroutine(CelebrationSequence());
                }
            }
        }

        IEnumerator WaitingForFish()
        {
            gameState = GAMESTATE.PAUSE;
            characterAnim.SetTrigger("Casting");
            yield return new WaitForSeconds(8f);
            gameState = GAMESTATE.WAITNGFORFISH;
            yield return new WaitForSeconds(waitingTime);
            gameState = GAMESTATE.CATCH;
            yield return null;
        }

        IEnumerator DefeatSequence()
        {
            gameState = GAMESTATE.PAUSE;
            failed++;
            characterAnim.SetTrigger("Defeat");
            yield return new WaitForSeconds(4.3f);
            gameState = GAMESTATE.IDLE;
            yield return null;
        }

        IEnumerator CelebrationSequence()
        {
            gameState = GAMESTATE.PAUSE;
            fish++;
            characterAnim.SetTrigger("Celebration");
            yield return new WaitForSeconds(4.3f);
            gameState = GAMESTATE.IDLE;
            yield return null;
        }
    }
}