using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] string uniqueIndentifier = "";

        static Dictionary<string, SaveableEntity> globalLookup = new Dictionary<string, SaveableEntity>();

        //Getter unique identifier variable
        public string GetUniqueIndentifier()
        {
            return uniqueIndentifier;
        }

        //saves state to dictionary of each component with the ISaveable interface attached
        public object CaptureState()
        {

            Dictionary<string, object> state = new Dictionary<string, object>();

            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;
        }

        //loads state of components with ISaveable from dictionary
        public void RestoreState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;

            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                string typeString = saveable.GetType().ToString();
                if (stateDict.ContainsKey(typeString))
                {
                    saveable.RestoreState(stateDict[typeString]);
                }
            }
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return; //does not assign unique indetifier to prefabs

            SerializedObject serializedObject = new SerializedObject(this);

            //find unique identifier property
            SerializedProperty property = serializedObject.FindProperty("uniqueIndentifier");

            //assigns new unique identifier if null/empty or is not unique
            if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

            globalLookup[property.stringValue] = this;
        }
#endif

        //determines if key is unique
        private bool IsUnique(string candidate)
        {
            //if key not used yet in globalLookup 
            if (!globalLookup.ContainsKey(candidate)) return true;
            //if key is current candidate
            if (globalLookup[candidate] == this) return true;
            //if candidate null, remove from list
            if (globalLookup[candidate] == null)
            {
                globalLookup.Remove(candidate);
                return true;
            }
            //if global lookup candidates unique indentifier is not the current candidate, remove from list
            if (globalLookup[candidate].GetUniqueIndentifier() != candidate)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            return false;

        }
    }
}
