using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] groceries;
    [SerializeField] private GameObject basket;
    
    // Scores
    public static int FarakhnaScore = 0;
    public static int EffortScore = 0;
    public static int DeletionError = 0;
    public static int SelectionError = 0;
    public static int RepetitionError = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplaySelectedGroceries(
            new GameObject[]{groceries[1], groceries[2], groceries[11]})
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DisplaySelectedGroceries(GameObject[] selectedGroceries)
    {
        foreach (GameObject tempGameObject in selectedGroceries)
        {
            tempGameObject.AddComponent<GroceryAnimation>();
            yield return new WaitForSeconds(2.0f);
            StartCoroutine(TransformToBasket(tempGameObject, 300));
            yield return new WaitForSeconds(1.0f);
        }
        yield break;
    }

    IEnumerator TransformToBasket(GameObject transformGameObject, int totalFrames)
    {
        Vector3 tgoCurrentPosition = transformGameObject.transform.position;
        int frameCount = 0;
        while (true)
        {
            transformGameObject.transform.position = Vector3.Lerp(
                transformGameObject.transform.position, basket.transform.position, (float)frameCount/totalFrames);
            if (frameCount<totalFrames)
            {
                frameCount++;
                yield return new WaitForFixedUpdate();
            }
            else
            {
                transformGameObject.SetActive(false);
                transformGameObject.transform.position = tgoCurrentPosition;
                Destroy(transformGameObject.GetComponent<GroceryAnimation>());
                yield break;
            }
        }
    }
}
