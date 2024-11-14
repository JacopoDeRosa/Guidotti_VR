using System;

namespace GameLogic
{
    [Serializable]
    public class PlayerScore
    {
        public string playerName = "";
        public float glucoseLevel = 0f;
        public float sodiumLevel = 0f;
        
        public float TotalScore => glucoseLevel + sodiumLevel;
    }
}