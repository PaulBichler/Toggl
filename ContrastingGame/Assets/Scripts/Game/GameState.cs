using System;
using System.Collections.Generic;

namespace Game
{
    public static class GameState
    {
        public static  Dictionary<string, bool> LevelUnlockStatus = new Dictionary<string, bool>()
        {
            {"Level0", true},
            {"Level1", false},
            {"Level2", false},
            {"Level3", false},
            {"Level4", false},
            {"Level5", false},
            {"Level6", false},
            {"Level7", false},
            {"Level8", false},
            {"Level9", false},
        };
    }
}
