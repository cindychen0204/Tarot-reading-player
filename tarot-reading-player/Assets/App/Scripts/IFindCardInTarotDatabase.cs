using TarotReadingPlayer.Information.Editor;
using UnityEngine;

namespace TarotReadingPlayer.Information
{
    public interface IFindCardInTarotDatabase
    {
        void RoadDatabase();

        Tarot FindCardAllInformation(string cardName);

        Tarot FindTarotCardByNameAndDirection(string cardName, CardDirection direction, Vector3 position);
    }
}