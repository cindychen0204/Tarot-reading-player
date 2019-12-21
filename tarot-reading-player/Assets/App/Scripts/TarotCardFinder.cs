using TarotReadingPlayer.Information.Editor;
using UnityEditor;
using UnityEngine;

namespace TarotReadingPlayer.Information.Reader
{
    public class TarotCardCreator : IFindCardInTarotDatabase
    {
        public TarotCardDatabaseObject TarotDatabaseObject;

        private static string DATABASE_PATH = @"Assets/Database/TarotDB.asset";

        public void RoadDatabase()
        {
            TarotDatabaseObject =
                (TarotCardDatabaseObject) AssetDatabase.LoadAssetAtPath(DATABASE_PATH, typeof(TarotCardDatabaseObject));
        }

        public Tarot FindCardAllInformation(string cardName)
        {
            if (TarotDatabaseObject == null)
            {
                RoadDatabase();
            }

            var dummyTarot = new Tarot();
            foreach (var card in TarotDatabaseObject.database)
            {
                if (TryGetCardInformationInCardDataBase(card, cardName))
                {
                    dummyTarot = new Tarot(card.cardName, card.cardEngName, card.number, card.keyword,
                        card.curSituation_up, card.curSituation_re, card.feelings_up, card.feelings_re, card.cause_up,
                        card.cause_re, card.future_up, card.future_re,
                        card.advice_up, card.advice_re, card.love_up, card.love_re, card.work_up, card.work_re,
                        card.interpersonal_up, card.interpersonal_re, card.other_up, card.other_re);
                    break;
                }
            }

            return dummyTarot;
        }

        public Tarot FindTarotCardByNameAndDirection(string cardName, CardDirection direction, Vector3 position)
        {
            if (TarotDatabaseObject == null)
            {
                RoadDatabase();
            }

            var dummyTarot = new Tarot();
            foreach (var card in TarotDatabaseObject.database)
            {
                if (TryGetCardInformationInCardDataBase(card, cardName))
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
                                card.curSituation_re, card.feelings_re, card.cause_re, card.future_re,
                                card.advice_re, card.love_re, card.work_re, card.interpersonal_re, card.other_re);
                            break;
                    }
                }
            }

            return dummyTarot;
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