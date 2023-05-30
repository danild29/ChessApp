using ChessServer.Data.Extensions;
using ChessServer.Hubs.GamePlay;
using ChessServer.Pages;
using System;
using System.Xml;

namespace ChessServer.Data;

public class GameInfo
{
    public int Id { get; set; }

    public string? White { get; set; } = null;
    public string? Black { get; set; } = null;
    public string? Winner { get; set; } = null;

    public bool IsGameOver => Winner != null;

    public Sides CurrentMove { get; set; } = Sides.White;
    public ChessBoard Board = new();
    public List<string> Moves = new();

    public DateTime GameStarted { get; set; }
    public DateTime GameEnded { get; set; }
    public DateTime LastMoveTime { get; set; }

    public double Duration = 15;

    public double WhitePlayed = 0;
    public double BlackPlayed = 0;




    public double TimeLeftWhite => Duration - WhitePlayed;
    public double TimeLeftBlack => Duration - BlackPlayed;




    public GameInfo()
    {

    }


    public GameInfo(int id, string whiteConId)
    {
        Id = id;
        White = whiteConId;
    }
    public void ChangeCurMove()
    {
        CurrentMove = CurrentMove.GetOpositeSide();
    }

    public void MakeMove(Move move)
    {
        Board.MakeMove(move);
        ChangeCurMove();
    }

    public string? GetEnemy(string myId)
    {
        if (White == null || Black == null) return null;
        if (White == myId) return Black;
        else if (Black == myId) return White;
        return null;
    }

}

