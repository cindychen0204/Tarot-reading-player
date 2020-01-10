using TarotReadingPlayer.Information;
using UnityEngine;

namespace TarotReadingPlayer.Information.Reader
{
    public interface IFindCardInformation
    {
        void ReadFromDatabase();

        TarotCard FindCardAllInformation(string cardName);

        TarotCard FindTarotCardByNameAndDirection(string cardName, CardDirection direction, Vector3 position);
    }
}