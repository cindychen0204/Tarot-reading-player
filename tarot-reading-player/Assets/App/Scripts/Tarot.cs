namespace TarotReadingPlayer.Information
{
    public class Tarot
    {
        public string CardName = "";
        public CardDirection Direction;
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
            
        }

        public Tarot(string cardName)
        {
            CardName = cardName;
        }

        public Tarot(string cardName, CardDirection direction, string keyword)
        {
            CardName = cardName;
            Direction = direction;
            Keyword = keyword;
        }

        public Tarot(string cardName, string keyword, CardDirection direction, string currentSituation, string humanFeeling,
            string problemCause, string future, string advice, string love, string work, string interpersonal, string other)
        {
            CardName = cardName;
            Direction = direction;
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
            CardName = name;
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