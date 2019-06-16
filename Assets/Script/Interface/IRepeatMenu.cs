using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRepeatMenu
{
    bool isFold { get; set; }
    
    void Fold(bool isFold);
}
