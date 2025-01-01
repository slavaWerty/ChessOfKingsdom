using Game.Settings;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Settings
{
    public class SettingsProvider : ISettingsProvider
    {
        public ApplicationSettings ApplicationSettings { get; }

        GameSettings ISettingsProvider.GameSettings => _gameSettings;

        private GameSettings _gameSettings;

        public SettingsProvider()
        {
            ApplicationSettings = Resources.Load<ApplicationSettings>("ApplicationSettings");
        }

        public Task<GameSettings> LoadGameSettings()
        {
            _gameSettings = Resources.Load<GameSettings>("GameSettings");

            return Task.FromResult(_gameSettings);
        }
    }
}
