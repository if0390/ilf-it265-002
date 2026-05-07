using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public int PlayerNumber;           
    public PlayerRole Role;            
    public Archetype Archetype;      
    public bool IsAlive = true;       
    public int FragmentsCollected = 0;
    public BoardLocation CurrentLocation = BoardLocation.TheDream;
    public List<CardType> Hand = new List<CardType>();

    public Player(int number, PlayerRole role, Archetype archetype)
    {
        PlayerNumber = number;
        Role = role;
        Archetype = archetype;
    }

    public string GetArchetypeName()
    {
        switch (Archetype)
        {
            case Archetype.Warrior: return "Warrior (Diamond)";
            case Archetype.Singer:  return "Singer (Club)";
            case Archetype.Queen:   return "Queen (Spade)";
            case Archetype.TwinA:   return "Twin A (Heart)";
            case Archetype.TwinB:   return "Twin B (Heart)";
            default:                return "Unknown";
        }
    }

    public string GetRoleName()
    {
        return Role == PlayerRole.Dreamer ? "Dreamer" : "Nightmare";
    }
}