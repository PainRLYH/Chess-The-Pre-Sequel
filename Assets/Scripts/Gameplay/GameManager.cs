using UnityEngine;

public class GameManager : MonoBehaviour
{
    Board m_board;
    private void Start()
    {
        m_board = GetComponent<Board>();
        m_board.CreateBoard();
        m_board.ArrangePieces();
    }
}
