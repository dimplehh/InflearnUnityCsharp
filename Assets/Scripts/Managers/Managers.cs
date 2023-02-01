using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance{ get { init(); return s_instance;} } //인스턴스?
    #region Contents
    GameManager _game = new GameManager();
    public static GameManager Game { get { return Instance._game; } }
    #endregion
    #region Core
    DataManager _data = new DataManager();
    InputManagers _input = new InputManagers();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();
    
    public static DataManager Data { get { return Instance._data; } }
    public static InputManagers Input { get { return Instance._input; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } } //인스턴스?
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();
    }

    static void init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {
                go = new GameObject { };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._data.Init();
            s_instance._pool.Init();
            s_instance._sound.Init();
        }
    }

    public static void Clear()
    {
        //data는 scene이 바뀌더라도 클리어할 필요 없고 안하는게 좋기때문에 clear x
        Input.Clear();
        Sound.Clear();
        UI.Clear();
        Scene.Clear();
        Pool.Clear();
    }
}
