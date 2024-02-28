using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using TMPro;


public class LoadBackend : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TMP_Text txt;
    [SerializeField] int index;
    [SerializeField] TMP_InputField inputFieldImage;
    [SerializeField] TMP_InputField inputFieldTxt;
    string url_image;
    string url_text;
    int n;

    private void Start()
    {
        url_image = "https://webhistorim.000webhostapp.com/image1.png";
        url_text = "https://webhistorim.000webhostapp.com/txt1.txt";
        inputFieldImage.text = url_image;
        inputFieldTxt.text = url_text;
        StartCoroutine("LoadImage");
        StartCoroutine("LoadTexto");
    }
    public void Click()
    {
        n++;
        index = n %= 3;
        switch (index)
        {
            case 0:
                url_image = "https://webhistorim.000webhostapp.com/image1.png";
                url_text = "https://webhistorim.000webhostapp.com/txt1.txt";


                break;

            case 1:
                url_image = "https://webhistorim.000webhostapp.com/image2.png";
                url_text = "https://webhistorim.000webhostapp.com/txt2.txt";
                break;

            case 2:
                url_image = "https://webhistorim.000webhostapp.com/image3.png";
                url_text = "https://webhistorim.000webhostapp.com/txt3.txt";
                break;

        }
        StartCoroutine("LoadImage");
        StartCoroutine("LoadTexto");
    }
    public void MudarURLImagem()
    {
    
        url_image = inputFieldImage.text;
        StartCoroutine("LoadImage");
    }
     public void MudarURLTexto()
    {
        url_text= inputFieldTxt.text;
        StartCoroutine("LoadTexto");

    }
    IEnumerator LoadImage()
    {

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url_image);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Rect rect = new Rect(0, 0, texture.width, texture.height);
            Vector2 center = new Vector2(texture.width / 2, texture.height / 2);
            Sprite sprite = Sprite.Create(texture, rect, center);
            image.sprite = sprite;
            inputFieldImage.text = url_image;
        }
    }
    IEnumerator LoadTexto()
    {
    
        UnityWebRequest request = UnityWebRequest.Get(url_text);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            txt.text = request.downloadHandler.text;
             inputFieldTxt.text = url_text;

        }
    }
}
