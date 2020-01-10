using UnityEngine;

namespace TarotReadingPlayer.Information.Reader
{
    public class TarotReadingController : MonoBehaviour
    {
        public TarotSpreadReader reader;

        void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 150, 50), "Read One Card"))
            {
                reader.SetSpread(Spreads.OneOracle);
            }
            if (GUI.Button(new Rect(160, 10, 150, 50), "Read Three Cards"))
            {
                reader.SetSpread(Spreads.ThreeCards);
            }
            if (GUI.Button(new Rect(10, 70, 300, 50), "Read Spread one more time"))
            {
                reader.RemoveAllCards();
            }
        }
    }
}