public class WinResult
{
    public PlayerRole Winner { get; private set; }
    public string Reason { get; private set; }

    public WinResult(PlayerRole winner, string reason)
    {
        Winner = winner;
        Reason = reason;
    }
}
