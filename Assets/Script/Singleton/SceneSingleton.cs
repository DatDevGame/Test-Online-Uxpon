﻿using UnityEngine;

/// <summary>
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// </summary>
public class SceneSingleton<T> : MonoBehaviour where T : Component
{
    protected static T _instance;

    /// <summary>
    /// Singleton design pattern
    /// </summary>
    /// <value>The instance.</value>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "Singleton_" + typeof(T).ToString();
                    //obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    /// <summary>
    /// On awake, we check if there's already a copy of the object in the scene. If there's one, we destroy it.
    /// </summary>
    private void Awake()
    {


        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this as T;
            OnAwake();
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
            else
            {
                OnAwake();
            }
        }
        
    }

    protected virtual void OnAwake()
    {

    }
    
    public virtual void Init()
    {

    }
}
