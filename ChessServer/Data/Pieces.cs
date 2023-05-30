namespace ChessServer.Data;

public static class Pieces
{

    public static string GetPiece(Piece name)
    {
        switch (name)
        {
            case Piece.wKing:
                return "♚";
            case Piece.wQueen:
                return "♛";
            case Piece.wRook:
                return "♜";
            case Piece.wHorse:
                return "♞";
            case Piece.wBishop:
                return "♝";
            case Piece.wPawn:
                return "♙";

            case Piece.bKing:
                return "1";
            case Piece.bQueen:
                return "2";
            case Piece.bRook:
                return "3";
            case Piece.bHorse:
                return "4";
            case Piece.bBishop:
                return "5";
            case Piece.bPawn:
                return "6";

            default:
                return " ";
        }
    }

}
public enum Piece
{
    wKing, 
    wQueen,
    wBishop, 
    wHorse,
    wRook,
    wPawn,
    bKing,
    bQueen,
    bBishop,
    bHorse,
    bRook,
    bPawn
}
