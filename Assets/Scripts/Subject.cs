using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject {
	//A list with observers that are waiting for something to happen
	private List<Observer> observers = new List<Observer>();

	//Send notifications if something has happened
	public void Notify(string ev, GameObject obj) {
		for (int i = 0; i < observers.Count; i++) {
			//Notify all observers even though some may not be interested in what has happened
			//Each observer should check if it is interested in this event
			observers[i].OnNotify(ev, obj);
		}
	}

	// Add a observer to the list
	public void AddObservers(Observer obs) {
		observers.Add(obs);
	}

	//Remove observer from the list
	public void RemoveObserver(Observer obs) {
		observers.Remove(obs);
	}
}
