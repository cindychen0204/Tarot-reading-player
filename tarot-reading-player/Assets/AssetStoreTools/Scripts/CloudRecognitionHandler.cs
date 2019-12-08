using System;
using UnityEngine;
using Vuforia;

namespace TarotReadingPlayer.Detection
{
    public class CloudRecognitionHandler : MonoBehaviour, IObjectRecoEventHandler
    {
        private CloudRecoBehaviour cloudRecoBehaviour;
        private bool isScanning = false;
        private string targetMetadata = "";

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
                // clear all known trackables
                var tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
                tracker.GetTargetFinder<ImageTargetFinder>().ClearTrackables(false);
            }
        }

        // Here we handle a cloud target recognition event
        public void OnNewSearchResult(TargetFinder.TargetSearchResult targetSearchResult)
        {
            TargetFinder.CloudRecoSearchResult cloudRecoSearchResult =
                (TargetFinder.CloudRecoSearchResult)targetSearchResult;
            // do something with the target metadata
            targetMetadata = cloudRecoSearchResult.MetaData;
            // stop the target finder (i.e. stop scanning the cloud)
            cloudRecoBehaviour.CloudRecoEnabled = false;
        }

        void OnGUI()
        {
            // Display current 'scanning' status
            GUI.Box(new Rect(100, 100, 200, 50), isScanning ? "Scanning" : "Not scanning");
            // Display metadata of latest detected cloud-target
            GUI.Box(new Rect(100, 200, 200, 50), "Metadata: " + targetMetadata);
            // If not scanning, show button
            // so that user can restart cloud scanning
            if (!isScanning)
            {
                if (GUI.Button(new Rect(100, 300, 200, 50), "Restart Scanning"))
                {
                    // Restart TargetFinder
                    cloudRecoBehaviour.CloudRecoEnabled = true;
                }
            }
        }

        // Use this for initialization 
        void Start()
        {
            // register this event handler at the cloud reco behaviour 
            cloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();

            if (cloudRecoBehaviour)
            {
                cloudRecoBehaviour.RegisterEventHandler(this);
            }
        }
    }
}
