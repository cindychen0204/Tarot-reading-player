using UnityEngine;

namespace TarotReadingPlayer.Information.Reader
{
    public class TarotReadingController : MonoBehaviour
    {
        public TarotSpreadReader reader;
        
        public void OnSelectedOneCardReading()
        {
            reader.SetSpread(Spreads.OneOracle);
            reader.cardMessage.text = "Finding One Card...";
        }

        public void OnSelectedThreeCardsReading()
        {
            reader.SetSpread(Spreads.ThreeCards);
            reader.cardMessage.text = "Finding Three Cards...";
        }

        public void OnSelectedOneMoreTimeReading()
        {
            reader.RemoveAllCards();
            reader.SetSpread(Spreads.Default);
            reader.cardMessage.text = "Please Select Button";
        }
    }
}