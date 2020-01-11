using TarotReadingPlayer.Information.Displayer;
using TarotReadingPlayer.Information.Reader;
using UnityEngine;

namespace TarotReadingPlayer.Information.Controller
{
    public class TarotReadingController : MonoBehaviour
    {
        public TarotSpreadReader reader;
        private TarotReadingDisplayer displayer;
        
        public void OnSelectedOneCardReading()
        {
            reader.SetSpread(TarotSpreads.OneOracle);
            var msg = "Finding One Card...";
            displayer.ShowMessage(msg);
        }

        public void OnSelectedThreeCardsReading()
        {
            reader.SetSpread(TarotSpreads.ThreeCards);
            var msg = "Finding Three Cards...";
            displayer.ShowMessage(msg);
        }

        public void OnSelectedOneMoreTimeReading()
        {
            reader.DeleteAllRecords();
            reader.SetSpread(TarotSpreads.Default);
            var msg = "Please Select Button";
            displayer.ShowMessage(msg);
        }
    }
}