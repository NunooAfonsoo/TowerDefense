using UnityEditor;
using UnityEngine;

public abstract class Turret : MonoBehaviour, ITurret
{
    [SerializeField] private TurretConfig turretConfig;

    [SerializeField] protected Projectile projectile;

    [SerializeField] private Material previewCanPlaceMaterial;
    [SerializeField] private Material previewCannotPlaceMaterial;
    [SerializeField] private Material normalMaterial;

    protected float timeSinceLastShot;

    private new Renderer renderer;
    private bool hasBeenPlaced;


    protected virtual void Awake()
    {
        renderer = GetComponentInChildren<Renderer>();

        hasBeenPlaced = false;
        timeSinceLastShot = turretConfig.TimeBetweemShots;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (!hasBeenPlaced || timeSinceLastShot <= turretConfig.TimeBetweemShots)
        {
            return;
        }

        ScanForTargets();
    }

    private void ScanForTargets()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, turretConfig.ScanRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Creep creep))
            {
                Shoot(creep.transform.position);
                return;
            }
        }
    }

    private void Shoot(Vector3 targetPosition)
    {
        timeSinceLastShot = 0;

        IProjectile newProjectile = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<IProjectile>();

        newProjectile.SetDirection(targetPosition);
    }

    public void SetPreviewPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void ShowPreview()
    {
        renderer.sharedMaterial = previewCanPlaceMaterial;
    }

    public bool CanPlace()
    {
        return renderer.sharedMaterial == previewCanPlaceMaterial;
    }

    public void Place()
    {
        hasBeenPlaced = true;

        renderer.sharedMaterial = normalMaterial;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenPlaced)
        {
            return;
        }

        if (other.gameObject.name != Constants.TERRAIN_NAME)
        {
            renderer.sharedMaterial = previewCannotPlaceMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (hasBeenPlaced)
        {
            return;
        }

        if (other.gameObject.name != Constants.TERRAIN_NAME)
        {
            renderer.sharedMaterial = previewCanPlaceMaterial;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.black;
        Handles.DrawWireArc(transform.position, Vector3.up, Vector3.forward, 360, turretConfig.ScanRadius);
    }
    #endif
}
