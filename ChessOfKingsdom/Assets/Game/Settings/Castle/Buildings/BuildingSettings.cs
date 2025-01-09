using System.Collections.Generic;
using UnityEngine;

namespace Settings.Castle
{
    [CreateAssetMenu(fileName = "BuildingsSettings", menuName = "Game Settings/Buildings/New Building Settings"        )]
    public class BuildingSettings : ScriptableObject
    {
        public string TypeId;
        public string AbilityName;
        public string TitleId;
        public string DescriptionID;
        public int Price;
        public List<BuildingsLevelSettings> BuildingsLevelSettings;
    }
}
