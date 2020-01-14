using TarotReadingPlayer.Information.Reader;
using UnityEngine;
using UnityEngine.UI;

namespace TarotReadingPlayer.Information.Displayer
{
    public class TarotReadingDisplayer : MonoBehaviour
    {
        [SerializeField] private Text message;
        [SerializeField] private Text nesscaryNumberText;
        [SerializeField] private Text readingNumberText;
        [SerializeField] private Image fllArea;
        [SerializeField] private GameObject progressRailParent;
        [SerializeField] private Animator resultPanelAnimator;
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
            nesscaryNumberText.text = "/" + necessaryCardNumber.ToString();
            readingNumberText.text = readingCardNumber.ToString();

            if (necessaryCardNumber == 0 || readingCardNumber == 0)
            {
                progressRailParent.SetActive(false);
                return;
            }
            progressRailParent.SetActive(true);
            fllArea.fillAmount = (float) readingCardNumber / necessaryCardNumber;
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
            resultPanelAnimator.SetTrigger("PopUp");
        }

        private void PopInAnimation()
        {
            resultPanelAnimator.SetTrigger("PopIn");
        }
    }
}