using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SpriteLoader : MonoBehaviour
{
    public string imageUrl = "https://scontent.fjsr6-1.fna.fbcdn.net/v/t39.30808-6/346635002_611767850898506_6342046067913210385_n.jpg?stp=cp6_dst-jpg&_nc_cat=102&cb=99be929b-59f725be&ccb=1-7&_nc_sid=09cbfe&_nc_eui2=AeGVO8vJ3qU45p_nRKF_kEVu8qgzL-hJmN_yqDMv6EmY33w8-064SIvdIBMBl8oXB0d19ahVawLILIBtrkmnSmhS&_nc_ohc=jOwarCkZLZkAX9PYgvw&_nc_ht=scontent.fjsr6-1.fna&oh=00_AfDrYTm87jtRxbeF7TzYIK-o7NtnEg6ITAGQtTp1Ul2eaA&oe=649D7353";
    public Image image;  // Reference to the Image component in the UI

    void Start()
    {
        StartCoroutine(LoadSprite());
    }

    IEnumerator LoadSprite()
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to download image: " + www.error);
                yield break;
            }

            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            DisplaySprite(sprite);
        }
    }

    void DisplaySprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
