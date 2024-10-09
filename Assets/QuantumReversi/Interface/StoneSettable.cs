using System.Collections;
using UnityEngine;

public interface StoneSettable
{
    public bool IsSettable { get; set; }

    public void Focus();

    void StoneSet(StoneType type);
  
}