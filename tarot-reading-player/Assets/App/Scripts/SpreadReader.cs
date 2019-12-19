using TarotReadingPlayer.Information.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace TarotReadingPlayer.Information.Reader
{
    public class SpreadReader : MonoBehaviour
    {
        //後の実装でUIから決めることにする
        public Spreads CurrentSpreads = Spreads.OneOracle;

        public ThreeCardsReadingMethods mothod = ThreeCardsReadingMethods.Default;

        public Text cardMessage;

        public TarotCardDatabase tarotDatabase;

        public void ReadOneCard(ARTrackedImage card)
        {
            foreach (var tarotCard in tarotDatabase.database)
            {
                if (tarotCard.cardEngName == card.referenceImage.name)
                {
                    Debug.Log(tarotCard.keyword);
                    Debug.Log(tarotCard.advice_re);
                    Debug.Log(tarotCard.cause_re);
                }
            }
        }
    }
}