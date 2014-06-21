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
	public int m_Quality;

	private Texture2D m_RenderTexture;
	private bool m_IsSaved = false;

	// Use this for initialization
	void Start()
	{

	}
	
	// Update is called once per frame
	void Update()
	{

	}

	void OnPostRender()
	{
		if (!m_IsSaved)
		{
			m_RenderTexture = new Texture2D(Mathf.RoundToInt(Screen.width), Mathf.RoundToInt(Screen.height), TextureFormat.ARGB32, false);
			m_RenderTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
			
			byte[] bytes = m_RenderTexture.EncodeToPNG();
			Destroy(m_RenderTexture);
			
			File.WriteAllBytes(TEXTURE_PATH + m_TextureName + TEXTURE_EXT, bytes);

			Application.CaptureScreenshot(TEXTURE_PATH + m_TextureName + TEXTURE_EXT, m_Quality);

			m_IsSaved = true;
		}
	}
}
