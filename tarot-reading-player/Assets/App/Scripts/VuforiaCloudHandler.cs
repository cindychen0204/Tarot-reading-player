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
        private string targetMetadata = "";
        private Transform currentTrackingTransform;

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
            var tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            tracker.GetTargetFinder<ImageTargetFinder>().ClearTrackables(false);
        }

        public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
        {
            GameObject newImageTarget = Instantiate(ImageTargetTemplate.gameObject) as GameObject;
            GameObject augmentation = null;
            if (augmentation)
            {
                augmentation.transform.parent = newImageTarget.transform;
                augmentation.name = targetMetadata;
            }
            var cloudRecoSearchResult = (TargetFinder.CloudRecoSearchResult) targetSearchResult;
            targetMetadata = cloudRecoSearchResult.MetaData;
            newImageTarget.name = targetMetadata;
            var tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            var imageTargetBehaviour = (ImageTargetBehaviour) tracker.GetTargetFinder<ImageTargetFinder>().EnableTracking(targetSearchResult, newImageTarget);
            currentTrackingTransform = newImageTarget.transform;

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
            GUI.Box(new Rect(100, 200, 200, 100), "Metadata: " + targetMetadata);

            if (currentTrackingTransform.localPosition != null)
            {
                GUI.Box(new Rect(100, 400, 200, 100), "Cube rotation \n  x:  " + currentTrackingTransform.rotation.x +
                                                      " y: " + currentTrackingTransform.rotation.y + 
                                                      " z: " + currentTrackingTransform.rotation.z + 
                                                      " w: " + currentTrackingTransform.rotation.w);
            }
            // If not scanning, show button
            // so that user can restart cloud scanning

            //// Restart TargetFinder
            //cloudRecoBehaviour.CloudRecoEnabled = true;
            //targetMetadata = "";

        }
    }
}