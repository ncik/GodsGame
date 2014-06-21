using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Card : MonoBehaviour
{
	public static int MAX_LINE_LENGTH = 28;
	public static int BOX_BORDER = 1;

	public TextMesh m_GodText;
	public TextMesh m_NameText;
	public TextMesh m_PowerText;
	public TextMesh m_AbilitiesText;
	public Transform m_AbilitiesBox;

	public GameObject m_UpArrow;
	public GameObject m_DownArrow;
	public GameObject m_LeftArrow;
	public GameObject m_RightArrow;

	[SerializeField] private string m_God;
	[SerializeField] private string m_CardName;
	[SerializeField] private int m_Power;
	[SerializeField] private string m_Abilities;

	public bool m_ShowUpArrow;
	public bool m_ShowDownArrow;
	public bool m_ShowLeftArrow;
	public bool m_ShowRightArrow;
	
	private float m_MinBoxHeight;
	
	public string God
	{
		get { return m_God; }
		set { m_GodText.text = m_God = value; }
	}

	public string Name
	{
		get { return m_CardName; }
		set { m_NameText.text = m_CardName = value; }
	}

	public int Power
	{
		get { return m_Power; }
		set { m_PowerText.text = (m_Power = value).ToString(); }
	}

	public string Abilities
	{
		get { return m_Abilities; }
		set { SetAbilitiesText(m_Abilities = value); }
	}

	// Use this for initialization
	void Start()
	{
		m_MinBoxHeight = m_AbilitiesBox.localScale.y;
		bool[] m_Arrows = new bool[4] { m_ShowUpArrow, m_ShowDownArrow, m_ShowLeftArrow, m_ShowRightArrow };

		Init(m_God, m_CardName, m_Power, m_Abilities, m_Arrows);
	}
	
	// Update is called once per frame
	void Update()
	{

	}

	public void Init(string god, string name, int power, string abilities, bool[] arrows)
	{
		God = god;
		Name = name;
		Power = power;
		Abilities = abilities;
		m_ShowUpArrow = arrows[0];
		m_ShowDownArrow = arrows[1];
		m_ShowLeftArrow = arrows[2];
		m_ShowRightArrow = arrows[3];
		SetArrows();
	}

	private void SetArrows()
	{
		m_UpArrow.SetActive(m_ShowUpArrow);
		m_DownArrow.SetActive(m_ShowDownArrow);
		m_LeftArrow.SetActive(m_ShowLeftArrow);
		m_RightArrow.SetActive(m_ShowRightArrow);
	}

	private void SetAbilitiesText(string abilities)
	{
		m_AbilitiesText.text = WrapText(abilities, MAX_LINE_LENGTH);

		Vector3 v = m_AbilitiesBox.localScale;
		v.y = Mathf.Max(m_AbilitiesText.renderer.bounds.size.y + BOX_BORDER, m_MinBoxHeight);
		m_AbilitiesBox.localScale = v;

		v = m_AbilitiesBox.localPosition;
		v.y = m_AbilitiesBox.localScale.y / 2f;
		m_AbilitiesBox.localPosition = v;

		v = m_AbilitiesText.transform.localPosition;
		v.y = m_AbilitiesBox.localPosition.y;
		m_AbilitiesText.transform.localPosition = v;
	}

	private string WrapText(string input, int lineLength)
	{     
		string[] words = input.Split(" "[0]);
		string result = "";
		string line = "";
		foreach(string s in words)
		{
			string temp = line + " " + s;
			if(temp.Length > lineLength)
			{
				result += line + "\n";
				line = s;
			}
			else if (temp.Contains("\n"))
			{
				string[] split = temp.Split(new string[]{"\n"}, 2, System.StringSplitOptions.None);
				result += split[0] + "\n";
				line = split[1];
			}
			else
			{
				line = temp;
			}
		}
		result += line;
		return result.Substring(1,result.Length-1);
	}
}
