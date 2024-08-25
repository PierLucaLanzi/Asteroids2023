using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEffects : MonoBehaviour
{
    public static IEnumerator BlinkCoroutine(SpriteRenderer[] spriteRenderers, float duration, float speed, float lowValue, float highValue)
    {
        // print("BLINK COROUTINE HAS BEEN CALLED");
        List<float> alphas = new List<float>(spriteRenderers.Length);

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            alphas.Add(spriteRenderers[i].color.a);
        }

        float range = (highValue - lowValue);
        
        var elapsedTime = 0f;
        
        while( elapsedTime <= duration )
        {
            for (int i=0; i < spriteRenderers.Length; i++)
            {
                Color color = spriteRenderers[i].color;
                
                color.a = lowValue + Mathf.PingPong( elapsedTime * speed, range );

                spriteRenderers[i].color = color;
            }
            
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }
        
        // set the same original alpha
        for (int i=0; i < spriteRenderers.Length; i++)
        {
            Color color = spriteRenderers[i].color;
                
            color.a = alphas[i];

            spriteRenderers[i].color = color;
        }
        
        // print("BLINK COROUTINE HAS FINISHED");

    }
    
    static IEnumerator FadeOnce(SpriteRenderer[] spriteRenderers, float duration) 
    { 
        bool alreadyFading = true;

        float from = 1.0f;
        float to = 0.0f;

        float timePassed = 0f;
        
        while(timePassed < duration)
        { 
            print("HELLO!");
            float factor = timePassed / duration; 
            float value = Mathf.Lerp(from, to, factor); 

            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                Color color = spriteRenderers[i].color;
                color.a = value;
                print(value);
                spriteRenderers[i].color = color;
            }

            timePassed = timePassed + Time.deltaTime;
            
            yield return null; 
        }
    }
    
    public static IEnumerator BlinkSmooth(SpriteRenderer spriteRenderer, float lowAlpha, float highAlpha, float speed, float duration)
    {
        float range = Mathf.Abs(highAlpha - lowAlpha);
        Color originalColor = spriteRenderer.color;
        
        var elapsedTime = 0f;
        while(elapsedTime <= duration)
        {
            Color color = spriteRenderer.color;
            color.a = lowAlpha + Mathf.PingPong( elapsedTime * speed, range );
            elapsedTime += Time.deltaTime;
            spriteRenderer.color = color;
            yield return null;
        }

        // revert to our standard sprite color
        spriteRenderer.color = originalColor; 
    }

}
