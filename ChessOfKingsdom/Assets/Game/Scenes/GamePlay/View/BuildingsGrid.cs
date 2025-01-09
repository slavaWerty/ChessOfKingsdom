using GamePlay.Services;
using GamePlay.View.Buildings;
using UnityEngine;
using UnityEngine.EventSystems;
using View.Buildings;

public class BuildingsGrid : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public Vector2Int GridSize = new Vector2Int(10, 10);

    private BuildingsBinder[,] grid;
    private BuildingsBinder FlyingBuilding;
    private BuildingsViewModel FlyingViewModel;
    private BuildingsService _buildingsService;

    private void Awake()
    {
        grid = new BuildingsBinder[GridSize.x, GridSize.y];

        mainCamera = Camera.main;
    }

    public void StartPlacingBuilding(BuildingsBinder buildingPrefab,
        BuildingsService buildingsService,
        BuildingsViewModel viewModel)
    {
        if (FlyingBuilding != null)
        {
            Destroy(FlyingBuilding.gameObject);
        }

        FlyingBuilding = Instantiate(buildingPrefab);
        _buildingsService = buildingsService;
        FlyingViewModel = viewModel;
    }

    private void Update()
    {
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (FlyingBuilding != null)
        {
            int x = Mathf.RoundToInt(mouseWorldPosition.x);
            int y = Mathf.RoundToInt(mouseWorldPosition.y);

            FlyingBuilding.transform.position = new Vector2(x, y);

            var available = true;

            if (x < 0 || x > GridSize.x - FlyingBuilding.Size.x) available = false;
            if (y < 0 || y > GridSize.y - FlyingBuilding.Size.y) available = false;
            if (available && IsPlaceTaken(x, y)) available = false;

            FlyingBuilding.SetTransparent(available);

            if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
                FlyingBuilding = null;

            if (Input.GetMouseButtonDown(1) && available)
            {
                PlaceFlyingBuilding(x, y);
                Destroy(FlyingBuilding);
            }

        }
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < FlyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < FlyingBuilding.Size.y; y++)
            {
                if (grid[placeX + x, placeY + y] != null) return true;
            }
        }

        return false;
    }

    private void PlaceFlyingBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < FlyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < FlyingBuilding.Size.y; y++)
            {
                grid[placeX + x, placeY + y] = FlyingBuilding;
            }
        }

        _buildingsService.PlaceBuilding(FlyingViewModel.TypeId, 
            new Vector2Int((int)FlyingBuilding.transform.position.x, 
            (int)FlyingBuilding.transform.position.y));

        FlyingBuilding.SetNormal();
        FlyingBuilding = null;
        FlyingViewModel = null;
    }
}