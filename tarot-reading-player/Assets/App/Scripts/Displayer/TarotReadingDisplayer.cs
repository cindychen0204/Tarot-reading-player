using UnityEngine;
using UnityEngine.UI;

namespace TarotReadingPlayer.Information.Displayer
{
    public class TarotReadingDisplayer : MonoBehaviour
    {
        [SerializeField] private Text message;
        [SerializeField] private Animator ResultMenu;
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