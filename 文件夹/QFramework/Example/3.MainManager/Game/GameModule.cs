using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameModule : MainManager
    {
        public static void LoadModule()
        {
            SceneManager.LoadScene("Game");
        }

        protected override void LaunchInDevelopingMode()
        {
            // 加载资源
            // 初始化 SDK
            // 点击开始游戏
            Debug.Log("developing mode");
        }

        protected override void LaunchInTestMode()
        {
            Debug.Log("test mode");
        }

        protected override void LaunchInProductionMode()
        {
            Debug.Log("production mode");
        }
    }
}