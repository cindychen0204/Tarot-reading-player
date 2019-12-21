using System.Numerics;

namespace TarotReadingPlayer.Information
{
    public class Tarot
    {
        public string Name = "";
        public string EnglishName = "";
        public CardDirection Direction;
        public Vector3 Position;
        public int Number;
        public string Keyword;

        //explanation
        public string CurrentSituationSituation_Up;
        public string CurrentSituationSituation_Re;
        public string HumanFeelings_Up;
        public string HumanFeelings_Re;
        public string ProblemCause_Up;
        public string ProblemCause_Re;
        public string Future_Up;
        public string Future_Re;
        public string Advice_Up;
        public string Advice_Re;

        //practical use
        public string Love_Up;
        public string Love_Re;
        public string Work_Up;
        public string Work_Re;
        public string Interpersonal_Up;
        public string Interpersonal_Re;
        public string Other_Up;
        public string Other_Re;

        public Tarot()
        {
            //Do nothing
        }

        public Tarot(string name, string englishName)
        {
            Name = name;
            EnglishName = englishName;
        }

        public Tarot(string name, string engName, string keyword, CardDirection direction,  Vector3 position)
        {
            Name = name;
            EnglishName = engName;
            Direction = direction;
            Position = position;
            Keyword = keyword;
        }

        /// <summary>
        /// スプレッド認識用
        /// </summary>
        /// <param name="name">タロットの名前</param>
        /// <param name="engName">英語の名前（データベース検索用）</param>
        /// <param name="keyword">タロットのキーワード</param>
        /// <param name="direction">タロットの向き（正位と逆位）</param>
        /// <param name="position">タロットの位置（スプレッド認識用）</param>
        /// <param name="currentSituation">現在の状況</param>
        /// <param name="humanFeeling">人の気もち</param>
        /// <param name="problemCause">問題の原因</param>
        /// <param name="future">未来の行末</param>
        /// <param name="advice">アドバイス</param>
        /// <param name="love">恋愛</param>
        /// <param name="work">仕事</param>
        /// <param name="interpersonal">対人関係</param>
        /// <param name="other">その他</param>
        public Tarot(string name, string engName,  string keyword, CardDirection direction, Vector3 position, string currentSituation, string humanFeeling,
            string problemCause, string future, string advice, string love, string work, string interpersonal, string other)
        {
            Name = name;
            EnglishName = engName;
            Direction = direction;
            Position = position;
            Keyword = keyword;

            switch (direction)
            {
                case CardDirection.Default:
                    break;
                case CardDirection.Upright:
                    CurrentSituationSituation_Up = currentSituation;
                    HumanFeelings_Up = humanFeeling;
                    ProblemCause_Up = problemCause;
                    Future_Up = future;
                    Advice_Up = advice;
                    Love_Up = love;
                    Work_Up = work;
                    Interpersonal_Up = interpersonal;
                    Other_Up = other;
                    break;
                case CardDirection.Reversed:
                    CurrentSituationSituation_Re = currentSituation;
                    HumanFeelings_Re = humanFeeling;
                    ProblemCause_Re = problemCause;
                    Future_Re = future;
                    Advice_Re = advice;
                    Love_Re = love;
                    Work_Re = work;
                    Interpersonal_Re = interpersonal;
                    Other_Re = other;
                    break;
            }
        }

        public Tarot(string name,
            string engName,
            int nbr,
            string ky,
            string currentU,
            string currentR,
            string hum_u,
            string hum_r,
            string prob_u,
            string prob_r,
            string fu_u,
            string fu_r,
            string adv_u,
            string adv_r,
            string lv_u,
            string lv_r,
            string wk_u,
            string wk_r,
            string int_u,
            string int_r,
            string oth_u,
            string oth_r)
        {
            Name = name;
            EnglishName = engName;
            Number = nbr;
            Keyword = ky;
            CurrentSituationSituation_Up = currentU;
            CurrentSituationSituation_Re = currentR;
            HumanFeelings_Up = hum_u;
            HumanFeelings_Re = hum_r;
            ProblemCause_Up = prob_u;
            ProblemCause_Re = prob_r;
            Future_Up = fu_u;
            Future_Re = fu_r;
            Advice_Up = adv_u;
            Advice_Re = adv_r;
            Love_Up = lv_u;
            Love_Re = lv_r;
            Work_Up = wk_u;
            Work_Re = wk_r;
            Interpersonal_Up = int_u;
            Interpersonal_Re = int_r;
            Other_Up = oth_u;
            Other_Re = oth_r;
        }

    }
}