using UnityEngine;
using TarotReadingPlayer.Information.Reader;

[RequireComponent(typeof(TarotSpreadReader))]
public class ReadCardsSimulator : MonoBehaviour
{
    enum SimulatingOracle
    {
        OneOracle,
        ThreeCardOracle
    }

    private TarotSpreadReader reader;
    [SerializeField] private SimulatingOracle simulatingOracle;

    void Start()
    {
        reader = this.gameObject.GetComponent<TarotSpreadReader>();
        if (simulatingOracle == SimulatingOracle.ThreeCardOracle) reader.SetSpread(TarotSpreads.ThreeCards);
        else if (simulatingOracle == SimulatingOracle.OneOracle) reader.SetSpread(TarotSpreads.OneOracle);
    }

    // Update is called once per frame
    void Update()
    {
        if (simulatingOracle == SimulatingOracle.ThreeCardOracle)
        {

            if (Input.GetKey(KeyCode.A))
            {
                var leftCard = new TarotCard("愚者", "the_fool", "自由", CardDirection.Reversed, new Vector3(-1, 0, 0));
                reader.AddDetectCard(leftCard);
            }
            if (Input.GetKey(KeyCode.B))
            {
                var centerCard = new TarotCard("女帝", "the_empress", "愛", CardDirection.Upright, new Vector3(0, 0, 0));
                reader.AddDetectCard(centerCard);
            }
            if (Input.GetKey(KeyCode.C))
            {
                var rightCard = new TarotCard("皇帝", "the_emperor", "社会", CardDirection.Reversed, new Vector3(1, 0, 0));
                reader.AddDetectCard(rightCard);
            }
        }
        else if (simulatingOracle == SimulatingOracle.OneOracle)
        {
            if (Input.GetKey(KeyCode.A))
            {
                var card = new TarotCard("愚者", "the_fool", "自由", CardDirection.Reversed, new Vector3(-1, 0, 0));
                reader.AddDetectCard(card);
            }
        }
    }
}
