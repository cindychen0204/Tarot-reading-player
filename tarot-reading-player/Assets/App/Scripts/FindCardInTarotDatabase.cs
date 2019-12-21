using System.Numerics;

namespace TarotReadingPlayer.Information
{
    public interface FindCardInTarotDatabase
    {
        Tarot FindCardAllInformation(string cardName);

        Tarot FindTarotCardByNameAndDirection(string cardName, CardDirection direction, Vector3 position);
    }
}