using Buildings;
using R3;
using StateRoot;
using States.GameResources;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace States
{
    public class PlayerPrefsGameStateProvider : IGameStateProvider
    {
        private const string GAME_STATE_KEY = nameof(GAME_STATE_KEY);

        public GameStateProxy GameState { get; private set; }

        private GameState _gameStateOrigin;

        public Observable<GameStateProxy> LoadGameState()
        {
            if (!PlayerPrefs.HasKey(GAME_STATE_KEY))
            {
                GameState = CreateGameStateFromSettings();
                Debug.Log("Game State created from settings: " + JsonUtility.ToJson(_gameStateOrigin, true));

                SaveGameState();
            }
            else
            {
                var json = PlayerPrefs.GetString(GAME_STATE_KEY);
                _gameStateOrigin = JsonUtility.FromJson<GameState>(json);
                GameState = new GameStateProxy(_gameStateOrigin);

                Debug.Log("Game State loaded: " + json);
            }

            return Observable.Return(GameState);
        }

        public Observable<bool> ResetGameState()
        {
            GameState = CreateGameStateFromSettings();
            SaveGameState();

            return Observable.Return(true);
        }

        public Observable<bool> SaveGameState()
        {
            var json = JsonUtility.ToJson(_gameStateOrigin, true);
            PlayerPrefs.SetString(GAME_STATE_KEY, json);

            return Observable.Return(true);
        }

        private GameStateProxy CreateGameStateFromSettings()
        {
            _gameStateOrigin = new GameState
            {
                Buildings = new List<BuildingsEntity>(),
                Resource = new List<ResourceData>()
                {
                    new() {Amount = 0, ResourceType = ResourceType.SoftCurrency},
                    new() {Amount = 0, ResourceType = ResourceType.HardCurrency},
                }
            };

            return new GameStateProxy(_gameStateOrigin);
        }
    }
}
