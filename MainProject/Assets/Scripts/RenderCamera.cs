using UnityEngine;
using System.Collections;
using System.IO;

public class RenderCamera : MonoBehaviour
{
	public static string TEXTURE_PATH = "C:/Users/samir/Documents/GodsGame/Sheets/";
	//public static string TEXTURE_PATH = "C:/Users/Public/Documents/Unity Projects/GodsGame/Sheets/";
	public static string TEXTURE_EXT = ".png";

	public string m_TextureName;
	public int m_Quality;

	private Texture2D m_RenderTexture;
	private bool m_IsSaved = true;
	private int m_Count = 0;

	public bool IsReady
	{
		get { return m_IsSaved; }
	}

	// Use this for initialization
	public void Start()
	{
		m_IsSaved = true;
	}
	
	// Update is called once per frame
	public void Update()
	{

	}

	public void SaveRenderTexture()
	{
		m_IsSaved = false;
    }
    
	public void OnPostRender()
	{
		if (!m_IsSaved)
		{
			m_RenderTexture = new Texture2D(Mathf.RoundToInt(Screen.width), Mathf.RoundToInt(Screen.height), TextureFormat.ARGB32, false);
			m_RenderTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
			
			byte[] bytes = m_RenderTexture.EncodeToPNG();
			Destroy(m_RenderTexture);
			
			File.WriteAllBytes(TEXTURE_PATH + m_TextureName + TEXTURE_EXT, bytes);
			
			++m_Count;
            Application.CaptureScreenshot(TEXTURE_PATH + m_TextureName + "_" + m_Count + TEXTURE_EXT, m_Quality);
            
            Debug.Log("Sheet " + m_Count + " printed.");
			m_IsSaved = true;
		}
	}
}
