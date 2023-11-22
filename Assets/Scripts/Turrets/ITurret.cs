using UnityEngine;

public interface ITurret
{
    void SetPreviewPosition(Vector3 position);
    void ShowPreview();
    bool CanPlace();
    void Place();
    void Destroy();
}
