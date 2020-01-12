using TarotReadingPlayer.Information.Displayer;
using TarotReadingPlayer.Information.Reader;
using UnityEngine;

namespace TarotReadingPlayer.Information.Controller
{
    public class TarotReadingController : MonoBehaviour
    {
        [SerializeField] private TarotSpreadReader reader;
        [SerializeField] private TarotReadingDisplayer displayer;
        
        public void OnSelectedOneCardReading()
        {
            reader.SetSpread(TarotSpreads.OneOracle);
        }

        public void OnSelectedThreeCardsReading()
        {
            reader.SetSpread(TarotSpreads.ThreeCards);
        }

        public void OnSelectedOneMoreTimeReading()
        {
            reader.DeleteAllRecords();
            reader.SetSpread(TarotSpreads.Default);
        }
    }
}