using TarotReadingPlayer.Information.Reader;
using UnityEngine;
using UnityEngine.UI;

namespace TarotReadingPlayer.Information.Displayer
{
    public class TarotReadingDisplayer : MonoBehaviour
    {
        [SerializeField] private Text message;

        [SerializeField] private Text NesscaryCardNumber;

        [SerializeField] private Text CurrentCardNumber;
        [SerializeField] private Image ProgressRail;
        [SerializeField] private Animator ResultMenu;


        [SerializeField] private TarotSpreadReader reader;
        
        private int ReadingCardNumber = 0;

        private int NecessaryCardNumber = 0;

        void Update()
        {
            ReadingCardNumber = reader.ReadingCardNumber;
            NecessaryCardNumber = reader.NecessaryCardNumber;
            CurrentCardNumber.text = ReadingCardNumber.ToString();
            NesscaryCardNumber.text = NecessaryCardNumber.ToString();

            if(ReadingCardNumber == 0 || NecessaryCardNumber == 0)
            {
                ProgressRail.gameObject.transform.parent.parent.gameObject.SetActive(false);
                return;
            }
            ProgressRail.gameObject.transform.parent.parent.gameObject.SetActive(true);
            ProgressRail.fillAmount = (float)ReadingCardNumber/NecessaryCardNumber;
        }
        public void ShowMessage(string msg)
        {
            message.text = msg;
        }

        public void DisplayResult()
        {
            PopInAnimation();
        }

        public void HideResult()
        {
            PopUpAnimation();
        }

        private void PopUpAnimation()
        {
            ResultMenu.SetTrigger("PopUp");
        }

        private void PopInAnimation()
        {
            ResultMenu.SetTrigger("PopIn");
        }
    }
}