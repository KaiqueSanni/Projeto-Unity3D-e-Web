using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ColorReceiver : MonoBehaviour
{
    private const string serverURL = "http://localhost:3000";
    public Renderer cubeRenderer; 

    IEnumerator Start()
    {
        while (true)
        {
            Debug.Log("Chamando GetColor()...");
            yield return GetColor();
            Debug.Log("Cor obtida e aplicada ao cubo.");
            yield return new WaitForSeconds(1f); // Aguarda 1 segundo antes de obter a próxima cor
        }
    }

    IEnumerator GetColor()
{
    using (UnityWebRequest webRequest = UnityWebRequest.Get(serverURL))
    {
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || 
            webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Erro ao acessar o endpoint: " + webRequest.error);
        }
        else
        {
            string jsonResponse = webRequest.downloadHandler.text;
            ColorResponse colorResponse = JsonUtility.FromJson<ColorResponse>(jsonResponse);
            Color newColor = ParseColor(colorResponse.cor);
            cubeRenderer.material.color = newColor;
           
        }
    }
}


    Color ParseColor(string colorName)
    {
        switch (colorName.ToLower())
        {
            case "red":
                return new Color(255, 0, 0);
            case "blue":
                return new Color(0, 0, 255);
            case "green":
                return new Color(0, 255, 0);
            case "yellow":
                return new Color(255, 255, 0);
            case "black":
                return new Color(0, 0, 0);
            case "Magenta":
                return new Color(255, 0, 255);
            case "cyan":
                return new Color(0, 255, 255);

            default:
               return Color.white;
        }
    }
}

[System.Serializable]
public class ColorResponse
{
    public string cor;
}
