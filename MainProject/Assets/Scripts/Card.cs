using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Card : MonoBehaviour
{
	//public static string ABILITIES_PATH = "C:/Users/samir/Documents/GodsGame/GodAbilities.csv";
	public static string ABILITIES_PATH = "C:/Users/Public/Documents/Unity Projects/GodsGame/GodAbilities.csv";
	
	public static int BOX_BORDER = 1;
	// gods
	public static int MAX_LINE_LENGTH = 38;
	public static int MIN_BOX_HEIGHT = 26;
	// king
	//public static int MAX_LINE_LENGTH = 64;
	//public static int MIN_BOX_HEIGHT = 26;

	public TextMesh m_GodText;
	public TextMesh m_NameText;
	public TextMesh m_TypeText;
	public TextMesh m_PowerText;
	public TextMesh m_AbilitiesText;
	public Transform m_AbilitiesBox;

	public GameObject m_UpArrow;
	public GameObject m_DownArrow;
	public GameObject m_LeftArrow;
	public GameObject m_RightArrow;

	[SerializeField] private string m_God;
	[SerializeField] private string m_CardName;
	[SerializeField] private string m_Type;
	[SerializeField] private int m_Power;
	[SerializeField] private string m_Abilities;

	public bool m_ShowUpArrow;
	public bool m_ShowDownArrow;
	public bool m_ShowLeftArrow;
	public bool m_ShowRightArrow;

	private static Dictionary<string, string> m_AbilityDictionary;
	
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
	
	public string Type
	{
		get { return m_Type; }
		set { m_TypeText.text = m_Type = value; }
	}

	public int Power
	{
		get { return m_Power; }
		set { m_PowerText.text = value > 0 ? (m_Power = value).ToString() : ""; }
	}

	public string Abilities
	{
		get { return m_Abilities; }
		set { SetAbilitiesText(m_Abilities = value); }
		/*
		set
		{
			m_AbilitiesText.text = WrapText(m_Abilities = value, MAX_LINE_LENGTH);
		}
		*/
	}

	public static Dictionary<string, string> AbilityDictionary
	{
		get
		{
			if (m_AbilityDictionary == null)
			{
				LoadAbilitiesFromFile(ABILITIES_PATH);
			}
			return m_AbilityDictionary;
		}
	}

	// Use this for initialization
	public void Start()
	{
		bool[] m_Arrows = new bool[4] { m_ShowUpArrow, m_ShowDownArrow, m_ShowLeftArrow, m_ShowRightArrow };

		Init(m_God, m_CardName, m_Type, m_Power, m_Abilities, m_Arrows);
	}
	
	// Update is called once per frame
	public void Update()
	{

	}

	public void Init(Card card)
	{
		Init(card.God, card.Name, card.Type, card.Power, card.Abilities, new bool[] { m_ShowUpArrow, m_ShowDownArrow, m_ShowLeftArrow, m_ShowRightArrow });
	}

	public void Init(string god, string name, string type, int power, string abilities, bool[] arrows)
	{
		God = god;
		Name = name;
		Type = type;
		Power = power;
		Abilities = abilities;
		m_ShowUpArrow = arrows[0];
		m_ShowDownArrow = arrows[1];
		m_ShowLeftArrow = arrows[2];
		m_ShowRightArrow = arrows[3];
		SetArrows();
	}

	public void Load(string[] data)
	{
		bool[] arrows = new bool[4];
		arrows[0] = bool.Parse(data[4]);
		arrows[1] = bool.Parse(data[5]);
		arrows[2] = bool.Parse(data[6]);
		arrows[3] = bool.Parse(data[7]);
		string abilities = "";
		for (int i = 8; i < data.Length; ++i)
		{
			if (data[i].Length > 0)
			{
				if (i > 8)
				{
					abilities += "\n\n";
				}
				if (AbilityDictionary.ContainsKey(data[i]))
				{
					abilities += data[i] + ": " + AbilityDictionary[data[i]];
				}
				else
				{
					abilities += data[i] + ".";
				}
			}
		}

		Init(data[0], data[1], data[2], int.Parse(data[3]), abilities, arrows);
	}

	public void SetArrows()
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
		v.y = Mathf.Max(m_AbilitiesText.renderer.bounds.size.y + BOX_BORDER, MIN_BOX_HEIGHT);
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
				result += line + "\n" /*+ "    "*/;
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

	public static void LoadAbilitiesFromFile(string filePath)
	{
		m_AbilityDictionary = new Dictionary<string, string>();
		string[] lines = System.IO.File.ReadAllLines(filePath);
		
		// skip the first line
		for (int i = 1; i < lines.Length; ++i)
		{
			string[] split = lines[i].Split(new string[]{";"}, 2, System.StringSplitOptions.RemoveEmptyEntries);
			m_AbilityDictionary.Add(split[0], split[1]);
		}
	}
}
