using System.Collections.Generic;

public static class LocationGraph
{
    private static readonly Dictionary<BoardLocation, BoardLocation[]> Connections =
        new Dictionary<BoardLocation, BoardLocation[]>
    {
        { BoardLocation.TheDream,           new[] { BoardLocation.ForgottenGarden, BoardLocation.ShatteredBallroom, BoardLocation.HollowLibrary } },
        { BoardLocation.ForgottenGarden,    new[] { BoardLocation.TheDream, BoardLocation.MirrorGate } },
        { BoardLocation.ShatteredBallroom,  new[] { BoardLocation.TheDream, BoardLocation.MirrorGate } },
        { BoardLocation.HollowLibrary,      new[] { BoardLocation.TheDream, BoardLocation.MirrorGate } },
        { BoardLocation.MirrorGate,         new[] { BoardLocation.ForgottenGarden, BoardLocation.ShatteredBallroom, BoardLocation.HollowLibrary } }
    };

    public static BoardLocation[] GetConnections(BoardLocation location)
    {
        return Connections.TryGetValue(location, out var connections) ? connections : new BoardLocation[0];
    }

    public static string GetDisplayName(BoardLocation location)
    {
        switch (location)
        {
            case BoardLocation.TheDream:          return "The Dream";
            case BoardLocation.ForgottenGarden:   return "Forgotten Garden";
            case BoardLocation.ShatteredBallroom: return "Shattered Ballroom";
            case BoardLocation.HollowLibrary:     return "Hollow Library";
            case BoardLocation.MirrorGate:        return "The Mirror Gate";
            default:                              return location.ToString();
        }
    }
}
