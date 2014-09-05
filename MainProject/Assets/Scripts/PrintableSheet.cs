using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrintableSheet : MonoBehaviour
{
	public static float RENDER_DELAY = 0.1f;
	//public static string DECK_PATH = "C:/Users/samir/Documents/GodsGame/Decks/";
	public static string DECK_PATH = "C:/Users/Public/Documents/Unity Projects/GodsGame/Decks/";
	public static string DECK_EXT = ".csv";

	public RenderCamera m_RenderCamera;
	public GameObject m_CardPrefab;
	public GameObject m_Cards;
	public GameObject m_KingCards;
	public Transform[] m_Anchors;
	public string[] m_Gods;

	// Use this for initialization
	public void Start()
	{
		CreateGodSheets(m_Gods);
		//CreateKingSheets();
	}
	
	// Update is called once per frame
	public void Update()
	{

	}

	public void CreateKingSheets()
	{
		List<Card> cards = new List<Card>();
		cards.AddRange(m_KingCards.GetComponentsInChildren<Card>(true));
		CreateSheets(cards);
	}

	public void CreateGodSheets(string[] gods)
	{
		List<Card> cards = new List<Card>();
		foreach (string god in gods)
		{
			cards.AddRange(LoadCardsFromFile(god));
		}
		CreateSheets(cards);
    }

	private void CreateSheets(List<Card> cards)
	{
		int pageCount = 0;
		foreach (Transform child in m_Cards.transform)
		{
			Destroy(child.gameObject);
			//child.gameObject.SetActive(false);
		}
		while (cards.Count > 0)
		{
			++pageCount;
			GameObject page = new GameObject("Page" + pageCount);
			page.transform.parent = m_Cards.transform;
			for (int i = 0; i < m_Anchors.Length; ++i)
			{
				if (cards.Count > 0)
				{
					cards[0].gameObject.SetActive(true);
					cards[0].transform.position = m_Anchors[i].position;
					cards[0].transform.parent = page.transform;
					cards.RemoveAt(0);
				}
				else
				{
					GameObject card = Instantiate(m_CardPrefab, m_Anchors[i].position, Quaternion.identity) as GameObject;
					card.GetComponent<Card>().Init("", "", "", 0, "", new bool[4]);
					card.transform.parent = page.transform;
				}
			}
		}
		m_RenderCamera.RenderCardSheets(m_Cards);
	}
    
	public Card[] LoadCardsFromFile(string fileName)
	{
		List<Card> cards = new List<Card>();
		string[] lines = System.IO.File.ReadAllLines(DECK_PATH + fileName + DECK_EXT);
		
		// skip the first line
		for (int i = 1; i < lines.Length; ++i)
		{
			for (int copy = 0; copy < 3; ++copy)
			{
				GameObject cardObject = Instantiate(m_CardPrefab, Vector3.zero, Quaternion.identity) as GameObject;
				Card cardComponent = cardObject.GetComponent<Card>();
				cardComponent.Load(lines[i].Split(new string[]{";"}, System.StringSplitOptions.RemoveEmptyEntries));
				if (cardComponent.Type.Equals("Minion"))
				{
					switch (copy)
					{
					case 0:
						cardComponent.m_ShowUpArrow = true;
						cardComponent.m_ShowDownArrow = false;
						cardComponent.m_ShowLeftArrow = true;
						cardComponent.m_ShowRightArrow = true;
						break;
					case 1:
						cardComponent.m_ShowUpArrow = true;
						cardComponent.m_ShowDownArrow = true;
						cardComponent.m_ShowLeftArrow = true;
						cardComponent.m_ShowRightArrow = false;
						break;
					case 2:
						cardComponent.m_ShowUpArrow = true;
						cardComponent.m_ShowDownArrow = true;
						cardComponent.m_ShowLeftArrow = false;
						cardComponent.m_ShowRightArrow = true;
						break;
					}
					cardComponent.SetArrows();
				}
				cards.Add(cardComponent);
				cardObject.SetActive(false);

				// print 3 copies of each card except 
				if (cardComponent.Type.Equals("God"))
				{
					break;
				}
			}
        }
        return cards.ToArray();
	}
}
