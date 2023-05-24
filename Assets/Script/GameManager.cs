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
        public Character character;
        public Animator characterAnim;
        public HookBehavior hookBehavior;
        public GAMESTATE gameState;
        private int waitingTime = 3;
        [SerializeField]
        private float timer;
        private int failed;
        private int fish;
        private int pointPlus;
        IEnumerator currentCoroutine;

        public void Init()
        {
            timer = 121f;
            currentCoroutine = GettingReadyGameplay();
            hookBehavior.gameObject.SetActive(false);
            StartCoroutine(currentCoroutine);
            ResetScore();
        }

        private void ResetScore()
        {
            failed = 0;
            fish = 0;
        }

        public void Resume()
        {
            currentCoroutine = GettingReadyGameplay();
            uIGameplay.ResetIndicator();
            StartCoroutine(currentCoroutine);
        }

        IEnumerator GettingReadyGameplay()
        {
            yield return new WaitForSeconds(0.5f);
            gameState = GAMESTATE.IDLE;
            yield return null;
        }

        // Update is called once per frame
        void Update()
        {
            if (gameState != GAMESTATE.PAUSE && timer > 0)
            {
                PauseInput();
                CastingInput();
                GetTheFish();
                timer -= Time.deltaTime;
                uIGameplay.ChangeTimeText(timer);
            }
            if (gameState != GAMESTATE.PAUSE && timer <=0)
            {
                gameState = GAMESTATE.GAMEOVER;
                uIGameplay.ShowResult(fish, failed);
            }
        }

        private void PauseInput()
        {
            if (gameState == GAMESTATE.IDLE)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    uIGameplay.ShowPause();
                    gameState = GAMESTATE.PAUSE;
                }
            }
        }

        private void CastingInput()
        {
            if (gameState == GAMESTATE.IDLE)
            {
                if (Input.GetMouseButton(0))
                {
                    uIGameplay.AddPowerValue(0.4f * Time.deltaTime);

                }
                if (Input.GetMouseButtonUp(0))
                {
                    if (uIGameplay.GetPowerValue() < 0.5f)
                    {
                        pointPlus = 0;
                    }
                    if (uIGameplay.GetPowerValue() > 0.5f)
                    {
                        pointPlus = 1;
                    }
                    if (uIGameplay.GetPowerValue() >= 0.8f)
                    {
                        pointPlus = 2;
                    }
                    currentCoroutine = WaitingForFish();
                    StartCoroutine(currentCoroutine);
                }
            }
        }

        public float GetDifficultFish()
        {
            if (pointPlus == 1)
            {
                return 0.2f;
            }
            if (pointPlus == 2)
            {
                return 0.05f;
            }
            return 1f; 
        }

        private void GetTheFish()
        {
            if (gameState == GAMESTATE.WAITNGFORFISH)
            {
                if (Input.GetMouseButton(0))
                {
                    StopCoroutine(currentCoroutine);
                    currentCoroutine = DefeatSequence();
                    StartCoroutine(currentCoroutine);
                }
            }
            if (gameState == GAMESTATE.CATCH)
            {
                if (Input.GetMouseButton(0))
                {
                    character.RequestResetBait();
                    character.HideCatchIndicator();
                    hookBehavior.gameObject.SetActive(true);
                    hookBehavior.Init();
                    gameState = GAMESTATE.HOOK;
                }
            }
            if (gameState == GAMESTATE.HOOK && !hookBehavior.IsHookEnd())
            {
                hookBehavior.MoveIndicator();
            }
            else if (gameState == GAMESTATE.HOOK && hookBehavior.IsHookEnd())
            {
                currentCoroutine = CelebrationSequence();
                StartCoroutine(currentCoroutine);
            }
        }

        IEnumerator WaitingForFish()
        {
            gameState = GAMESTATE.PAUSE;
            characterAnim.SetTrigger("Casting");
            yield return new WaitForSeconds(8f);
            gameState = GAMESTATE.WAITNGFORFISH;
            yield return new WaitForSeconds(waitingTime);
            character.RequestBaitCatch();
            gameState = GAMESTATE.CATCH;
            yield return null;
        }

        IEnumerator DefeatSequence()
        {
            gameState = GAMESTATE.PAUSE;
            AudioManager.Instance.PlaySFX(SFXENUM.LOSE);
            character.RequestResetBait();
            failed++;
            uIGameplay.ChangeFailedText(failed);
            characterAnim.SetTrigger("Defeat");
            yield return new WaitForSeconds(4.3f);
            gameState = GAMESTATE.IDLE;
            uIGameplay.ResetIndicator();
            yield return null;
        }

        IEnumerator CelebrationSequence()
        {
            gameState = GAMESTATE.PAUSE;
            if (hookBehavior.isCatched())
            {
                fish += 1 + pointPlus;
                uIGameplay.ChangeFishText(fish);
                AudioManager.Instance.PlaySFX(SFXENUM.HIT);
                characterAnim.SetTrigger("Celebration");
            }
            else
            {
                failed++;
                AudioManager.Instance.PlaySFX(SFXENUM.LOSE);
                uIGameplay.ChangeFailedText(failed);
                characterAnim.SetTrigger("Defeat");
            }
            yield return new WaitForSeconds(4.3f);
            gameState = GAMESTATE.IDLE;
            uIGameplay.ResetIndicator();
            hookBehavior.gameObject.SetActive(false);
            yield return null;
        }
    }
}