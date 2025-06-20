using UnityEngine;

public class ShipDragHandler : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;

    private float currentRotation = 0f;
    private Camera mainCamera;

    [SerializeField] private int shipLength; // Define no Inspector ou script

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = 0f;
            transform.position = mouseWorld + offset;
        }

        if (Input.GetKeyDown(KeyCode.R) && isDragging)
        {
            currentRotation += 90f;
            transform.rotation = Quaternion.Euler(0, 0, currentRotation % 360);
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mouseWorld;
    }

    void OnMouseUp()
    {
        isDragging = false;

        // SNAP
        Vector3 snappedPos = GetClosestValidSnapPosition();
        transform.position = snappedPos + new Vector3(0, 0.5f, 0); // Pequeno deslocamento para centrar com as tiles
    }

    Vector3 GetClosestValidSnapPosition()
    {
        Vector3 pos = transform.position;
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);

        Vector2Int gridPos = new Vector2Int(x, y);

        if (IsValidPlacement(gridPos))
            return new Vector3(x, y, 0);
        else
        {
            // Tenta encontrar a posição válida mais próxima (opcional)
            return transform.position; // Ou: voltar à posição original
        }
    }

    bool IsValidPlacement(Vector2Int gridPos)
    {
        // Tenta validar com base na direção e tamanho do barco
        Vector2 direction = GetFacingDirection();

        for (int i = 0; i < shipLength; i++)
        {
            Vector2Int checkPos = gridPos + Vector2Int.RoundToInt(direction * i);
            if (GridManager.Instance.GetTileAtPosition(checkPos) == null)
                return false;
        }

        return true;
    }

    Vector2 GetFacingDirection()
    {
        int rot = Mathf.RoundToInt(currentRotation) % 360;

        switch (rot)
        {
            case 0: return Vector2.right;
            case 90: return Vector2.up;
            case 180: return Vector2.left;
            case 270: return Vector2.down;
            default: return Vector2.right;
        }
    }
}
