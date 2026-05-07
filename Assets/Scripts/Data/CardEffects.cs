using System.Collections.Generic;
using UnityEngine;

public static class CardEffects
{
    public static string Resolve(CardType card, Player player)
    {
        switch (card)
        {
            case CardType.DreamFragment: return ResolveDreamFragment(player);
            case CardType.WorldFlip:     return ResolveWorldFlip(player);
            case CardType.GuidingLight:  return ResolveGuidingLight(player);
            case CardType.LockedDoor:    return ResolveLockedDoor(player);
            case CardType.FadingMemory:  return ResolveFadingMemory(player);
            default: return "";
        }
    }

    private static string ResolveDreamFragment(Player player)
    {
        var loc = player.CurrentLocation;
        var ws = GameManager.Instance.CurrentWorldState;

        bool canCollect =
            loc == BoardLocation.ShatteredBallroom ||
            (loc == BoardLocation.ForgottenGarden && ws == WorldState.Dream) ||
            (loc == BoardLocation.HollowLibrary  && ws == WorldState.Nightmare);

        if (canCollect)
        {
            player.FragmentsCollected++;
            return $"collected a Dream Fragment ({player.FragmentsCollected} total)";
        }
        return $"found nothing — {LocationGraph.GetDisplayName(loc)} doesn't yield fragments in {ws} state";
    }

    private static string ResolveWorldFlip(Player player)
    {
        GameManager.Instance.FlipWorldState();
        return $"flipped the world to <b>{GameManager.Instance.CurrentWorldState}</b>";
    }

    private static string ResolveGuidingLight(Player player)
    {
        var others = new List<Player>();
        foreach (var p in GameManager.Instance.Players)
            if (p != player && p.IsAlive) others.Add(p);
        if (others.Count == 0) return "found no one to guide";

        var target = others[Random.Range(0, others.Count)];
        var connections = LocationGraph.GetConnections(target.CurrentLocation);
        var available = new List<BoardLocation>();
        foreach (var l in connections)
            if (!GameManager.Instance.LockedLocations.Contains(l)) available.Add(l);
        if (available.Count == 0) return $"tried to guide Player {target.PlayerNumber} but every path was locked";

        var dest = available[Random.Range(0, available.Count)];
        target.CurrentLocation = dest;
        return $"guided Player {target.PlayerNumber} to <b>{LocationGraph.GetDisplayName(dest)}</b>";
    }

    private static string ResolveLockedDoor(Player player)
    {
        var allLocs = new[]
        {
            BoardLocation.ForgottenGarden,
            BoardLocation.ShatteredBallroom,
            BoardLocation.HollowLibrary,
            BoardLocation.MirrorGate
        };
        var available = new List<BoardLocation>();
        foreach (var l in allLocs)
            if (!GameManager.Instance.LockedLocations.Contains(l)) available.Add(l);
        if (available.Count == 0) return "found every door already locked";

        var locked = available[Random.Range(0, available.Count)];
        GameManager.Instance.LockLocation(locked);
        return $"locked <b>{LocationGraph.GetDisplayName(locked)}</b> for the rest of the round";
    }

    private static string ResolveFadingMemory(Player player)
    {
        int added = 0;
        for (int i = 0; i < 3; i++)
        {
            var card = GameManager.Instance.DrawCard();
            if (!card.HasValue) break;
            GameManager.Instance.ConsumeCard(card.Value);
            added++;
        }
        return $"burned <b>{added}</b> cards into the Consumed pile";
    }
}
