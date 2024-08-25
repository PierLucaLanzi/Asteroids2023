using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace POLIMIGameCollective
{
    public class Utility 
    {
        public static string RandomString(int n = 10, string glyphs="abcdefghijklmnopqrstuvwxyz0123456789")
        {
            StringBuilder str = new StringBuilder(" ", n);
            for (int i = 0; i < n; i++)
            {
                str[i] = glyphs[Random.Range(0, glyphs.Length)];
            }

            return str.ToString();
        }
        
        public static void Swap<T>(T a, T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
        
        public static void Shuffle<T>(List<T> vector)
        {
            int n = vector.Count;
            for (int i = n - 1; i >= 1; i--)
            {
                int j = UnityEngine.Random.Range(0, (i + 1));

                Swap(vector[i], vector[j]);
            }
        }

    }
    

}

