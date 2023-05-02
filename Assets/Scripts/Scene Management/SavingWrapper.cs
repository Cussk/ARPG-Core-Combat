using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        [SerializeField] float fadeInTime = 0.2f;

        private void Awake()
        {
            StartCoroutine(LoadLAstScene());
        }

        //Loads scene from last save
        private IEnumerator LoadLAstScene()
        {
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            Fader fader = FindObjectOfType<Fader>();

            fader.FadeOutImmediate();
            yield return fader.FadeIn(fadeInTime);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if (Input.GetKeyDown(KeyCode.Delete)) 
            {
                Delete();
            }

        }

        //calls load from Saving System
        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        //calls save from Saving System
        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        //calls delete from Saving System
        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }
    }
}