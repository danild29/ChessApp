using ChessServer.Hubs.GamePlay;

namespace ChessServer.Data;

public class ChessBoard
{
    public  int NumRows = 8;
    public int NumCols = 8;

    public string[,] Board;


    public bool IsEmpty(int pos)
    {
        return Board[pos / 8, pos % 8] == " ";
    }

    public void MakeMove(Move move)
    {
        Board[move.Toy, move.Tox] = Board[move.Fromy, move.Fromx];
        Board[move.Fromy, move.Fromx] = " ";
    }

    public ChessBoard()
    {
        Board = new string[NumRows, NumCols];

        for (int i = 0; i < NumRows; i++)
            for (int j = 0; j < NumRows; j++) Board[i, j] = " ";

        Board[0, 0] = Pieces.GetPiece(Piece.bRook);
        Board[0, 1] = Pieces.GetPiece(Piece.bHorse);
        Board[0, 2] = Pieces.GetPiece(Piece.bBishop);
        Board[0, 3] = Pieces.GetPiece(Piece.bKing);
        Board[0, 4] = Pieces.GetPiece(Piece.bQueen);
        Board[0, 5] = Pieces.GetPiece(Piece.bBishop);
        Board[0, 6] = Pieces.GetPiece(Piece.bHorse);
        Board[0, 7] = Pieces.GetPiece(Piece.bRook);
        
        Board[1, 0] = Pieces.GetPiece(Piece.bPawn);
        Board[1, 1] = Pieces.GetPiece(Piece.bPawn);
        Board[1, 2] = Pieces.GetPiece(Piece.bPawn);
        Board[1, 3] = Pieces.GetPiece(Piece.bPawn);  
        Board[1, 4] = Pieces.GetPiece(Piece.bPawn);
        Board[1, 5] = Pieces.GetPiece(Piece.bPawn);
        Board[1, 6] = Pieces.GetPiece(Piece.bPawn);
        Board[1, 7] = Pieces.GetPiece(Piece.bPawn);

        Board[6, 0] = Pieces.GetPiece(Piece.wPawn);
        Board[6, 1] = Pieces.GetPiece(Piece.wPawn);
        Board[6, 2] = Pieces.GetPiece(Piece.wPawn);
        Board[6, 3] = Pieces.GetPiece(Piece.wPawn);
        Board[6, 4] = Pieces.GetPiece(Piece.wPawn);
        Board[6, 5] = Pieces.GetPiece(Piece.wPawn);
        Board[6, 6] = Pieces.GetPiece(Piece.wPawn);
        Board[6, 7] = Pieces.GetPiece(Piece.wPawn);

        Board[7, 0] = Pieces.GetPiece(Piece.wRook);
        Board[7, 1] = Pieces.GetPiece(Piece.wHorse);
        Board[7, 2] = Pieces.GetPiece(Piece.wBishop);
        Board[7, 3] = Pieces.GetPiece(Piece.wKing);
        Board[7, 4] = Pieces.GetPiece(Piece.wQueen);
        Board[7, 5] = Pieces.GetPiece(Piece.wBishop);
        Board[7, 6] = Pieces.GetPiece(Piece.wHorse);
        Board[7, 7] = Pieces.GetPiece(Piece.wRook);

    }
}