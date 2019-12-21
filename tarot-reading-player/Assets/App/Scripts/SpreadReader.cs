using System;
using System.Collections.Generic;
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
        private Spreads currentSpread = Spreads.Default;

        public Spreads CurrentSpread => currentSpread;

        public ThreeCardsReadingMethods method = ThreeCardsReadingMethods.Default;

        public Text cardMessage;

        public TarotCardDatabase tarotDatabase;

        public List<Tarot> detectCardList = new List<Tarot>();

        public void SetSpread(Spreads spread)
        {
            currentSpread = spread;
        }

        public void ReadCard(Tarot tarotCard)
        {
            detectCardList.Add(tarotCard);

            switch (currentSpread)
            {
                case Spreads.Default:
                    break;
                case Spreads.OneOracle:
                    if (detectCardList.Count == 1)
                    {
                        //TODO
                    }
                    break;
                case Spreads.ThreeCards:
                    if (detectCardList.Count == 3)
                    {
                        ReadThreeCards();
                    }
                    break;
                case Spreads.Alternatively:
                    break;
                case Spreads.Hexagram:
                    break;
                case Spreads.CelticCross:
                    break;
                case Spreads.Horseshoe:
                    break;
                case Spreads.Horoscope:
                    break;
                case Spreads.HeartSonar:
                    break;
                case Spreads.Calendar:
                    break;
            }
        }

        private void ReadThreeCards()
        {
            //TODO
        }
    }
}