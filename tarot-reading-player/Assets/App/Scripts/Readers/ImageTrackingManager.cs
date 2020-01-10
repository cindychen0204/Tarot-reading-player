using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace TarotReadingPlayer.Information.Reader
{
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
    public class ImageTrackingManager : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The camera to set on the world space UI canvas for each instantiated image information.")]
        private Camera aRCamera;

        //スマートフォンのカメラ
        public Camera ARCamera
        {
            get => aRCamera;
            set => aRCamera = value;
        }

        [SerializeField] private Text text;

        [SerializeField] private ARTrackedImageManager trackingManager;

        [SerializeField] private TarotSpreadReader reader;

        private IFindCardInformation finder;

        void Start()
        {
            trackingManager = GetComponent<ARTrackedImageManager>();
            finder = new TarotCardFinder();
            finder.ObtainDatabase();
        }

        void OnEnable()
        {
            trackingManager.trackedImagesChanged += OnNewTrackingResultAdded;
        }

        void OnDisable()
        {
            trackingManager.trackedImagesChanged -= OnNewTrackingResultAdded;
        }

        //新しいカードが検出されるときに呼び出される
        void OnNewTrackingResultAdded(ARTrackedImagesChangedEventArgs eventArgs)
        {
            //ARTrackedImagesChangedEventArgs.added →新しく追加されるカードのリスト
            foreach (var trackedImage in eventArgs.added)
            {
                var cardName = trackedImage.referenceImage.name;
                var direction = DetectUprightAndReversed(trackedImage);
                //ShowText(trackedImage, cardName, direction);
                var tarotCard =
                    finder.FindTarotCardByNameAndDirection(cardName, direction, trackedImage.transform.position);
                reader.AddDetectCard(tarotCard);
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
        /// カードの向きを取得
        /// </summary>
        /// <param name="trackedImage"></param>
        /// <returns></returns>
        CardDirection DetectUprightAndReversed(ARTrackedImage trackedImage)
        {
            var cardDirection = CardDirection.Default;
            var rotationY = RelativeRotation(ARCamera.transform.rotation.eulerAngles,
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
