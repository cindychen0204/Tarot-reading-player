using UnityEngine;
using Vuforia;

namespace TarotReadingPlayer.Detection
{
    [RequireComponent(typeof(CloudRecoBehaviour))]
    public class VuforiaCloudHandler : MonoBehaviour, IObjectRecoEventHandler
    {
        public ImageTargetBehaviour ImageTargetTemplate;
        private CloudRecoBehaviour cloudRecoBehaviour;
        private bool isScanning = false;
        private string mTargetMetadata = "";

        void Start()
        {
            cloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();

            if (cloudRecoBehaviour)
            {
                cloudRecoBehaviour.RegisterEventHandler(this);
            }

        }

        public void OnInitialized(TargetFinder targetFinder)
        {
            Debug.Log("Cloud Reco initialized");
        }

        public void OnInitError(TargetFinder.InitState initError)
        {
            Debug.Log("Cloud Reco init error " + initError.ToString());
        }

        public void OnUpdateError(TargetFinder.UpdateState updateError)
        {
            Debug.Log("Cloud Reco update error " + updateError.ToString());
        }

        public void OnStateChanged(bool scanning)
        {
            isScanning = scanning;
            if (scanning)
            {
                ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
                tracker.GetTargetFinder<ImageTargetFinder>().ClearTrackables(false);
            }
        }

        public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
        {
            GameObject newImageTarget = Instantiate(ImageTargetTemplate.gameObject) as GameObject;
            GameObject augmentation = null;
            //mTargetMetadata = targetSearchResult.;

            if (augmentation != null)
            {
                augmentation.transform.SetParent(newImageTarget.transform);
            }

            if (ImageTargetTemplate)
            {
                ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
                ImageTargetBehaviour imageTargetBehaviour = (ImageTargetBehaviour)tracker.GetTargetFinder<ImageTargetFinder>().EnableTracking(targetSearchResult, newImageTarget);
            }

            if (!isScanning)
            {
                cloudRecoBehaviour.CloudRecoEnabled = true;
            }
        }

        void OnGUI()
        {
            // Display current 'scanning' status
            GUI.Box(new Rect(100, 100, 200, 50), isScanning ? "Scanning" : "Not scanning");
            // Display metadata of latest detected cloud-target
            GUI.Box(new Rect(100, 200, 800, 400), "Metadata: " + mTargetMetadata);
            // If not scanning, show button
            // so that user can restart cloud scanning
            if (!isScanning)
            {
                if (GUI.Button(new Rect(100, 300, 200, 50), "Restart Scanning"))
                {
                    // Restart TargetFinder
                    cloudRecoBehaviour.CloudRecoEnabled = true;
                    mTargetMetadata = "";
                }
            }
        }
    }
}