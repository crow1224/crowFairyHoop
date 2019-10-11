using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


namespace QMG
{

	/// <summary>
	/// 游戏初始化时调用的类,基本上就是Unity加载第一个场景时会被调用一次的方法
	/// </summary>
	[DisallowMultipleComponent]
	public sealed class IGExporter : MonoBehaviour
	{
        public static string globalGoName = "__IGEXPORTER__";

        public static System.Action shareAsync_Callback = null;

        public static System.Action ShowInterstitialAd_Preload_Method = null;

        public static System.Action ShowRewaredVideoAd_Preload_Method = null;
        public static System.Action ShowRewaredVideoAd_Complete_Callback = null;
        public static System.Action<FBError> ShowRewaredVideoAd_Error_Callback = null;

        public static System.Action<bool> PreloadRewaredVideoAd_Ready_Callback = null;

        /// <summary>
        /// 首个场景开始加载前调用此方法
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void IGExporterGameStart()
		{
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                Debug.LogWarning("IGExporter|start|return|direct");
                return;
            }

            Debug.Log("IGExporter|start");

			GameObject go = new GameObject();
			go.name = IGExporter.globalGoName;
			DontDestroyOnLoad(go);

			go.AddComponent<IGExporter>();

            Debug.Log("IGExporter|end");
        }

		/// <summary>
		/// 首个场景完成加载后调用此方法
		/// </summary>
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void IGExporterGameEnd()
		{
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                Debug.LogWarning("IGExporter|start|return|direct");
                return;
            }

            Debug.Log("IGExporter|start");

            Debug.Log("IGExporter|end");
        }

        #region Common Promise Callback
        private void Promise_on_context_chooseAsync()
        {
            Debug.Log("Promise_on_context_chooseAsync");

            if (GameContext.chooseAsync_Callback != null)
            {
                GameContext.chooseAsync_Callback();
            }
        }

        private void Promise_on_leaderboard_setScoreAsync()
        {
            Debug.Log("Promise_on_leaderboard_setScoreAsync");

            if (FBLeaderboard.setScoreAsync_Callback != null)
            {
                FBLeaderboard.setScoreAsync_Callback();
            }
        }

        private void Promise_on_leaderboard_getPlayerEntryAsync(string jsonStr)
        {
            Debug.Log("Promise_on_leaderboard_getPlayerEntryAsync");
            if (FBLeaderboard.getPlayerEntryAsync_Callback != null)
            {   
                FBLeaderboardEntry entry = SimpleJson.SimpleJson.DeserializeObject<FBLeaderboardEntry>(jsonStr);
                FBLeaderboard.getPlayerEntryAsync_Callback(entry);
            }
        }

        private void Promise_on_leaderboard_getEntriesAsync(string jsonStr)
        {
            Debug.Log("Promise_on_leaderboard_getEntriesAsync");
            if (FBLeaderboard.getEntriesAsync_Callback != null)
            {
                FBLeaderboardEntry[] entries = SimpleJson.SimpleJson.DeserializeObject<FBLeaderboardEntry[]>(jsonStr);
                FBLeaderboard.getEntriesAsync_Callback(entries);
            }
        }

        private void Promise_on_leaderboard_getConnectedPlayerEntriesAsync(string jsonStr)
        {
            Debug.Log("Promise_on_leaderboard_getConnectedPlayerEntriesAsync");
            if (FBLeaderboard.getConnectedPlayerEntriesAsync_Callback != null)
            {
                FBLeaderboardEntry[] entries = SimpleJson.SimpleJson.DeserializeObject<FBLeaderboardEntry[]>(jsonStr);
                FBLeaderboard.getConnectedPlayerEntriesAsync_Callback(entries);
            }
        }

        private void Promise_on_player_getDataAsync(string jsonStr)
        {
            Debug.Log("Promise_on_player_getDataAsync");
            Dictionary<string, object> data = SimpleJson.SimpleJson.DeserializeObject<Dictionary<string, object>>(jsonStr);
            if (ContextPlayer.getDataAsync_Callback != null)
            {
                ContextPlayer.getDataAsync_Callback(data);
            }
        }

        private void Promise_on_player_setDataAsync()
        {
            Debug.Log("Promise_on_player_setDataAsync");
            if (ContextPlayer.setDataAsync_Callback != null)
            {
                ContextPlayer.setDataAsync_Callback();
            }
        }

        private void Promise_on_player_canSubscribeBotAsync(string jsonStr)
        {
            Debug.Log("Promise_on_player_canSubscribeBotAsync");
            bool can_subscribe = SimpleJson.SimpleJson.DeserializeObject<bool>(jsonStr);
            if (ContextPlayer.canSubscribeBotAsync_Callback != null)
            {
                ContextPlayer.canSubscribeBotAsync_Callback(can_subscribe);
            }
        }

        private void Promise_on_player_subscribeBotAsync_Success()
        {
            Debug.Log("Promise_on_player_subscribeBotAsync_Success");
            if (ContextPlayer.subscribeBot_Success_Callback != null)
            {
                ContextPlayer.subscribeBot_Success_Callback();
            }
        }

        private void Promise_on_player_subscribeBotAsync_Error(string errJsonStr)
        {
            Debug.Log("Promise_on_player_subscribeBotAsync_Error");
            if (ContextPlayer.subscribeBot_Error_Callback != null)
            {
                FBError err = SimpleJson.SimpleJson.DeserializeObject<FBError>(errJsonStr);
                ContextPlayer.subscribeBot_Error_Callback(err);
            }
        }

        private void Promise_on_fbinstant_shareAsync()
        {
            Debug.Log("Promise_on_fbinstant_shareAsync");
            if (shareAsync_Callback != null)
            {
                shareAsync_Callback();
            }
        }

        private void Promise_on_fbinstant_showInterstitialAdAsync()
        {
            Debug.Log("Promise_on_fbinstant_showInterstitialAdAsync");
            if (ShowInterstitialAd_Preload_Method != null)
            {
                ShowInterstitialAd_Preload_Method();
            }
        }

        private void Promise_on_fbinstant_showRewardedVideoAsync_Complete()
        {
            Debug.Log("Promise_on_fbinstant_showRewardedVideoAsync_Complete");
            if (ShowRewaredVideoAd_Complete_Callback != null)
            {
                ShowRewaredVideoAd_Complete_Callback();
            }
        }

        private void Promise_on_fbinstant_showRewardedVideoAsync_Error(string errJsonStr)
        {
            Debug.Log("Promise_on_fbinstant_showRewardedVideoAsync_Error");
            if (ShowRewaredVideoAd_Error_Callback != null)
            {
                FBError err = SimpleJson.SimpleJson.DeserializeObject<FBError>(errJsonStr);
                ShowRewaredVideoAd_Error_Callback(err);
            }
        }

        private void Promise_on_fbinstant_showRewardedVideoAsync_Preload()
        {
            Debug.Log("Promise_on_fbinstant_showRewardedVideoAsync_Preload");
            if (ShowRewaredVideoAd_Preload_Method != null)
            {
                ShowRewaredVideoAd_Preload_Method();
            }
        }

        private void Promise_on_fbinstant_getRewardedVideoAsync(string jsonStr)
        {
            Debug.Log("Promise_on_fbinstant_getRewardedVideoAsync");
            bool isReady = SimpleJson.SimpleJson.DeserializeObject<bool>(jsonStr);
            if (PreloadRewaredVideoAd_Ready_Callback != null)
            {
                PreloadRewaredVideoAd_Ready_Callback(isReady);
            }
        }
        #endregion

        /// <summary>
        /// 在WebGL的Facebook Instant测试中发现,加载的时候,已经把首个场景加载进来的了,所以这里写一个方法用于在startGameAsync触发游戏逻辑,对于非WebGL的,应该要判断
        /// </summary>
        void StartGameLogic()
        {
            Debug.Log("StartGameLogic|start");
            // CUSTOM CODE HERE START
            this.gameObject.AddComponent<TestFB>(); //Game Logic Entry
            // CUSTOM CODE HERE END
            Debug.Log("StartGameLogic|end");
        }

	}

}
