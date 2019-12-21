using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace TarotReadingPlayer.Information.Reader
{
    //Input
    public enum CardDirection
    {
        Default,
        Upright,
        Reversed
    }

    /// <summary>
    /// This component listens for images detected by the <c>XRImageTrackingSubsystem</c>
    /// and overlays some information as well as the source Texture2D on top of the
    /// detected image.
    /// </summary>>
    [RequireComponent(typeof(ARTrackedImageManager))]
    public class TrackedImageInfoManager : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The camera to set on the world space UI canvas for each instantiated image information.")]
        private Camera worldSpaceCamera;

        //スマートフォンのカメラ
        public Camera WorldSpaceCamera
        {
            get { return worldSpaceCamera; }
            set { worldSpaceCamera = value; }
        }

        [SerializeField] private Text text;

        [SerializeField] private ARTrackedImageManager trackedImageManager;

        [SerializeField] private SpreadReader SpreadReader;

        private IFindCardInTarotDatabase Finder;

        void Awake()
        {
            trackedImageManager = GetComponent<ARTrackedImageManager>();
            Finder = new TarotCardCreator();
        }

        void OnEnable()
        {
            trackedImageManager.trackedImagesChanged += OnNewTrackedImagesAdded;
        }

        void OnDisable()
        {
            trackedImageManager.trackedImagesChanged -= OnNewTrackedImagesAdded;
        }

        //新しいカードが検出されるときに呼び出される
        void OnNewTrackedImagesAdded(ARTrackedImagesChangedEventArgs eventArgs)
        {
            //ARTrackedImagesChangedEventArgs.added →新しく追加されるカードのリスト
            foreach (var trackedImage in eventArgs.added)
            {
                var cardName = trackedImage.referenceImage.name;
                var direction = DetectUprightAndReversed(trackedImage);
                ShowText(trackedImage, cardName, direction);
                //TODO : スプレッドによりFinderを呼び出す方法も異なるはず
                var tarotCard =
                    Finder.FindTarotCardByNameAndDirection(cardName, direction, trackedImage.transform.position);
                SendCardInformationToSpreadReader(tarotCard);
            }
        }

        /// <summary>
        /// UIにタロットの情報を呈示する
        /// </summary>
        /// <param name="trackedImage"></param>
        /// <param name="cardName"></param>
        /// <param name="direction"></param>
        void ShowText(ARTrackedImage trackedImage, string cardName, CardDirection direction)
        {
            if (text != null)
            {
                text.text = string.Format(
                    "{0}\ntrackingState: {1}\nGUID: {2}\nReference size: {3} cm\nDetected size: {4} cm\nDirection: {5}",
                    cardName,
                    trackedImage.trackingState,
                    trackedImage.referenceImage.guid,
                    trackedImage.referenceImage.size * 100f,
                    trackedImage.size * 100f,
                    direction);
            }
        }

        /// <summary>
        /// Readerに情報を送り、スプレッドの判断をさせる
        /// </summary>
        /// <param name="tarot">Finderで取得したタロット情報</param>
        void SendCardInformationToSpreadReader(Tarot tarot)
        {
            SpreadReader.ReadCard(tarot);
        }

        /// <summary>
        /// カードの向きを取得
        /// </summary>
        /// <param name="trackedImage"></param>
        /// <returns></returns>
        CardDirection DetectUprightAndReversed(ARTrackedImage trackedImage)
        {
            CardDirection cardDirection = CardDirection.Default;
            var rotationY = RelativeRotation(WorldSpaceCamera.transform.rotation.eulerAngles,
                trackedImage.transform.rotation.eulerAngles).y;

            if (Mathf.Abs(rotationY) < 90)
            {
                cardDirection = CardDirection.Upright;
            }
            else if (Mathf.Abs(180 - Mathf.Abs(rotationY)) < 90)
            {
                cardDirection = CardDirection.Reversed;
            }
            else
            {
                cardDirection = CardDirection.Default;
            }

            return cardDirection;
        }

        Vector3 RelativeRotation(Vector3 origin, Vector3 target)
        {
            return origin - target;
        }
    }
}
