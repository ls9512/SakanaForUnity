using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Aya.Sakana.Example
{
    public class Test : MonoBehaviour
    {
        public float Interval;
        [NonSerialized] public List<Sakana> SakanaList;

        public void Awake()
        {
            SakanaList = new List<Sakana>();
            SakanaList.AddRange(FindObjectsOfType<Sakana>());
        }

        public void OnEnable()
        {
            foreach (var sakana in SakanaList)
            {
                StartCoroutine(AutoPlay(sakana));
            }
        }

        public IEnumerator AutoPlay(Sakana sakana)
        {
            while (true)
            {
                sakana.Play();

                while (sakana.IsRunning)
                {
                    yield return null;
                }

                yield return new WaitForSeconds(Random.Range(0f, Interval));
            }
        }
    }
}