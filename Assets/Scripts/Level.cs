using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int breakableBlocks;

    SceneLoader sceneLoader;

    private void Start() {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }


    public void CountBlock() {
        breakableBlocks++;
    }

    public void BlockDestroyed() {
        breakableBlocks--;

        if (breakableBlocks <= 0) {
            sceneLoader.LoadNextScene();
        }
    }

}
