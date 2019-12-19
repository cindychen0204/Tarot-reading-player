using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace TarotReadingPlayer.Information.Reader
{
}

namespace TarotReadingPlayer.Information.Editor
{
    public class TarotCardDatabase : ScriptableObject
    {
        [SerializeField] public List<TarotCardInformation> database;

        void OnEnable()
        {
            if (database == null)
            {
                database = new List<TarotCardInformation>();
            }
        }

        public void Add(TarotCardInformation tarot)
        {
            database.Add(tarot);
        }

        public void Remove(TarotCardInformation tarot)
        {
            database.Remove(tarot);
        }

        public int count
        {
            get { return database.Count; }
        }

        public TarotCardInformation TarotCard(int index)
        {
            return database.ElementAt(index);
        }

        public void SortTarotNumber()
        {
            database = database.OrderBy(x => x.number).ToList();
        }
    }
}