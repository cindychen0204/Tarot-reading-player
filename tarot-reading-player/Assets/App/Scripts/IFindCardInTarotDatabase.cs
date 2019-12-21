using TarotReadingPlayer.Information;
using UnityEngine;

namespace TarotReadingPlayer.Information.Reader
{
    public interface IFindCardInTarotDatabase
    {
        void RoadDatabase();

        Tarot FindCardAllInformation(string cardName);

        Tarot FindTarotCardByNameAndDirection(string cardName, CardDirection direction, Vector3 position);
    }
}