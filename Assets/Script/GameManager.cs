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
        private int waitingTime;

        // Start is called before the first frame update
        void Start()
        {

        }

        public void Init()
        {
            gameState = GAMESTATE.IDLE;
        }

        // Update is called once per frame
        void Update()
        {
            if (gameState != GAMESTATE.PAUSE)
            {
                CastingInput();
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
                    characterAnim.SetTrigger("Casting");
                }
            }
        }

        IEnumerator WaitingForFish()
        {
            gameState = GAMESTATE.WAITNGFORFISH;
            waitingTime = Random.Range(1, 4);
            yield return new WaitForSeconds(waitingTime);
            
        }
    }
}