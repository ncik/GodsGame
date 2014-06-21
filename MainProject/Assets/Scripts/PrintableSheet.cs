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
		foreach (Transform anchor in m_Anchors)
		{
			GameObject card = Instantiate(m_CardPrefab, anchor.transform.position, Quaternion.identity) as GameObject;
			card.transform.parent = m_Cards.transform;
		}
	}
	
	// Update is called once per frame
	void Update()
	{

	}
}
