using UnityEngine;

public class Rook : Piece
{
    protected override void Start()
    {
        base.Start();
        health = 10;
        attack_pwr = 5;
        defense = 3;
        attack_Type = Attack_Types.Bludgeoning;
        pierce_resist = 1.0f;
        slash_resist = 0.75f;
        bludgeon_resist = 1.25f;
    }

    public override bool legal_Move(Tile target_Tile)
    {
        // X & Y checks
        if (current_Coordinates.x == target_Tile.coordinates.x || current_Coordinates.y == target_Tile.coordinates.y)
        {
            if (target_Tile.occupied_By == null)
            { 
                int min_x = Mathf.Min(current_Coordinates.x, target_Tile.coordinates.x);    // Left
                int max_x = Mathf.Max(current_Coordinates.x, target_Tile.coordinates.x);    // Right
                int min_y = Mathf.Min(current_Coordinates.y, target_Tile.coordinates.y);    // Down
                int max_y = Mathf.Max(current_Coordinates.y, target_Tile.coordinates.y);    // Up

                // Check if there are pieces in the way
                for (int i = min_x + 1; i < max_x; i++)     // Check horizontal movement
                {
                    if (board.tiles[i, current_Coordinates.y].GetComponent<Tile>().occupied_By != null)
                    {
                        return false;
                    }
                }
                for (int j = min_y + 1; j < max_y; j++)     // Check vertical movement
                {
                    if (board.tiles[current_Coordinates.x, j].GetComponent<Tile>().occupied_By != null)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        return false;
    }
}
