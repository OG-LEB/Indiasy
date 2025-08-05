using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteAnimationByFrames : MonoBehaviour
{
    [SerializeField] private float TimeBetweenFrames;
    [SerializeField] private Sprite[] KotelFrames;
    private SpriteRenderer _spriteRenderer;
    private int frameCounter;
    //private void Start() 
    //{
    //    _spriteRenderer = GetComponent<SpriteRenderer>();
    //    StartCoroutine(Animation());
    //}
    IEnumerator Animation()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeBetweenFrames);
            _spriteRenderer.sprite = KotelFrames[frameCounter];
            frameCounter++;
            if (frameCounter >= KotelFrames.Length) frameCounter = 0;
        }
    }
    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Animation());
    }
}
