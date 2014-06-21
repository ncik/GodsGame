using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour
{
	public TextMesh m_NameText;
	public TextMesh m_PowerText;
	public TextMesh m_AbilitiesText;

	public GameObject m_UpArrow;
	public GameObject m_DownArrow;
	public GameObject m_LeftArrow;
	public GameObject m_RightArrow;

	public string m_CardName;
	public int m_Power;
	public string m_Abilities;

	// Use this for initialization
	void Start()
	{
		m_NameText.text = m_CardName;
		m_PowerText.text = m_Power.ToString();
		m_AbilitiesText.text = m_Abilities;
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
