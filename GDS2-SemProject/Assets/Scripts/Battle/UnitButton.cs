using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Unit unit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Unit i)
    {
        text = GetComponentInChildren<Text>();
        //text.text = i.name;
        unit = i;
    }
}
