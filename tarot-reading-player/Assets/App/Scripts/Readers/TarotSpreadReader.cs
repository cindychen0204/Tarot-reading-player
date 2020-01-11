using System.Linq;
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

        private List<string> cardNameList = new List<string>();

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
            if (cardNameList.Contains(tarotCard.Name)) return;
            Debug.Log("Detected Card number" + detectCardList.Count);
            cardNameList.Add(tarotCard.Name);
            detectCardList.Add(tarotCard);
            ReadCard();
        }

        public void RemoveAllCards(){
            cardNameList.Clear();
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
        /// 現状は「過去、現在、未来」の恋愛運のみ
        /// </summary>
        private void ReadThreeCards()
        {
            //In order
            List<TarotCard> SortedList = detectCardList.OrderBy(card => card.Position.x).ToList();
            var pastCart = SortedList[0];
            var currentCard = SortedList[1];
            var futrueCard = SortedList[2];
            Debug.Log("pastCart:" + pastCart.Name);
            Debug.Log("currentCard:" + currentCard.Name);
            Debug.Log("futrueCard:" + futrueCard.Name);
            
            var pastLove = "";
            var pastDirection = "";
            if (pastCart.Direction == CardDirection.Upright)
            {
                pastDirection = "正位";
                pastLove = pastCart.Love_Up;
            }
            else if (pastCart.Direction == CardDirection.Reversed)
            {
                pastDirection = "逆位";
                pastLove = pastCart.Love_Re;
            }

            var currentLove = "";
            var currentDirection = "";
            if (currentCard.Direction == CardDirection.Upright)
            {
                currentDirection = "正位";
                currentLove = currentCard.Love_Up;
            }
            else if (currentCard.Direction == CardDirection.Reversed)
            {
                currentDirection = "逆位";
                currentLove = currentCard.Love_Re;
            }

            var futureLove = "";
            var futureDirection = "";
            if (futrueCard.Direction == CardDirection.Upright)
            {
                futureDirection = "正位";
                futureLove = futrueCard.Love_Up;
            }
            else if (futrueCard.Direction == CardDirection.Reversed)
            {
                futureDirection = "逆位";
                futureLove = futrueCard.Love_Re;
            }

            var msg = string.Format("（恋愛運）\n 一番左は{0}カードの{1}です。つまり、過去は{2}な状況でした。\n" +
                                     "\n 真ん中は{3}カードの{4}です。今は{5}な状況にあります。\n "+
                                     "\n 一番右は{6}カードの{7}です。今は{8}な状況にあります。",
                pastCart.Name,
                pastDirection,
                pastLove,
                currentCard.Name,
                currentDirection,
                currentLove,
                futrueCard.Name,
                futureDirection,
                futureLove);
            cardMessage.text = msg;
        }

        private void ReadOneCard()
        {
            var work = "";
            var love = "";
            var advice = "";
            var direction = "";
            var card = detectCardList[0];
            if (card.Direction == CardDirection.Upright)
            {
                direction = "正位";
                work = card.Work_Up;
                love = card.Love_Up;
                advice = card.Advice_Up;
            }
            else if (card.Direction == CardDirection.Reversed)
            {
                direction = "逆位";
                work = card.Work_Re;
                love = card.Love_Re;
                advice = card.Advice_Re;
            }

            var msg = string.Format("{0}のカードの{1}です。\n 仕事運は：{2}\n恋愛運は： {3} \n アドバイス：{4}",
                card.Name,
                direction,
                work,
                love,
                advice);
            cardMessage.text = msg;
        }
    }
}