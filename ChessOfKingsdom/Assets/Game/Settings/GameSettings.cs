using Settings.Castle.Buildings;
using UnityEngine;

namespace Game.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings/New Game Settings")]
    public class GameSettings : ScriptableObject
    {
        public BuildingsSettings BuildingsSettings;
        public GamePlay.Settings.BuildingsSettings NewBuildingsSettingsp;
    }
}
