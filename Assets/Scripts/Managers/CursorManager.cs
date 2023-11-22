using UnityEngine;

public class CursorManager : Singleton<CursorManager>
{
    [SerializeField] private LayerMask mouseColliderLayerMask = new LayerMask();

    [SerializeField] private IntEventSO turretPlacedEvent;

    private ITurret turret;
    private int selectedTurretCost;

    protected override void Awake()
    {
        base.Awake();

        selectedTurretCost = 0;
    }

    private void Update()
    {
        if (selectedTurretCost == 0)
        {
            return;
        }

        turret.SetPreviewPosition(GetMouseWorldPosition());

        CheckButtonPresses();
    }

    private void CheckButtonPresses()
    {
        if (Input.GetMouseButtonDown(0) && turret.CanPlace())
        {
            turret.Place();

            turret = null;
            turretPlacedEvent.RaiseEvent(-selectedTurretCost);
            selectedTurretCost = 0;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            turret.Destroy();
            turret = null;
            selectedTurretCost = 0;
        }
    }

    /// <summary>
    /// Returns the position in the Terrain where the mouse is aiming
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public void SetPlaceableTurret(ITurret turret, int cost)
    {
        this.turret = turret;
        selectedTurretCost = cost;
        turret.ShowPreview();
    }
}
