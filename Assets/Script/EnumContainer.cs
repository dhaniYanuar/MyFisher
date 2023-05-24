using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFisher
{
    public class EnumContainer
    {
        public enum SFXENUM { CASTINGROD, CASTBAIT, HIT, LOSE, PULLSTRING, LOSESTRING }
        public enum GAMESTATE { IDLE, WAITNGFORFISH, HOOK, BREAK, CATCH, PAUSE, GAMEOVER }
        public enum MENUSTATE { MAINMENU, GAMEPLAY }
    }
}