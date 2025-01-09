using mBuilding.Scripts.MVVM.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using R3;


public class ScreenCastleBinder : WindowBinder<ScreenCastleViewModel>
{
    [SerializeField] private Button _btnGoToMenu;
    [SerializeField] private Button _btnGoToFight;
    [SerializeField] private Button _openSpawnPopup;

    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _queenText;

    private void Start()
    {
        ViewModel.RequestChangeMoneyText()
            .Subscribe(newValue => _moneyText.text = newValue.ToString());
        ViewModel.RequestChangeQueen()
            .Subscribe(newValue => _queenText.text = newValue.ToString());
    }

    private void OnEnable()
    {
        _btnGoToMenu.onClick.AddListener(OnGoToMainMenuButtonClicked);
        _btnGoToFight.onClick.AddListener(OnGoToFightButtonClicked);
        _openSpawnPopup.onClick.AddListener(OnOpenSpawnPopupButtonClicked);
    }

    private void OnDisable()
    {
        _btnGoToMenu.onClick.RemoveListener(OnGoToMainMenuButtonClicked);
        _btnGoToFight.onClick.RemoveListener(OnGoToFightButtonClicked);
        _openSpawnPopup.onClick.RemoveListener(OnOpenSpawnPopupButtonClicked);
    }

    private void OnOpenSpawnPopupButtonClicked()
    {
        ViewModel.RequestOpenSpawnPopup();
    }

    private void OnGoToMainMenuButtonClicked()
    {
        ViewModel.RequestGoToMainMenu();
    }

    private void OnGoToFightButtonClicked()
    {
        ViewModel.RequestGoToFight();
    }
}
