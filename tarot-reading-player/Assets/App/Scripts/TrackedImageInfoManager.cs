using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
/// This component listens for images detected by the <c>XRImageTrackingSubsystem</c>
/// and overlays some information as well as the source Texture2D on top of the
/// detected image.
/// </summary>
[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The camera to set on the world space UI canvas for each instantiated image info.")]
    Camera worldSpaceCamera;
    /// <summary>
    /// The prefab has a world space UI canvas,
    /// which requires a camera to function properly.
    /// </summary>
    public Camera WorldSpaceCamera
    {
        get { return worldSpaceCamera; }
        set { worldSpaceCamera = value; }
    }

    public Text text;
    ARTrackedImageManager trackedImageManager;

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void UpdateInfo(ARTrackedImage trackedImage)
    {

        text.text = string.Format("{0} \ntrackingState: {1}\nGUID: { 2}\nReference size: {3} cm\nDetected size: { 4}cm\n Direcction: { 5}",
        trackedImage.referenceImage.name,
        trackedImage.trackingState,
        trackedImage.referenceImage.guid,
        trackedImage.referenceImage.size * 100f,
        trackedImage.size * 100f,
        DetectUprightAndReversed(trackedImage));

    }

    string DetectUprightAndReversed(ARTrackedImage trackedImage)
    {
        string cardDirection = "";
        var rotationY = RelativeRoation(WorldSpaceCamera.transform, trackedImage.transform).y;

        if (Mathf.Abs(rotationY) < 90)
        {
            cardDirection = "正位";
        }
        else if (Mathf.Abs(180 - Mathf.Abs(rotationY)) < 90)
        {
            cardDirection = "逆位";
        }
        else
        {
            cardDirection = "カードを正しい向きに置いてください";
        }

        return cardDirection;
    }

    Vector3 RelativeRoation(Transform origin, Transform target)
    {
        return origin.rotation.eulerAngles - target.rotation.eulerAngles;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            // Give the initial image a reasonable default scale
            //trackedImage.transform.localScale = new Vector3(0.01f, 1f, 0.01f);
            UpdateInfo(trackedImage);
        }
    }
}