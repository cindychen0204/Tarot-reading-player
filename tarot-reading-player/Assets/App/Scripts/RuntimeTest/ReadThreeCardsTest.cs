using UnityEngine;
using TarotReadingPlayer.Information.Reader;

[RequireComponent(typeof(TarotSpreadReader))]
public class ReadThreeCardsTest : MonoBehaviour
{
    private TarotSpreadReader reader;
    private TarotCardFinder finder;

    void Start()
    {
        reader = this.gameObject.GetComponent<TarotSpreadReader>();
        reader.SetSpread(TarotSpreads.ThreeCards);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))    
        {    
            var leftcard = new TarotCard("愚者","the_fool","自由",CardDirection.Reversed, new Vector3(-1, 0 ,0));
            reader.AddDetectCard(leftcard);
        }
        if(Input.GetKey(KeyCode.B)) 
        {           
            var centercard = new TarotCard("女帝","the_empress","愛",CardDirection.Upright, new Vector3(0, 0 ,0));
            reader.AddDetectCard(centercard);
        }
        if(Input.GetKey(KeyCode.C))
        {
            var rightcard = new TarotCard("皇帝","the_emperor","社会",CardDirection.Reversed, new Vector3(1, 0 ,0));
            reader.AddDetectCard(rightcard);
        }
    }
}
