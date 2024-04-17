using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Subject
{
    void subscribe(Observer observer);
    void unsubscribe(Observer observer);
    void notify();
}
