using UnityEngine;
using UnityEngine.UI;

public class WorldMap : MonoBehaviour
{
	public GameObject worldMapCamera;
	private Text text;
	public Image image;
	

	public void Start()
	{
		text = GetComponentInChildren<Text>();
		image = GetComponent<Image>();
		WorldMapDeactivated();
	}

	public void WorldMapActivated()
	{
		image.enabled = false;
		text.enabled = false;
		worldMapCamera.SetActive(true);
	}

	public void WorldMapDeactivated()
	{
		image.enabled = true;
		text.enabled = true;
		worldMapCamera.SetActive(false);
	}
}
