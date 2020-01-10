using System.Collections.Generic;
using TarotReadingPlayer.Information.Editor;
using UnityEngine;
using UnityEngine.UI;

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

    public class TarotSpreadReader : MonoBehaviour
    {
        //後の実装でUIから決めることにする
        private Spreads currentSpread = Spreads.Default;

        public Spreads CurrentSpread => currentSpread;

        public ThreeCardsReadingMethods method = ThreeCardsReadingMethods.Default;

        public Text cardMessage;

        public TarotCardDatabaseObject TarotDatabaseObject;

        public List<TarotCard> detectCardList = new List<TarotCard>();

        public void SetSpread(Spreads spread)
        {
            currentSpread = spread;
        }

        /// <summary>
        /// カード情報を受け取り、設定されたスプレッドに対応させる
        /// </summary>
        /// <param name="tarotCardCard"></param>
        public void ReadCard(TarotCard tarotCardCard)
        {
            detectCardList.Add(tarotCardCard);

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

        /// <summary>
        /// 位置違いにより、カードのそれぞれ該当する場所を判別
        /// </summary>
        private void ReadThreeCards()
        {
            //TODO
        }
    }
}