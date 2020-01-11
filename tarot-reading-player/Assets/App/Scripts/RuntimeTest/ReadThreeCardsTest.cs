using UnityEngine;
using TarotReadingPlayer.Information.Reader;

public class ReadThreeCardsTest : MonoBehaviour
{
    public TarotSpreadReader reader;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            reader.SetSpread(TarotSpreads.ThreeCards);
            var leftcard = new TarotCard("愚者","the_fool","自由",CardDirection.Reversed, new Vector3(-1, 0 ,0));
            var centercard = new TarotCard("女帝","the_empress","愛",CardDirection.Upright, new Vector3(0, 0 ,0));
            var rightcard = new TarotCard("皇帝","the_emperor","社会",CardDirection.Reversed, new Vector3(1, 0 ,0));
            reader.AddDetectCard(leftcard);
            reader.AddDetectCard(centercard);
            reader.AddDetectCard(rightcard);
        }
    }
}
