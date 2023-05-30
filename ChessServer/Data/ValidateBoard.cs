using ChessServer.Data.Extensions;

namespace ChessServer.Data;

public static class ValidateBoard
{

    public static bool[,] FindGoodCells(ChessBoard board, int pos, Sides side)
    {
        

        bool[,] Moves = new bool[board.NumRows, board.NumCols];
        if (board == null || pos < 0 || pos > 72) return Moves;

        int y = pos / 8;
        int x = pos % 8;

        if(side == Sides.White)
        {
            if (board.Board[y, x] == Pieces.GetPiece(Piece.wPawn))
            {
                if (y == 6) Moves[y - 2, x] = true;
                Moves[y - 1, x] = true;
            }
        }
        else if (side == Sides.Black)
        {
            if (board.Board[y, x] == Pieces.GetPiece(Piece.bPawn))
            {
                if (y == 1) Moves[y + 2, x] = true;
                Moves[y + 1, x] = true;
            }
        }

        return Moves;
    }
}
