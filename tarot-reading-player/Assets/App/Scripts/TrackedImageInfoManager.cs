﻿using System;
using System.Collections.Generic;
using TarotReadingPlayer.Information;
using TarotReadingPlayer.Information.Reader;
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

    //スマートフォンのカメラ
    public Camera WorldSpaceCamera
    {
        get { return worldSpaceCamera; }
        set { worldSpaceCamera = value; }
    }

    [SerializeField]
    private Text text;

    [SerializeField]
    private ARTrackedImageManager trackedImageManager;

    [SerializeField]
    private SpreadReader SpreadReader;

    private FindCardInTarotDatabase Finder;

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        Finder = new TarotCardFinder();
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    //新しいカードが検出されるときに呼び出される
    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        //ARTrackedImagesChangedEventArgs.added →新しく追加されるカードのリスト
        foreach (var trackedImage in eventArgs.added)
        {
            Debug.Log("new card found : " + trackedImage.referenceImage.name);
            UpdateInfo(trackedImage);
        }
    }

    void UpdateInfo(ARTrackedImage trackedImage)
    {
        var cardName = trackedImage.referenceImage.name;
        var direction = DetectUprightAndReversed(trackedImage);
        if(text != null)
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

        var tarotCard = Finder.FindTarotCardByNameAndDirection(cardName, direction);

        SendCardInformationToSpreadReader(tarotCard);
    }

    void SendCardInformationToSpreadReader(Tarot tarot)
    {
        SpreadReader.ReadCard(tarot);
    }


    CardDirection DetectUprightAndReversed(ARTrackedImage trackedImage)
    {
        CardDirection cardDirection = CardDirection.Default;
        var rotationY = RelativeRotation(WorldSpaceCamera.transform.rotation.eulerAngles, trackedImage.transform.rotation.eulerAngles).y;

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