using UnityEngine;
using System.Collections.Generic;

public class ShipManager : MonoBehaviour
{
    public static ShipManager Instance;
    [SerializeField] private GameObject smallShipPrefab;
    [SerializeField] private GameObject mediumShipPrefab;
    [SerializeField] private GameObject largeShipPrefab;

    private List<GameObject> spawnedShips = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }

    internal void InitializeShips()
    {
        // Posições iniciais
        Vector2[] smallShipPositions = { new Vector2(8, 2), new Vector2(9, 2) };
        Vector2 mediumShipPosition = new Vector2(11, 2);
        Vector2 largeShipPosition = new Vector2(13, 2);

        // Dois barcos pequenos
        foreach (var pos in smallShipPositions)
        {
            var ship = Instantiate(smallShipPrefab, pos, Quaternion.identity);
            ship.name = "SmallShip";
            spawnedShips.Add(ship);
        }

        // Um barco médio
        var medium = Instantiate(mediumShipPrefab, mediumShipPosition, Quaternion.identity);
        medium.name = "MediumShip";
        spawnedShips.Add(medium);

        // Um barco grande
        var large = Instantiate(largeShipPrefab, largeShipPosition, Quaternion.identity);
        large.name = "LargeShip";
        spawnedShips.Add(large);
    }
}
