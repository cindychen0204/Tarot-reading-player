using System.Collections.Generic;
using NUnit.Framework;
using TarotReadingPlayer.Information.Reader;
using UnityEditor;
using UnityEngine;

namespace TarotReadingPlayer.Information.Test
{
    public class TarotReadTest
    {
        private TarotCardCreator creator;

        private IFindCardInTarotDatabase finder;

        public TarotCardDatabaseObject TarotDatabaseObject;

        private static string DATABASE_PATH = @"Assets/Database/TarotDB.asset";

        private List<string> tarotNameList = new List<string>()
        {
            "the_fool",
            "the_magician",
            "the_high_priestess",
            "the_empress",
            "the_emperor",
            "the_hierophant",
            "the_lovers",
            "the_chariot",
            "strength",
            "the_hermit",
            "wheel_of_fortune",
            "justice",
            "the_hanged_man",
            "death",
            "temperance",
            "the_devil",
            "the_tower",
            "the_star",
            "the_moon",
            "the_sun",
            "judgement",
            "the_world"
        };

        [Test]
        public void EveryExtractTarot_SameAsDataStoreInDatabase_Upright()
        {
            finder = new TarotCardCreator();
            TarotDatabaseObject =
                (TarotCardDatabaseObject) AssetDatabase.LoadAssetAtPath(DATABASE_PATH, typeof(TarotCardDatabaseObject));

            var direction = CardDirection.Upright;
            var position = Vector3.zero;

            for (int cardIndex = 0; cardIndex < tarotNameList.Count; cardIndex++)
            {
                var dummyTarot = finder.FindTarotCardByNameAndDirection(tarotNameList[cardIndex], direction, position);
                var expectTarot = TarotDatabaseObject.TarotCard(index: cardIndex);

                Assert.AreEqual(expectTarot.cardName, dummyTarot.Name);
                Assert.AreEqual(expectTarot.cardEngName, dummyTarot.EnglishName);

                Assert.AreEqual(expectTarot.curSituation_up, dummyTarot.CurrentSituationSituation_Up);
                Assert.AreEqual(expectTarot.feelings_up, dummyTarot.HumanFeelings_Up);
                Assert.AreEqual(expectTarot.cause_up, dummyTarot.ProblemCause_Up);
                Assert.AreEqual(expectTarot.future_up, dummyTarot.Future_Up);
                Assert.AreEqual(expectTarot.advice_up, dummyTarot.Advice_Up);

                Assert.AreEqual(expectTarot.love_up, dummyTarot.Love_Up);
                Assert.AreEqual(expectTarot.work_up, dummyTarot.Work_Up);
                Assert.AreEqual(expectTarot.interpersonal_up, dummyTarot.Interpersonal_Up);
                Assert.AreEqual(expectTarot.other_up, dummyTarot.Other_Up);
            }
        }

        [Test]
        public void EveryExtractTarot_SameAsDataStoreInDatabase_Reversed()
        {
            finder = new TarotCardCreator();
            TarotDatabaseObject =
                (TarotCardDatabaseObject) AssetDatabase.LoadAssetAtPath(DATABASE_PATH, typeof(TarotCardDatabaseObject));

            var direction = CardDirection.Reversed;
            var position = Vector3.zero;

            for (int cardIndex = 0; cardIndex < tarotNameList.Count; cardIndex++)
            {
                var dummyTarot = finder.FindTarotCardByNameAndDirection(tarotNameList[cardIndex], direction, position);
                var expectTarot = TarotDatabaseObject.TarotCard(index: cardIndex);

                Assert.AreEqual(expectTarot.cardName, dummyTarot.Name);
                Assert.AreEqual(expectTarot.cardEngName, dummyTarot.EnglishName);

                Assert.AreEqual(expectTarot.curSituation_re, dummyTarot.CurrentSituationSituation_Re);
                Assert.AreEqual(expectTarot.feelings_re, dummyTarot.HumanFeelings_Re);
                Assert.AreEqual(expectTarot.cause_re, dummyTarot.ProblemCause_Re);
                Assert.AreEqual(expectTarot.future_re, dummyTarot.Future_Re);
                Assert.AreEqual(expectTarot.advice_re, dummyTarot.Advice_Re);

                Assert.AreEqual(expectTarot.love_re, dummyTarot.Love_Re);
                Assert.AreEqual(expectTarot.work_re, dummyTarot.Work_Re);
                Assert.AreEqual(expectTarot.interpersonal_re, dummyTarot.Interpersonal_Re);
                Assert.AreEqual(expectTarot.other_re, dummyTarot.Other_Re);
            }
        }
    }
}
