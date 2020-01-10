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
        [SerializeField]
        private Spreads currentSpread = Spreads.Default;

        public Spreads CurrentSpread => currentSpread;

        public ThreeCardsReadingMethods method = ThreeCardsReadingMethods.Past_Now_NearFuture;

        public Text cardMessage;

        public TarotCardDatabaseObject TarotDatabaseObject;

        private List<TarotCard> detectCardList = new List<TarotCard>();

        void Start()
        {
            cardMessage.text = "Please Select Button";
        }

        public void SetSpread(Spreads spread)
        {
            currentSpread = spread;
        }

        public void AddDetectCard(TarotCard tarotCard)
        {
            cardMessage.text = "Detected card.";
            detectCardList.Add(tarotCard);
            ReadCard();
        }

        public void RemoveAllCards()
        {
            detectCardList.Clear();
        }


        /// <summary>
        /// カード情報を受け取り、設定されたスプレッドに対応させる
        /// </summary>
        /// <param name="tarotCard"></param>
        public void ReadCard()
        {
            switch (currentSpread)
            {
                case Spreads.Default:
                    break;
                case Spreads.OneOracle:
                    if (detectCardList.Count == 1)
                    {
                        ReadOneCard();
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

        private void ReadOneCard()
        {
            var work = "";
            var love = "";
            var advice = "";
            var card = detectCardList[0];
            if (card.Direction == CardDirection.Upright)
            {
                work = card.Work_Up;
                love = card.Love_Up;
                advice = card.Advice_Up;
            }
            else if (card.Direction == CardDirection.Reversed)
            {
                work = card.Work_Re;
                love = card.Love_Re;
                advice = card.Advice_Re;
            }

            var msg = string.Format("{0}\nのカードです。向きは{1}\nです。 仕事運は：{2} \n恋愛運は： {3} \n アドバイス：{4}",
                card.Name,
                card.Direction,
                work,
                love,
                advice);
            cardMessage.text = msg;
        }
    }
}