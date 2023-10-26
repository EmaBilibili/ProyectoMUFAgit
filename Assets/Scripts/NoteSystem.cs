using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteSystem : MonoBehaviour
{
   [SerializeField] private Image _noteImage;

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         _noteImage.enabled = true;
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         _noteImage.enabled = false;
      }
   }
}
