using UnityEngine;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using EaAds;
using UnityEngine.Networking;

public class Utils {

    public static async Task<JSONData> GetJsonDataFromServer(string url, Action<DataProgress> progresss)
    {
        var dataProgress = new DataProgress();
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            // Send request and wait
            www.SendWebRequest();

            // While the request is still processing...
            while (!www.isDone)
            {
                var progresssPersen = string.Format("{0:0}%", www.downloadProgress * 100);

                dataProgress.progressPersen = progresssPersen;
                dataProgress.progressFloat = www.downloadProgress;

                progresss.Invoke(dataProgress);
                await Task.Delay(100);  // Tunggu sedikit sebelum memeriksa lagi.
            }

            // If there are network errors
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                return null;
            }
            else
            {
                // Parse JSON response
                var result = JsonConvert.DeserializeObject<JSONData>(www.downloadHandler.text);
                dataProgress.progressPersen = "100%";
                dataProgress.progressFloat = www.downloadProgress;

                progresss.Invoke(dataProgress);
                await Task.Delay(300);  // Tunggu sedikit sebelum memeriksa lagi.
                return result;
            }
        }
    }
    public static JSONData GetJsonDataFromLocal()
    {
        TextAsset jsonTextAsset = Resources.Load<TextAsset>("JsonData");
        // Parse JSON response
        return JsonConvert.DeserializeObject<JSONData>(jsonTextAsset.text);
    }

   public static System.Collections.IEnumerator LoadSpriteCoroutine(string url, Action<Sprite> callback)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                callback(sprite);
            }
            else
            {
                Debug.LogError("Failed to load texture from URL");
                callback(null);
            }
        }
    }

    public static Sprite LoadImageFromResources(string pathImage)
    {
        // Resources.Load berfungsi relatif terhadap direktori Resources,
        // jadi Anda hanya perlu menyertakan bagian "Images/imag001" dari jalur
        Texture2D texture = Resources.Load<Texture2D>(pathImage);

        if(texture == null)
        {
            Debug.LogError("Image not found at specified path.");
            return null;
        }

        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        return sprite;
    }
}