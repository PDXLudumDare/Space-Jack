using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FarrokhGames.Inventory;


public class DesireSystem : MonoBehaviour {
	[SerializeField] Emote emoteBubble;
	[SerializeField] Sprite happyIcon;
	[SerializeField] Sprite angryIcon;
	[SerializeField] float destroyEmoteTime = 3f;
    [SerializeField] Vector3 emoteOffset;

	public IInventoryItem currentDesire;

	Emote currentEmote;
    PlayerStatus status;

    void Start(){
        status = FindObjectOfType<PlayerStatus>();
    }

	public void CreateDesire()
    {
        ItemDefinition[] items = FindObjectOfType<ConveyerBelt>().itemsToSpawn;
        currentDesire = ScriptableObject.Instantiate(items[UnityEngine.Random.Range(0, items.Length)]);
        CreateEmote(currentDesire.Sprite);
    }

	public void LoseDesire(int penalty = 1){
        status.ChangeSecurityPoints(-penalty);
		currentDesire = null;
		DestroyCurrentEmote();
		StartCoroutine(ActivateTempBubble(angryIcon));
	}
            
    public void FulfillDesire(int reward = 1){
        status.ChangeSecurityPoints(reward);
        currentDesire = null;
        DestroyCurrentEmote();
		StartCoroutine(ActivateTempBubble(happyIcon));
	}

    private Emote CreateEmote(Sprite emoteSprite)
    {
        GameObject emoteObj = Instantiate(emoteBubble.gameObject, transform.position + emoteOffset, Quaternion.identity, transform);
        currentEmote = emoteObj.GetComponent<Emote>();
        currentEmote.SetEmoteIcon(emoteSprite);
        return currentEmote;
    }

    private IEnumerator ActivateTempBubble(Sprite icon)
    {
        Emote tempEmote = CreateEmote(icon);
		yield return new WaitForSecondsRealtime(destroyEmoteTime);
		Destroy(tempEmote.gameObject, .1f);
		
    }

    private void DestroyCurrentEmote()
    {
		if (currentEmote){
			Destroy(currentEmote.gameObject, .1f);
		}
        
    }

    public void GetItem(IInventoryItem item)
    {
        if (item == null) { return; }
        if (currentDesire != null && currentDesire.Name == item.Name){
            FulfillDesire();
        }else{
            LoseDesire();
        }
    }

    void OnMouseOver()
    {
        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>()){
            renderer.color = Color.red;
        }
    }

    void OnMouseExit()
    {
        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>()){
            renderer.color = Color.white;
        }
    }
}
