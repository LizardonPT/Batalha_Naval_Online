using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject hoverColor;

    public void Initialize(bool isOffset)
    {
        if (isOffset)
        {
            spriteRenderer.color = offsetColor;
        }
        else
        {
            spriteRenderer.color = baseColor;
        }
    }

    void OnMouseEnter()
    {
        hoverColor.SetActive(true);
    }

    void OnMouseExit()
    {
        hoverColor.SetActive(false);
    }
}
