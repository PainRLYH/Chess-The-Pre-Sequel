    using UnityEngine;

    public class Pawn : Piece
    {
        protected override void Start()
        {
            base.Start();
            health = 5;
            attack_pwr = 2;
            defense = 1;
            attack_Type = Attack_Types.Slashing;
            pierce_resist = 1.25f;
            slash_resist = 1.0f;
            bludgeon_resist = 0.75f;
        }

       public override bool legal_Move(Tile target_Tile)
       {
            // 1 tile step forward
            if (target_Tile.occupied_By == null)
            {
                if (target_Tile.coordinates.y == current_Coordinates.y + 1)
                {
                    if (target_Tile.coordinates.x == current_Coordinates.x)
                    {
                        return true;
                    }
                }
            }

            // 2 tile step forward
            if (target_Tile.occupied_By == null)
            {
                if (target_Tile.coordinates.y == current_Coordinates.y + 2 && current_Coordinates.y == 1)
                {
                    if (board.tiles[current_Coordinates.x, current_Coordinates.y + 1].GetComponent<Tile>().occupied_By == null)
                    {
                        if (target_Tile.coordinates.x == current_Coordinates.x)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
       }

    public override bool legal_Attack(Tile target_Tile)
    {
        if (target_Tile.coordinates.y == current_Coordinates.y + 1)     // Check if the target tile is one step forward
        {
            if (Mathf.Abs(target_Tile.coordinates.x - current_Coordinates.x) == 1)      // Check if the target tile is one step diagonally
            {
                return true;
            }
        }
        return false;
    }
}

