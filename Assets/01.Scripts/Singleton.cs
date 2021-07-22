using UnityEngine; 


public class Singleton<T> : MonoBehaviour where T : MonoBehaviour 
{ 
    private static T instance = null; 
    public static T Insatnce 
    { get 
        { 
            instance = FindObjectOfType(typeof(T)) as T; 
            if (instance == null) 
            { 
                instance = new GameObject(typeof(T).ToString(), typeof(T)).AddComponent<T>(); 
            } 
            return instance; 
        } 
    } 
}

