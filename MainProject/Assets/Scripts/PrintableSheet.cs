using UnityEngine;
using System.Collections;

public class PrintableSheet : MonoBehaviour
{
	public GameObject m_CardPrefab;
	public GameObject m_Cards;
	public Transform[] m_Anchors;

	// Use this for initialization
	void Start()
	{
		for (int i = 0; i < m_Anchors.Length; ++i)
		{
			GameObject card = Instantiate(m_CardPrefab, m_Anchors[i].transform.position, Quaternion.identity) as GameObject;
			card.transform.parent = m_Cards.transform;
		}
	}
	
	// Update is called once per frame
	void Update()
	{

	}
}
