using System;
using UnityEngine;

namespace Molecules
{
    public class BodyStatus : MonoBehaviour
    {
       public static BodyStatus Instance { get; private set; }

       [SerializeField] private float _glucoseLevel = 100;
       [SerializeField] private float _sodiumLevel = 100;
       
       private void Awake()
       {
              if (Instance == null)
              {
                Instance = this;
              }
              else
              {
                Destroy(gameObject);
              }
       }
    }
}
