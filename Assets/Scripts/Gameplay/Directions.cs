using UnityEngine;

public static class Directions
{
    public static readonly Vector2Int[] Orthogonal = new Vector2Int[]
    {
        new Vector2Int(0, 1),   // Up
        new Vector2Int(1, 0),   // Right
        new Vector2Int(0, -1),  // Down
        new Vector2Int(-1, 0)   // Left
    };
    public static readonly Vector2Int[] Diagonal = new Vector2Int[]
    {
        new Vector2Int(1, 1),   // Up-Right
        new Vector2Int(1, -1),  // Down-Right
        new Vector2Int(-1, -1), // Down-Left
        new Vector2Int(-1, 1)   // Up-Left
    };
    public static readonly Vector2Int[] Universal = new Vector2Int[]
    {
        new Vector2Int(0, 1),   // Up
        new Vector2Int(1, 1),   // Up-Right
        new Vector2Int(1, 0),   // Right
        new Vector2Int(1, -1),  // Down-Right
        new Vector2Int(0, -1),  // Down
        new Vector2Int(-1, -1), // Down-Left
        new Vector2Int(-1, 0),  // Left
        new Vector2Int(-1, 1)   // Up-Left
    };
    public static readonly Vector2Int[] Knight = new Vector2Int[]
    {
        new Vector2Int(1, 2),   // Up-Right
        new Vector2Int(-1, 2),  // Up-Left
        new Vector2Int(2, 1),   // Right-Up
        new Vector2Int(2, -1),  // Right-Down
        new Vector2Int(1, -2),  // Down-Right
        new Vector2Int(-1, -2), // Down-Left
        new Vector2Int(-2, -1), // Left-Down
        new Vector2Int(-2, 1),  // Left-Up
    };
}
