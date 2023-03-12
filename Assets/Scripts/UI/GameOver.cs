using System;
using Bank;
using Player;
using UnityEngine;

namespace UI
{
   public class GameOver : BasePanel, IInit<Reborn>
   {
      [SerializeField] private Score score;
      private event Reborn _reborn;
      

      private void Update()
      {
         CheckScore();
      }

      protected override void OnContinue()
      {
         _reborn?.Invoke();
      }

      public void Initialize(Reborn @delegate)
      {
         _reborn += @delegate;
      }


      private void CheckScore()
      {
         continueButton.gameObject.SetActive(score.Value > 1);
      }
   }
}