using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observer
{
    void updated(Subject subject);
}
