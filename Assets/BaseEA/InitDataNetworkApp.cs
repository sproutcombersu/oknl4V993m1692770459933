using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System;
using EaAds;
using System.Threading.Tasks;

namespace BaseEA
{
    public class InitDataNetworkApp : MonoBehaviour
    {
        public Slider sliderLoading;
        public Text textLoading;
        public string nextScene;

        public string baseUrl;

        // Use this for initialization
        private void Start()
        {
            string url =  baseUrl+"/configApp/apps?packageName=" + Application.identifier;
            GetDataLocal();
            StartCoroutine(
                EaAds.AdsUtils.GetAdConfigFromServer(
                    url,
                    (progress) => HandleProgress(progress),
                     async (adConfig, isSuccess) =>
                    {
                        if(!isSuccess) {
                            GotoNextScene();
                            return;
                        }
                        if (!adConfig.IsAppActive && isSuccess)
                        {
                            return;
                        }
                        // Debug.Log("isRedirect: "+adConfig.IsOnRedirect);
                        if (adConfig.IsOnRedirect)
                        {
                            FindAnyObjectByType<EARedirectApp>().ShowPanelUpdate(adConfig.UrlRedirect);
                            return;
                        }

                        EaAds.AdsUtils.MapConfigAd(adConfig);
                        AdsManagerWrapper.INSTANCE.Initialize((init) => { });
                        AdsManagerWrapper.INSTANCE.LoadInterstitial();
                        AdsManagerWrapper.INSTANCE.LoadRewards();

                        try
                        {
                            var setting = await Utils.GetJsonDataFromServer( baseUrl+"/json/apps?packageName="+ Application.identifier, (progress) => HandleProgress(progress));
                            ListColoringManager.instance.listColoring = setting.listColoring;
                            var mainMenu = setting.mainMenu;
                            if(!String.IsNullOrEmpty(mainMenu.urlBgMenu)) {
                                var sprite = await EAUtils.LoadSpriteFromURL(mainMenu.urlBgMenu, (progress) => HandleProgress(progress));
                                EASettingManager.instance.spriteBgMenu = sprite;
                            }
                            if(!String.IsNullOrEmpty(mainMenu.urlTitleMenu)) {
                                var sprite = await EAUtils.LoadSpriteFromURL(mainMenu.urlTitleMenu, (progress) => HandleProgress(progress));
                                EASettingManager.instance.spriteTitleMenu = sprite;
                            }
                            if(!String.IsNullOrEmpty(mainMenu.urlPaintBookMenu)) {
                                var sprite = await EAUtils.LoadSpriteFromURL(mainMenu.urlPaintBookMenu, (progress) => HandleProgress(progress));
                                EASettingManager.instance.spritePaintBookMenu = sprite;
                            }
                            if(!String.IsNullOrEmpty(mainMenu.urlColoringBookMenu)) {
                                var sprite = await EAUtils.LoadSpriteFromURL(mainMenu.urlColoringBookMenu, (progress) => HandleProgress(progress));
                                EASettingManager.instance.spriteColoringBookMenu = sprite;
                            }
                           
                            GotoNextScene();
                        }
                        catch (System.Exception e)
                        {
                            GotoNextScene();
                            Debug.LogError($"Failed to load sprite from URL: {e}");
                        }
                       
                    }
                )
            );
        }

        private void GotoNextScene()
        {
            SceneManager.LoadScene(nextScene);
        }

        private void HandleProgress(DataProgress dataProgress) {
            textLoading.text = dataProgress.progressPersen;
            sliderLoading.value = dataProgress.progressFloat;
        }

        private void GetDataLocal() {
            var setting =  Utils.GetJsonDataFromLocal();
            ListColoringManager.instance.listColoring = setting.listColoring;
        }

    }
}
