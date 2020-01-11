using UnityEngine;
using UnityEngine.UI;

namespace TarotReadingPlayer.Information.Displayer
{
    public class TarotReadingDisplayer : MonoBehaviour
    {
        [SerializeField] private Text message;
        public void ShowMessage(string msg)
        {
            message.text = msg;
        }
    }
}