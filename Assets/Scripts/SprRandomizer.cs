using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprRandomizer : MonoBehaviour
{
    [SerializeField] List<Sprite> sprList;
    private Sprite spr;

    // Start is called before the first frame update
    void Start()
    {
        int rndSpr = Random.Range(0, sprList.Count);
        spr = GetComponent<SpriteRenderer>().sprite = sprList[rndSpr];
    }
}
