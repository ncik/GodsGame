using UnityEngine;
using System.Collections;
using System.IO;

public class RenderCamera : MonoBehaviour
{
	//public static string TEXTURE_PATH = "C:/Users/samir/Documents/GodsGame/Sheets/";
	public static string TEXTURE_PATH = "C:/Users/Public/Documents/Unity Projects/GodsGame/Sheets/";
	public static string TEXTURE_EXT = ".png";

	public string m_TextureName;
	public int m_Quality;

	private Texture2D m_RenderTexture;
	private bool m_IsReady = true;
	private int m_Count = 0;
	private GameObject m_CardSheets = null;

	// Use this for initialization
	public void Start()
	{
		m_IsReady = true;
	}
	
	// Update is called once per frame
	public void Update()
	{
		if (m_IsReady && m_CardSheets != null && m_Count < m_CardSheets.transform.childCount)
		{
			for (int i = 0; i < m_CardSheets.transform.childCount; ++i)
			{
				m_CardSheets.transform.GetChild(i).gameObject.SetActive(i == m_Count);
			}
			m_IsReady = false;
		}
		else if (m_CardSheets != null && m_Count >= m_CardSheets.transform.childCount)
		{
			m_CardSheets = null;
		}
	}
    
	public void OnPostRender()
	{
		if (!m_IsReady)
		{
			++m_Count;
            Application.CaptureScreenshot(TEXTURE_PATH + m_TextureName + "_" + m_Count + TEXTURE_EXT, m_Quality);
            
            Debug.Log("Sheet " + m_Count + " saved.");
			m_IsReady = true;
		}
	}

	public void RenderCardSheets(GameObject cardSheets)
	{
		m_CardSheets = cardSheets;
		m_Count = 0;
	}
}
