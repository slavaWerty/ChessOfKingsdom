using mBuilding.Scripts.MVVM.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.MainMenu.View.UI.Screen
{
    public class ScreenMenuBinder : WindowBinder<ScreenMenuViewModel>
    {
        [SerializeField] private Button _btnGoToCastle;
        [SerializeField] private Button _btnGoToSettings;
        [SerializeField] private Button _btnGoToAuthor;

        private void OnEnable()
        {
            _btnGoToCastle.onClick.AddListener(OnGoToCastleButtonClicked);
            _btnGoToSettings.onClick.AddListener(OnGoToSettingsButtonClicked);
            _btnGoToAuthor.onClick.AddListener(OnGoToAuthorButtonClicked);
        }

        private void OnDisable()
        {
            _btnGoToCastle.onClick.RemoveListener(OnGoToCastleButtonClicked);
            _btnGoToSettings.onClick.RemoveListener(OnGoToSettingsButtonClicked);
            _btnGoToAuthor.onClick.RemoveListener(OnGoToAuthorButtonClicked);
        }

        private void OnGoToCastleButtonClicked()
        {
            ViewModel.RequestGoToCastle();
        }

        private void OnGoToSettingsButtonClicked()
        {
            ViewModel.RequestOpenSetting();
        }

        private void OnGoToAuthorButtonClicked()
        {
            ViewModel.RequestOpenAuthor();
        }
    }
}