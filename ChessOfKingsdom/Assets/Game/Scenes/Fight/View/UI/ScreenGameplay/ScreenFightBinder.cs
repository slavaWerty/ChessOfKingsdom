using Fight.View;
using mBuilding.Scripts.MVVM.UI;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFightBinder : WindowBinder<ScreenFightViewModel>
{
    [SerializeField] private Button _btnGoToMenu;
    [SerializeField] private Button _btnStartFight;
    [SerializeField] private Button _btnTest;
 
    private WorldFightRootBinder _worldBinder;

    private void Start()
    {
        _worldBinder = FindObjectOfType<WorldFightRootBinder>();
    }

    private void OnEnable()
    {
        _btnGoToMenu.onClick.AddListener(OnGoToMenuButtonClicked);
        _btnStartFight.onClick.AddListener(OnStartFightButtonClicked);
        _btnTest.onClick.AddListener(OnTest);
    }

    private void OnDisable()
    {
        _btnGoToMenu.onClick.RemoveListener(OnGoToMenuButtonClicked);
        _btnStartFight.onClick.RemoveListener(OnStartFightButtonClicked);
        _btnTest.onClick.RemoveListener(OnTest);
    }

    private void OnGoToMenuButtonClicked()
    {
        ViewModel.RequestGoToMenu();
    }

    private void OnStartFightButtonClicked()
    {
        ViewModel.RequestStartFight();
    }

    private void OnTest()
    {
        var militaryService = ViewModel.MilitaryService;
        var militaryViewModel = militaryService._militariesMap[1];

        _worldBinder.CreateMilitaryBinder(militaryViewModel);
    }
}
