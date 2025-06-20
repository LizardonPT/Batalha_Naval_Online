using UnityEngine;

public class Boats : MonoBehaviour
{
    [SerializeField] private GameObject hoverColor;

    void OnMouseEnter()
    {
        hoverColor.SetActive(true);
    }

    void OnMouseExit()
    {
        hoverColor.SetActive(false);
    }
}
