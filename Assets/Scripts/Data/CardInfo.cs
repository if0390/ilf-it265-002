public static class CardInfo
{
    public static string GetDisplayName(CardType card)
    {
        switch (card)
        {
            case CardType.DreamFragment: return "Dream Fragment";
            case CardType.WorldFlip:     return "World Flip";
            case CardType.GuidingLight:  return "Guiding Light";
            case CardType.LockedDoor:    return "Locked Door";
            case CardType.FadingMemory:  return "Fading Memory";
            default:                     return card.ToString();
        }
    }
}
