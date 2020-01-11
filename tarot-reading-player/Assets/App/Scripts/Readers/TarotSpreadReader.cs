using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TarotReadingPlayer.Information.Displayer;

namespace TarotReadingPlayer.Information.Reader
{
    public enum TarotSpreads
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

    public enum ThreeCardsReadingMethods
    {
        Default,

        //運の流れ
        Past_Now_NearFuture,

        //問題の対処法
        Cause_Result_Advice,

        //判断の仕方
        Yes_Hold_No
    }

    public class TarotSpreadReader : MonoBehaviour
    {
        //後の実装でUIから決めることにする
        [SerializeField]
        private TarotSpreads currentTarotSpread = TarotSpreads.Default;

        public TarotSpreads CurrentTarotSpread => currentTarotSpread;

        [SerializeField]
        private ThreeCardsReadingMethods threeCardMethod = ThreeCardsReadingMethods.Default;

        public ThreeCardsReadingMethods ThreeCardMethod => threeCardMethod;

        [SerializeField] private TarotReadingDisplayer displayer;

        public TarotCardDatabaseObject TarotDatabaseObject { get; }

        private readonly List<TarotCard> detectCardList = new List<TarotCard>();

        private readonly List<string> cardNameList = new List<string>();

        void Start()
        {
            //var msg = "Please Select Button";
            //ShowTextMessage(msg);
        }

        #region 外クラス用

        public void SetSpread(TarotSpreads tarotSpread)
        {
            currentTarotSpread = tarotSpread;
        }

        public void AddDetectCard(TarotCard tarotCard)
        {
            if (cardNameList.Contains(tarotCard.Name)) return;
            Debug.Log("Detected Card number" + detectCardList.Count);
            cardNameList.Add(tarotCard.Name);
            detectCardList.Add(tarotCard);
            ReadCardWithSettingsSpreads();
        }

        public void DeleteAllRecords()
        {
            cardNameList.Clear();
            detectCardList.Clear();
        }

        /// <summary>
        /// カード情報を受け取り、設定されたスプレッドに対応させる
        /// </summary>
        /// <param name="tarotCard"></param>
        public void ReadCardWithSettingsSpreads()
        {
            switch (currentTarotSpread)
            {
                case TarotSpreads.Default:
                    break;
                case TarotSpreads.OneOracle:
                    if (detectCardList.Count == 1)
                    {
                        ReadOneCard();
                    }
                    break;
                case TarotSpreads.ThreeCards:
                    if (detectCardList.Count == 3)
                    {
                        ReadThreeCards();
                    }
                    break;
                case TarotSpreads.Alternatively:
                    break;
                case TarotSpreads.Hexagram:
                    break;
                case TarotSpreads.CelticCross:
                    break;
                case TarotSpreads.Horseshoe:
                    break;
                case TarotSpreads.Horoscope:
                    break;
                case TarotSpreads.HeartSonar:
                    break;
                case TarotSpreads.Calendar:
                    break;
            }
        }
        #endregion

        #region カードの向きにより情報を取り出すメソッド
        private static string Direction_ConvertToString(TarotCard card)
        {
            var directionResult = "";
            if (card.Direction == CardDirection.Upright)
            {
                directionResult = "正位";
            }
            else if (card.Direction == CardDirection.Reversed)
            {
                directionResult = "逆位";
            }
            return directionResult;
        }

        private static string Love_ConvertToString(TarotCard card)
        {
            var loveResult = "";
            if (card.Direction == CardDirection.Upright)
            {
                loveResult = card.Love_Up;
            }
            else if (card.Direction == CardDirection.Reversed)
            {
                loveResult = card.Love_Re;
            }
            return loveResult;
        }

        private static string Word_ConvertToString(TarotCard card)
        {
            var wordResult = "";
            if (card.Direction == CardDirection.Upright)
            {
                wordResult = card.Work_Up;
            }
            else if (card.Direction == CardDirection.Reversed)
            {
                wordResult = card.Work_Re;
            }
            return wordResult;
        }

        private static string Advice_ConvertToString(TarotCard card)
        {
            var advice = "";
            if (card.Direction == CardDirection.Upright)
            {
                advice = card.Advice_Up;
            }
            else if (card.Direction == CardDirection.Reversed)
            {
                advice = card.Advice_Re;
            }
            return advice;
        }
        #endregion

        #region カード情報の読み込み
        private void ReadOneCard()
        {
            var card = detectCardList[0];
            var work = Word_ConvertToString(card);
            var love = Love_ConvertToString(card);
            var advice = Advice_ConvertToString(card);
            var direction = Direction_ConvertToString(card);

            var msg = string.Format("{0}のカードの{1}です。\n 仕事運は：{2}\n恋愛運は： {3} \n アドバイス：{4}",
                card.Name,
                direction,
                work,
                love,
                advice);

            ShowResult(msg);
        }

        /// <summary>
        /// 位置違いにより、カードのそれぞれ該当する場所を判別
        /// 現状は「過去、現在、未来」の恋愛運のみ
        /// </summary>
        private void ReadThreeCards()
        {
            //In order
            var sortedList = detectCardList.OrderBy(card => card.Position.x).ToList();
            var pastCard = sortedList[0];
            var currentCard = sortedList[1];
            var futureCard = sortedList[2];

            var pastLove = Love_ConvertToString(pastCard);
            var pastDirection = Direction_ConvertToString(pastCard);

            var currentLove = Love_ConvertToString(currentCard);
            var currentDirection = Direction_ConvertToString(currentCard);

            var futureLove = Love_ConvertToString(futureCard);
            var futureDirection = Direction_ConvertToString(futureCard);

            var msg = string.Format("（恋愛運） "+
                                    "\n 一番左は{0}カードの{1}です。つまり、過去は{2}な状況でした" +
                                    "\n 真ん中は{3}カードの{4}です。今は{5}な状況にあります。" +
                                    "\n 一番右は{6}カードの{7}です。今は{8}な状況にあります。",
                pastCard.Name,
                pastDirection,
                pastLove,
                currentCard.Name,
                currentDirection,
                currentLove,
                futureCard.Name,
                futureDirection,
                futureLove);

            ShowResult(msg);
           
        }
        #endregion

        public void ShowTextMessage(string msg)
        {
            displayer.ShowMessage(msg);
        }

        public void ShowResult(string msg)
        {
            displayer.ShowMessage(msg);
            displayer.DisplayResult();
            DeleteAllRecords();
        }
    }
}