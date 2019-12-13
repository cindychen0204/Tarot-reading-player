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

        [SerializeField]
        Transform ARCamera;

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
            GUIStyle guiStyle = new GUIStyle(GUI.skin.box);
            guiStyle.fontSize = 30;
            // Display current 'scanning' status
            GUI.Box(new Rect(100, 100, 400, 100), isScanning ? "Scanning" : "Not scanning", guiStyle);
            // Display metadata of latest detected cloud-target
            GUI.Box(new Rect(100, 200, 400, 200), "Metadata: " + targetMetadata, guiStyle);

            if (currentTrackingTransform!= null)
            {
               
                string situation = "";
                var rotationX = ARCamera.rotation.eulerAngles.x - currentTrackingTransform.rotation.eulerAngles.x;
                var rotationY = ARCamera.rotation.eulerAngles.y - currentTrackingTransform.rotation.eulerAngles.y;
                var rotationZ = ARCamera.rotation.eulerAngles.z - currentTrackingTransform.rotation.eulerAngles.z;
                if(Mathf.Abs(rotationY) < 90)
                {
                    situation = "正位";
                }
                else if(Mathf.Abs(180 - Mathf.Abs(rotationY)) < 90)
                {
                    situation = "逆位";
                }
                else
                {
                    situation = "カードを正しい方向に配置してください, Y: " + Mathf.Abs(rotationY);
                }

                GUI.Box(new Rect(100, 400, 400, 200), "relative rotation \n  x:  " + rotationX +
                                                      "\n  y: " + rotationY + 
                                                      "\n  z: " + rotationZ, guiStyle);
                GUI.Box(new Rect(100, 700, 400, 200), "Situation : ¥n" + situation, guiStyle );
            }
        }
    }
}