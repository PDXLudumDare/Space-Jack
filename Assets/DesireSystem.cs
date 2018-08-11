using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesireSystem : MonoBehaviour {
	[SerializeField] Emote emoteBubble;
	[SerializeField] Sprite happyIcon;
	[SerializeField] Sprite angryIcon;
	[SerializeField] float destroyEmoteTime = 3f;

	public  Item currentDesire;

	Emote currentEmote;

	public void CreateDesire()
    {
        Item[] items = FindObjectOfType<ItemHandler>().availableItems;
        currentDesire = items[UnityEngine.Random.Range(0, items.Length)];
        CreateEmote(currentDesire.GetComponent<SpriteRenderer>().sprite);
    }

	public void LoseDesire(){
		currentDesire = null;
		DestroyCurrentEmote();
		StartCoroutine(ActivateTempBubble(angryIcon));
	}

    private void CreateEmote(Sprite emoteSprite)
    {
        GameObject emoteObj = Instantiate(emoteBubble.gameObject, transform);
        currentEmote = emoteObj.GetComponent<Emote>();
        currentEmote.SetEmoteIcon(emoteSprite);
    }

    public void FulfillDesire(Item droppedItem){
		if (droppedItem == currentDesire){
			StartCoroutine(ActivateTempBubble(happyIcon));
		}
	}

    private IEnumerator ActivateTempBubble(Sprite icon)
    {
        CreateEmote(icon);
		yield return new WaitForSecondsRealtime(destroyEmoteTime);
		DestroyCurrentEmote();
		
    }

    private void DestroyCurrentEmote()
    {
		if (currentEmote){
			Destroy(currentEmote.gameObject, .1f);
		}
        
    }
}
