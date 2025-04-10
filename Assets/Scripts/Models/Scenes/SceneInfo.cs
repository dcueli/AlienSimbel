using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInfo {
	private string Name;
	private string Path;
	private int Index;
	public string name {
    get => Name;
    set => Name = value;
  }
	public string path {
    get => Path;
    set => Path = value;
  }
	public int index {
    get => Index;
    set => Index = value;
  }

  public SceneInfo(Scene pCurrScene, string pName = null, string pPath = null, int pIndex = 0) {
    Set(pCurrScene);

    if (!!!string.IsNullOrEmpty(pName) && !!!string.IsNullOrWhiteSpace(pName))
      name = pName;
    if (!!!string.IsNullOrEmpty(pPath) && !!!string.IsNullOrWhiteSpace(pPath))
      path = pPath;
    if (0 < pIndex)
      index = pIndex;
  }

  public void Set(Scene pCurrScene) {
    if (null == pCurrScene) return;

    index = pCurrScene.buildIndex;
    name = pCurrScene.name;
    path = pCurrScene.path;
  }

  public override string ToString() {
    string message = 
      $"Name: {name}\r\n" + 
      $"Path: {path}\r\n" + 
      $"Index:\r\n\t{index}";

    return message;
    // Serialize the current object
    // return JsonUtility.ToJson(this, true);
  }
}