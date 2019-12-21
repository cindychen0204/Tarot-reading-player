using System.Numerics;
using TarotReadingPlayer.Information.Editor;

namespace TarotReadingPlayer.Information
{
    //Input
    public enum CardDirection
    {
        Default,
        Upright,
        Reversed
    }

    public class TarotCardCreator : FindCardInTarotDatabase
    {
        public TarotCardDatabase tarotDatabase;

        public Tarot FindCardAllInformation(string cardName)
        {
            var dummyTarot = new Tarot();
            foreach (var card in tarotDatabase.database)
            {
                if (TryGetCardInformationInCardDataBase(cardName))
                {
                    dummyTarot = new Tarot(card.cardName, card.cardEngName, card.keyword, direction, position,
                        card.curSituation_up, card.feelings_up, card.cause_up, card.future_up,
                        card.advice_up, card.love_up, card.work_up, card.interpersonal_up, card.other_up);
                    break;

                }
            }
            return dummyTarot;
        }

        public Tarot FindTarotCardByNameAndDirection(string cardName, CardDirection direction, Vector3 position)
        {
            var dummyTarot = new Tarot();
            foreach (var card in tarotDatabase.database)
            {
                if (TryGetCardInformationInCardDataBase(cardName))
                {
                    switch (direction)
                    {
                        case CardDirection.Default:
                            break;
                        case CardDirection.Upright:
                            dummyTarot = new Tarot(card.cardName, card.cardEngName, card.keyword, direction, position,
                                card.curSituation_up, card.feelings_up, card.cause_up, card.future_up,
                                card.advice_up, card.love_up, card.work_up, card.interpersonal_up, card.other_up);
                            break;
                        case CardDirection.Reversed:
                            dummyTarot = new Tarot(card.cardName, card.cardEngName, card.keyword, direction, position,
                                card.curSituation_re, card.feelings_re, card.cause_re, card.feelings_re,
                                card.advice_re, card.love_re, card.work_re, card.interpersonal_re, card.other_re);
                            break;
                    }
                }
            }
            return dummyTarot;
        }

        private bool TryGetCardInformationInCardDataBase(string cardName)
        {
            foreach (var card in tarotDatabase.database)
            {
                if (cardName.Contains(card.cardEngName))
                {
                    return true;
                }
            }
            return false;
        }
    }
}