using UnityEngine;

public class GameManager : MonoBehaviour
{
    Board board;
    private void Start()
    {
        board = GetComponent<Board>();
        board.CreateBoard();
        board.ArrangePieces();
    }
}
