using UnityEngine;
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

        [SerializeField] private ARTrackedImageManager trackingManager;

        [SerializeField] private TarotSpreadReader reader;

        private IFindCardInformation finder;

        private void Start()
        {
            trackingManager = GetComponent<ARTrackedImageManager>();
            finder = new TarotCardFinder();
            finder.ObtainDatabase();
        }

        private void OnEnable()
        {
            trackingManager.trackedImagesChanged += OnNewTrackingResultAdded;
        }

        private void OnDisable()
        {
            trackingManager.trackedImagesChanged -= OnNewTrackingResultAdded;
        }

        //新しいカードが検出されるときに呼び出される
        private void OnNewTrackingResultAdded(ARTrackedImagesChangedEventArgs eventArgs)
        {
            //更新される画像リスト
            foreach (var trackedImage in eventArgs.added)
            {
                var cardName = trackedImage.referenceImage.name;
                var direction = DetectCardDirection(trackedImage);
                var tarotCard =
                    finder.FindTarotCardByNameAndDirection(cardName, direction, trackedImage.transform.position);
                reader.AddDetectCard(tarotCard);
            }
        }

        /// <summary>
        /// カードの向きを取得
        /// </summary>
        /// <param name="trackedImage"></param>
        /// <returns></returns>
        private CardDirection DetectCardDirection(ARTrackedImage trackedImage)
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

        private Vector3 RelativeRotation(Vector3 origin, Vector3 target)
        {
            return origin - target;
        }
    }
}
