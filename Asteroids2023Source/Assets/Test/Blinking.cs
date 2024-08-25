// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class Blinking : MonoBehaviour
// {
//     [SerializeField] private SpriteRenderer[] spriteRenderers;
//     [SerializeField] private float duration = 2f;
//     [SerializeField] private float speed = 1f;
//
//     public static void Blink(SpriteRenderer[] spriteRenderers, float duration, float speed)
//     {
//         // StartCoroutine(BlinkCoroutine(spriteRenderers, duration, speed));
//     }
//
//     private IEnumerator BlinkCoroutine(SpriteRenderer[] spriteRenderers, float duration, float speed)
//     {
//         var elapsedTime = 0f;
//         while( elapsedTime <= duration )
//         {
//             for (int i=0; i < spriteRenderers.Length; i++)
//             {
//                 Color color = spriteRenderers[i].color;
//                 
//                 color.a = Mathf.PingPong( elapsedTime * speed, 1f );
//
//                 spriteRenderers[i].color = color;
//             }
//             
//             elapsedTime += Time.deltaTime;
//             
//             yield return null;
//         }
//     }
//     
//     private IEnumerator BlinkCoroutine(SpriteRenderer[] spriteRenderers, float duration, float speed, float lowValue, float highValue)
//     {
//         List<float> alphas = new List<float>(spriteRenderers.Length);
//
//         for (int i = 0; i < spriteRenderers.Length; i++)
//         {
//             alphas.Add(spriteRenderers[i].color.a);
//         }
//
//         float range = (highValue - lowValue);
//         
//         var elapsedTime = 0f;
//         
//         while( elapsedTime <= duration )
//         {
//             for (int i=0; i < spriteRenderers.Length; i++)
//             {
//                 Color color = spriteRenderers[i].color;
//                 
//                 color.a = lowValue + Mathf.PingPong( elapsedTime * speed, range );
//
//                 spriteRenderers[i].color = color;
//             }
//             
//             elapsedTime += Time.deltaTime;
//             
//             yield return null;
//         }
//         
//         // set the same original alpha
//         for (int i=0; i < spriteRenderers.Length; i++)
//         {
//             Color color = spriteRenderers[i].color;
//                 
//             color.a = alphas[i];
//
//             spriteRenderers[i].color = color;
//         }
//     }
//     
//     IEnumerator blinkSmooth(float timeScale, float duration, Color blinkColor )
//     {
//         var material = GetComponent<SpriteRenderer>().material;
//         var elapsedTime = 0f;
//         while( elapsedTime <= duration )
//         {
//             material.SetColor( "_BlinkColor", blinkColor );
//
//             blinkColor.a = Mathf.PingPong( elapsedTime * timeScale, 1f );
//             elapsedTime += Time.deltaTime;
//             yield return null;
//         }
//
//         // revert to our standard sprite color
//         blinkColor.a = 0f;
//         material.SetColor( "_BlinkColor", blinkColor );
//     }
//     
//     
//     IEnumerator FadeOnce(float duration) 
//     { 
//         bool alreadyFading = true;
//
//         float from = 1.0f;
//         float to = 0.0f;
//
//         float timePassed = 0f;
//         
//         while(timePassed < duration)
//         { 
//             print("HELLO!");
//             float factor = timePassed / duration; 
//             float value = Mathf.Lerp(from, to, factor); 
//
//             for (int i = 0; i < spriteRenderers.Length; i++)
//             {
//                 Color color = spriteRenderers[i].color;
//                 color.a = value;
//                 print(value);
//                 spriteRenderers[i].color = color;
//             }
//
//             timePassed = timePassed + Time.deltaTime;
//             
//             yield return null; 
//         }
//     }
//
//     
//     // Update is called once per frame
//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.F1))
//         {
//             StartCoroutine(FadeOnce(duration));
//         }
//         
//         if (Input.GetKeyDown(KeyCode.F2))
//         {
//             Blink(spriteRenderers,duration,speed);
//         }
//     }
// }
