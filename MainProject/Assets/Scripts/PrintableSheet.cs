using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrintableSheet : MonoBehaviour
{
	public static float RENDER_DELAY = 0.1f;
	public static string DECK_PATH = "C:/Users/samir/Documents/GodsGame/Decks/";
	//public static string TEXTURE_PATH = "C:/Users/Public/Documents/Unity Projects/GodsGame/Decks/";
	public static string DECK_EXT = ".csv";

	public RenderCamera m_RenderCamera;
	public GameObject m_CardPrefab;
	public GameObject m_Cards;
	public GameObject m_KingCards;
	public Transform[] m_Anchors;

	// Use this for initialization
	public void Start()
	{
		//string[] gods = new string[]{"Zeus", "Zeus", "Zeus", "Zeus", "Zeus"};
		//StartCoroutine("CreateSheets", gods);
		StartCoroutine("CreateSheets");
	}
	
	// Update is called once per frame
	public void Update()
	{

	}

	public IEnumerator CreateSheets(/*string[] gods*/)
	{
		List<Card> cards = new List<Card>();
		/*
		foreach (string god in gods)
		{
			cards.AddRange(LoadCardsFromFile(god));
		}
		*/
		cards.AddRange(m_KingCards.GetComponentsInChildren<Card>(true));
		cards.AddRange(m_KingCards.GetComponentsInChildren<Card>(true));
		cards.AddRange(m_KingCards.GetComponentsInChildren<Card>(true));
		while (cards.Count > 0)
		{
			foreach (Transform child in m_Cards.transform)
			{
				//Destroy(child.gameObject);
				child.gameObject.SetActive(false);
			}
			for (int i = 0; i < m_Anchors.Length; ++i)
			{
				if (cards.Count > 0)
				{
					cards[0].gameObject.SetActive(true);
					cards[0].transform.position = m_Anchors[i].position;
					cards[0].transform.parent = m_Cards.transform;
					cards.RemoveAt(0);
				}
            }
			yield return new WaitForSeconds(RENDER_DELAY);
			m_RenderCamera.SaveRenderTexture();
        }
    }
    
	public Card[] LoadCardsFromFile(string fileName)
	{
		List<Card> cards = new List<Card>();
		string[] lines = System.IO.File.ReadAllLines(DECK_PATH + fileName + DECK_EXT);
		
		// skip the first line
		for (int i = 1; i < lines.Length; ++i)
		{
			GameObject card = Instantiate(m_CardPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			card.GetComponent<Card>().Load(lines[i].Split(new string[]{";"}, System.StringSplitOptions.RemoveEmptyEntries));
            cards.Add(card.GetComponent<Card>());
			card.SetActive(false);
        }
        return cards.ToArray();
	}
}
