using UnityEngine;
using System.Collections;
using System.IO;

public class RenderTexture : MonoBehaviour
{
	//public static string TEXTURE_PATH = "C:/Users/samir/Documents/GodsGame/Sheets/";
	public static string TEXTURE_PATH = "C:/Users/Public/Documents/Unity Projects/GodsGame/Sheets/";
	public static string TEXTURE_EXT = ".png";

	public GameObject m_Paper;
	public string m_TextureName;

	private int oldAntiAliasingSettings;
	private Texture2D m_RenderTexture;
	private int screenshotCounter = 2;

	// Use this for initialization
	void Start()
	{
		//oldAntiAliasingSettings = QualitySettings.antiAliasing;
		//QualitySettings.antiAliasing = 0;
	}
	
	// Update is called once per frame
	void Update()
	{
		--screenshotCounter;
	}

	void OnPostRender()
	{
		if (screenshotCounter == 0)
		{
			m_RenderTexture = new Texture2D(Mathf.RoundToInt(Screen.width), Mathf.RoundToInt(Screen.height), TextureFormat.ARGB32, false);
			m_RenderTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
			
			byte[] bytes = m_RenderTexture.EncodeToPNG();
			Destroy(m_RenderTexture);
			
			File.WriteAllBytes(TEXTURE_PATH + m_TextureName + TEXTURE_EXT, bytes);

			//QualitySettings.antiAliasing = oldAntiAliasingSettings;

			Application.CaptureScreenshot(TEXTURE_PATH + m_TextureName + TEXTURE_EXT, 10);
		}
	}
}
