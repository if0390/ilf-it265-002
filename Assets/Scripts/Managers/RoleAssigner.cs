using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoleAssigner
{
    public static List<Player> CreatePlayers()
    {
        List<PlayerRole> rolePool = new List<PlayerRole>
        {
            PlayerRole.Dreamer,
            PlayerRole.Dreamer,
            PlayerRole.Dreamer,
            PlayerRole.Nightmare,
            PlayerRole.Nightmare
        };

        List<Archetype> archetypePool = new List<Archetype>
        {
            Archetype.Warrior,
            Archetype.Singer,
            Archetype.Queen,
            Archetype.TwinA,
            Archetype.TwinB
        };

        Shuffle(rolePool);
        Shuffle(archetypePool);

        List<Player> players = new List<Player>();
        for (int i = 0; i < 5; i++)
        {
            Player p = new Player(i + 1, rolePool[i], archetypePool[i]);
            players.Add(p);
            Debug.Log($"[RoleAssigner] Player {p.PlayerNumber}: {p.GetRoleName()} - {p.GetArchetypeName()}");
        }

        return players;
    }

    private static void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}