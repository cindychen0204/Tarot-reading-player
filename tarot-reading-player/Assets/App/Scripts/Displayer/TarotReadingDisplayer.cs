using TarotReadingPlayer.Information.Reader;
using UnityEngine;
using UnityEngine.UI;

namespace TarotReadingPlayer.Information.Displayer
{
    public class TarotReadingDisplayer : MonoBehaviour
    {
        [SerializeField] private Text message;
        [SerializeField] private Text NesscaryNumberText;
        [SerializeField] private Text ReadingNumberText;
        [SerializeField] private Image ProgressRail;
        [SerializeField] private Animator ResultMenu;
        [SerializeField] private TarotSpreadReader reader;

        void Awake()
        {
            reader.OnNecessaryCardNumberChange += OnCardNumberChange;
            reader.OnReadingCardNumberChange += OnCardNumberChange;
        }

        void OnCardNumberChange()
        {
            var readingCardNumber = reader.ReadingCardNumber;
            var necessaryCardNumber = reader.NecessaryCardNumber;
            NesscaryNumberText.text = "/" + necessaryCardNumber.ToString();
            ReadingNumberText.text = readingCardNumber.ToString();

            if (necessaryCardNumber == 0 || readingCardNumber == 0)
            {
                ProgressRail.gameObject.transform.parent.parent.gameObject.SetActive(false);
                return;
            }
            ProgressRail.gameObject.transform.parent.parent.gameObject.SetActive(true);
            ProgressRail.fillAmount = (float) readingCardNumber / necessaryCardNumber;
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