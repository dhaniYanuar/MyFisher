using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFisher
{
    public class EnumContainer
    {
        public enum SFXENUM { HIT, LOSE, SWIPE }
        public enum GAMESTATE { IDLE, WAITNGFORFISH, BREAK, CATCH, PAUSE }
        public enum MENUSTATE { MAINMENU, GAMEPLAY }
    }
}