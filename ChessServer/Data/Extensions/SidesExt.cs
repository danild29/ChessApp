namespace ChessServer.Data.Extensions;

public static class SidesExt
{
    public static Sides GetOpositeSide(this Sides side)
    {
        if (side == Sides.White) return Sides.Black;
        return Sides.White;
    }
}

public enum Sides
{
    White,
    Black

};