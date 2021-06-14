using System;
using UnityEngine;

public class Block : MonoBehaviour
{

    //config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;   
    [SerializeField] Sprite[] hitSprites;

    // cached reference
    Level level;
    GameSession gameStatus;

    // state variables
    [SerializeField] int timesHit;  // TODO only serialized for debug purposes
    private void Start() {

        gameStatus = FindObjectOfType<GameSession>();
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks() {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable") {
            level.CountBlock();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (tag == "Breakable") {
            HandleHit();
        }

    }

    private void HandleHit() {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits) {
            DestroyBlock();
        }
        else {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite() {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null) { 
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else {
            Debug.LogError("Block sprite is missing from array: " + gameObject.name);
        }
    }

    private void DestroyBlock() {
        PlayBlockDestroySFX();
        Destroy(gameObject);
        TriggerSparklesVFX();
        level.BlockDestroyed();
        gameStatus.AddToScore();
    }

    private void PlayBlockDestroySFX() {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX() {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
