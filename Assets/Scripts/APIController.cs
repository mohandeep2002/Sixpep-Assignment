using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIController : MonoBehaviour
{
    public void PostRequestSender(string apiURL, string requestBody, string authorizationToken)
    {
        StartCoroutine(DelayForPostRequest(apiURL, requestBody, authorizationToken));
    }

    IEnumerator DelayForPostRequest(string apiURL, string requestBody, string authorizationToken)
    {
        using (UnityWebRequest request = new UnityWebRequest(apiURL, "POST"))
        {
            byte[] bodyRAW = System.Text.Encoding.UTF8.GetBytes(requestBody);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRAW);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", authorizationToken);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError) Debug.Log("Error: " + request.error);
            else Debug.Log("Response: " + request.downloadHandler.text);
        }
    }
}
