using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprRandomizer : MonoBehaviour
{
    [SerializeField] List<Sprite> sprList;

    // Start is called before the first frame update
    void Start()
    {
        SetRdnSpr();
    }

    private void SetRdnSpr()
    {
        int rndSpr = Random.Range(0, sprList.Count);
        GetComponent<SpriteRenderer>().sprite = sprList[rndSpr];
    }
}
