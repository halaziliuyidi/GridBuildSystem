using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField]
    private float previewYOffest = 0.06f;

    [SerializeField]
    private GameObject cellIndicator;
    private GameObject previewobject;

    [SerializeField]
    private Material previewMaterialsPrefab;
    private Material previewmaterialInstance;

    private Renderer cellIndicatorRenderer;

    private void Start()
    {
        previewmaterialInstance = new Material(previewMaterialsPrefab);
        cellIndicator.SetActive(false);
        cellIndicatorRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        previewobject=Instantiate(prefab);
        PreparePreaview(previewobject);
        PrepareCursoe(size);
        cellIndicator.SetActive(true);
    }

    private void PrepareCursoe(Vector2Int size)
    {
        if (size.x > 0 || size.y > 0)
        {
            cellIndicator.transform.localScale = new Vector3(size.x,1,size.y);
            cellIndicatorRenderer.material.mainTextureScale = size;
        }
    }

    private void PreparePreaview(GameObject previewobject)
    {
        Renderer[] renderers=previewobject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material[] materials=renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewmaterialInstance;
            }
            renderer.materials = materials;
        }
    }

    public void StopShowingPreview()
    {
        cellIndicator.SetActive(false);
        if (previewobject != null)
        {
            Destroy(previewobject);
        }
    }

    public void UpdatePosition(Vector3 position, bool validity)
    {
        if (previewobject != null)
        {
            MovePreview(position);
            ApplyFeedbackToPreview(validity);
        }
        MoveCursor(position);
        ApplyFeedbackToCoursor(validity);
    }

    private void ApplyFeedbackToPreview(bool validity)
    {
        Color c=validity ? Color.white : Color.red;
        c.a = 0.5f;
        previewmaterialInstance.color = c;
    }

    private void ApplyFeedbackToCoursor(bool validity)
    {
        Color c = validity ? Color.white : Color.red;
        c.a = 0.5f;
        cellIndicatorRenderer.material.color = c;
    }

    private void MoveCursor(Vector3 position)
    {
        cellIndicator.transform.position = position;
    }

    private void MovePreview(Vector3 position)
    {
        previewobject.transform.position = new Vector3(
            position.x,
            position.y+previewYOffest,
            position.z);
    }

    internal void StartShowingRemovePreview()
    {
        cellIndicator.SetActive(true);
        PrepareCursoe(Vector2Int.one);
        ApplyFeedbackToCoursor(false);
    }
}
