using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invador : MonoBehaviour
{
    //public Sprite[] animationSprites;
    //public float animatonTime = 1f;
    public System.Action killed;
    
    private SpriteRenderer _spriteRenderer;
    private int _animationFrame;

   // private void Awake()
   // {
   //     _spriteRenderer = GetComponent<SpriteRenderer>();
   // }

    //private void Start()
    //{
    //    InvokeRepeating(nameof(AnimatedSprite), animatonTime, animatonTime);
    //}

    //private void AnimatedSprite()
    //{
    //    _animationFrame++;
//
    //    if (_animationFrame >= animationSprites.Length)
    //        _animationFrame = 0;
//
    //    _spriteRenderer.sprite = animationSprites[_animationFrame];
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9)
        {
            killed.Invoke();
            gameObject.SetActive(false);
        }
    }
}
