using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
   public event Action<int> OnStageChange;
   [Tooltip("an array with the number of enemies at each stage")]
   [SerializeField] private int[] enemiesCount;
   private int _currentStage = 0;
   
   /// <summary>
   /// Changes stages
   /// </summary>
   /// <param name="stageNumber"></param>
   public void OnEnemyDie(int stageNumber)
   {
      enemiesCount[stageNumber]--;
      if (enemiesCount[_currentStage] <= 0)
      {
         _currentStage++;
         if(_currentStage >= enemiesCount.Length) SceneController.SceneRestart();
         else OnStageChange?.Invoke(_currentStage);
      }
   }
   public void InitializeEnemiesOnScene(int stageNumber)
   {
      enemiesCount[stageNumber]++; 
   }
}
