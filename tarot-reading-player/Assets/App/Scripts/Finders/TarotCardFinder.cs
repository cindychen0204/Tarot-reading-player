using System;
using TarotReadingPlayer.Information.Editor;
using UnityEngine;

namespace TarotReadingPlayer.Information.Reader
{
    public class TarotCardFinder : IFindCardInformation
    {
        public TarotCardDatabaseObject TarotDatabaseObject;

        public void ObtainDatabase()
        {
            TarotDatabaseObject = GameObject.Find("AR Session Origin").GetComponent<TarotSpreadReader>().TarotDatabaseObject;
        }

        public TarotCard FindCardAllInformation(string cardName)
        {
            if (TarotDatabaseObject == null) ObtainDatabase();
            try
            {
                var dummyTarot = new TarotCard();
                foreach (var card in TarotDatabaseObject.database)
                {
                    if (TryGetCardInformationInCardDataBase(card, cardName))
                    {
                        dummyTarot = new TarotCard(card.cardName, card.cardEngName, card.number, card.keyword,
                            card.curSituation_up, card.curSituation_re, card.feelings_up, card.feelings_re, card.cause_up,
                            card.cause_re, card.future_up, card.future_re,
                            card.advice_up, card.advice_re, card.love_up, card.love_re, card.work_up, card.work_re,
                            card.interpersonal_up, card.interpersonal_re, card.other_up, card.other_re);
                        break;
                    }
                }
                return dummyTarot;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public TarotCard FindTarotCardByNameAndDirection(string cardName, CardDirection direction, Vector3 position)
        {
            if (TarotDatabaseObject == null) ObtainDatabase();
            try
            {
                var dummyTarot = new TarotCard();
                foreach (var card in TarotDatabaseObject.database)
                {
                    if (TryGetCardInformationInCardDataBase(card, cardName))
                    {
                        switch (direction)
                        {
                            case CardDirection.Default:
                                break;
                            case CardDirection.Upright:
                                dummyTarot = new TarotCard(card.cardName, card.cardEngName, card.keyword, direction, position,
                                    card.curSituation_up, card.feelings_up, card.cause_up, card.future_up,
                                    card.advice_up, card.love_up, card.work_up, card.interpersonal_up, card.other_up);
                                break;
                            case CardDirection.Reversed:
                                dummyTarot = new TarotCard(card.cardName, card.cardEngName, card.keyword, direction, position,
                                    card.curSituation_re, card.feelings_re, card.cause_re, card.future_re,
                                    card.advice_re, card.love_re, card.work_re, card.interpersonal_re, card.other_re);
                                break;
                        }
                    }
                }
                return dummyTarot;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private bool TryGetCardInformationInCardDataBase(TarotCardInformation card, string cardName)
        {
            if (cardName.Contains(card.cardEngName))
            {
                return true;
            }
            return false;
        }
    }
}