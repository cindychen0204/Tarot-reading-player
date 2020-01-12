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

        void Start()
        {
            reader.OnNecessaryCardNumberChange += OnCardNumberChange;
            reader.OnReadingCardNumberChange += OnCardNumberChange;
        }

        void OnCardNumberChange()
        {
            var ReadingCardNumber = reader.ReadingCardNumber;
            var NecessaryCardNumber = reader.NecessaryCardNumber;
            NesscaryNumberText.text = "/" + NecessaryCardNumber.ToString();
            ReadingNumberText.text = ReadingCardNumber.ToString();

            if (NecessaryCardNumber == 0 || ReadingCardNumber == 0)
            {
                ProgressRail.gameObject.transform.parent.parent.gameObject.SetActive(false);
                return;
            }
            ProgressRail.gameObject.transform.parent.parent.gameObject.SetActive(true);
            ProgressRail.fillAmount = (float)ReadingCardNumber / NecessaryCardNumber;
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