using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class AddressableHandler:MonoBehaviour {
  [SerializeField]
  private AssetReference scene;

  [SerializeField]
  private TextMeshProUGUI progressText;

  private void Update() {
    if (Input.GetKeyUp(KeyCode.T)) {
      AsyncOperationHandle downloadOperation = Addressables.LoadSceneAsync(scene);
      downloadOperation.Completed += OnSceneDownloaded;
      StartCoroutine(OnDownloadingProgress(downloadOperation));    
    }
  }

  private IEnumerator OnDownloadingProgress(AsyncOperationHandle operation) {
    while (!operation.IsDone) { 
      yield return new WaitForEndOfFrame();
      string progressInfo = $"Downloading... ({(operation.PercentComplete * 100f).ToString("F2")}%)";
      progressText.text = progressInfo;
    }
  }

  private void OnSceneDownloaded(AsyncOperationHandle handle) {
    if (handle.Status.Equals(AsyncOperationStatus.Succeeded)) {
      Debug.Log("Scene Loaded");
    } else {
      Debug.LogError("ERROR: on Scene Load!!!");
    }
  }

}
