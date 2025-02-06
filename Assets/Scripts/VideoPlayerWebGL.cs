using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;    

public class VideoPlayerWebGL : MonoBehaviour
{
    public string videoFileName; // Name of the video file in StreamingAssets
    public RawImage rawImage;    // RawImage to display the video
    public RenderTexture renderTexture; // Render Texture to render the video

    private VideoPlayer videoPlayer;

    void Start()
    {
        StartCoroutine(LoadVideo());
    }

    IEnumerator LoadVideo()
    {
        // Construct the path to the video file
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);

        // Create a UnityWebRequest to load the video
        UnityWebRequest request = UnityWebRequest.Get(videoPath);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("Error loading video: " + request.error);
             Debug.LogError("wtf" );
            yield break;
        }

        // Create a new VideoPlayer component
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        videoPlayer.targetTexture = renderTexture;
        videoPlayer.url = videoPath;
        videoPlayer.isLooping = true;

        // Assign the Render Texture to the Raw Image
        rawImage.texture = renderTexture;

        // Play the video
        videoPlayer.Play();
    }
}
