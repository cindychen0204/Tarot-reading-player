using TarotReadingPlayer.Information;
using UnityEngine;

namespace TarotReadingPlayer.Information.Reader
{
    public interface IFindCardInformation
    {
        TarotCard FindCardAllInformation(string cardName);

        TarotCard FindTarotCardByNameAndDirection(string cardName, CardDirection direction, Vector3 position);
    }
}