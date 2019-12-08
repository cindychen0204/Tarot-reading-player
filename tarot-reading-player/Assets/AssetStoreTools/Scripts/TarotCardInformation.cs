using System;

namespace TarotReadingPlayer.Detection
{
    [Serializable]
    public class TarotCardInformation
    {
        public string cardName;
        public int number;
        public string keyword;

        //explanation
        public string curSituation_up;
        public string curSituation_re;
        public string feelings_up;
        public string feelings_re;
        public string cause_up;
        public string cause_re;
        public string future_up;
        public string future_re;
        public string advice_up;
        public string advice_re;

        //practical use
        public string love_up;
        public string love_re;
        public string work_up;
        public string work_re;
        public string interpersonal_up;
        public string interpersonal_re;
        public string other_up;
        public string other_re;


        public TarotCardInformation(string name, int nbr, string ky, string cur_u, string cur_r, string hum_u, string hum_r, string prob_u, string prob_r, string fu_u, string fu_r, string adv_u, string adv_r, string lv_u, string lv_r, string wk_u, string wk_r, string int_u, string int_r, string oth_u, string oth_r)
        {
            cardName = name;
            number = nbr;
            keyword = ky;
            curSituation_up = cur_u;
            curSituation_re = cur_r;
            feelings_up = hum_u;
            feelings_re = hum_r;
            cause_up = prob_u;
            cause_re = prob_r;
            future_up = fu_u;
            future_re = fu_r;
            advice_up = adv_u;
            advice_re = adv_r;

            love_up = lv_u;
            love_re = lv_r;
            work_up = wk_u;
            work_re = wk_r;
            interpersonal_up = int_u;
            interpersonal_re = int_r;
            other_up = oth_u;
            other_re = oth_r;
        }
    }

    [Serializable]
    public class TarotInformations
    {
        public TarotCardInformation[] TarotCard;
    }
}