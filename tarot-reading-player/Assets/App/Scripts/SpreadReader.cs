using TarotReadingPlayer.Information.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace TarotReadingPlayer.Information.Reader
{
    public enum Spreads
    {
        Default,
        OneOracle,
        ThreeCards,
        Alternatively,
        Hexagram,
        CelticCross,
        Horseshoe,
        Horoscope,
        HeartSonar,
        Calendar
    }

    public class SpreadReader : MonoBehaviour
    {
        //後の実装でUIから決めることにする
        private Spreads CurrentSpreads = Spreads.Default;

        public ThreeCardsReadingMethods method = ThreeCardsReadingMethods.Default;

        public Text cardMessage;

        public TarotCardDatabase tarotDatabase;

        public TrackedImageInfoManager trackManager;

        public void SetSpread(Spreads spread)
        {
            CurrentSpreads = spread;
        }

        public void ReadOneCard(ARTrackedImage card, string direction)
        {
            Debug.Log("Search card..." + card.referenceImage.name);
            foreach (var tarotCard in tarotDatabase.database)
            {
                if (card.referenceImage.name.Contains(tarotCard.cardEngName))
                {
                    var cardName = card.referenceImage.name;
                    cardMessage.text = string.Format(
                        "{0}\ntrackingState: {1}\nGUID: {2}\nReference size: {3} cm\nDetected size: {4} cm\nDirection: {5}\n Keyword: {6}\nAdvice: {7}",
                        cardName,
                        card.trackingState,
                        card.referenceImage.guid,
                        card.referenceImage.size * 100f,
                        card.size * 100f,
                        direction,
                        tarotCard.keyword,
                        tarotCard.advice_up);
                }
            }
        }

    }
}