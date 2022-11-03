using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgainBtn : MonoBehaviour
{
    public void RestartTheScene()
    {
        
        // Reset the static values
        Level1GameManager.tutorialMode = true;
        Level1GameManager.eligibleToPlay = true;
        Level1GameManager.tempAct = 0;
        Level1GameManager.negativeScore = 0;
        Level1GameManager.negativeScoreLocked = false;
        Level1GameManager.noMistake = true;
        Level1GameManager.basketHovered = false;
        Level1GameManager.trashHovered = false;
        Level1GameManager.FarakhnaActualScore = 0;
        Level1GameManager.EffortScore = 0;
        Level1GameManager.DeletionError = 0;
        Level1GameManager.SelectionError = 0;
        Level1GameManager.RepetitionError = 0;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
