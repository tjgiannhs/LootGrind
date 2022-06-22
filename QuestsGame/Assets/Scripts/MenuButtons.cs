using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    SoundFX soundFX;
    MenuBehavior menuBehaviorComponent;

    private void Start()
    {
        soundFX = gameManager.GetComponent<SoundFX>();
        menuBehaviorComponent = transform.parent.GetComponent<MenuBehavior>();
    }

    public void onPlayButtonClicked() { PlayerPrefs.SetInt("Game Started",1); soundFX.playMenuButtonDown(); menuBehaviorComponent.unloadMenu(); }
    public void onCreditsButtonClicked() { soundFX.playMenuButtonDown(); menuBehaviorComponent.showCreditsCanvas(); }
    public void onHowToPlayButtonClicked() { soundFX.playMenuButtonDown(); menuBehaviorComponent.showHowToPlayCanvas(); }
    public void onAchievementsButtonClicked() { soundFX.playMenuButtonDown(); menuBehaviorComponent.showAchievementsCanvas(); }
    public void onSettingsButtonClicked() { soundFX.playMenuButtonDown(); menuBehaviorComponent.showSettingsCanvas(); }
    public void OnTwitterLinkClicked() { soundFX.playLinkButtonDown(); Application.OpenURL("https://twitter.com/gggamesdev"); sendLinkAnalytics("twitter"); }
    public void OnItchioLinkClicked() { soundFX.playLinkButtonDown(); Application.OpenURL("https://gg-undroid-games.itch.io"); sendLinkAnalytics("itch"); }
    public void OnGooglePlayLinkClicked() { soundFX.playLinkButtonDown(); Application.OpenURL("https://play.google.com/store/apps/dev?id=5698010272073934079"); sendLinkAnalytics("google play"); }
    public void OnPayPalMeLinkClicked() { soundFX.playLinkButtonDown(); Application.OpenURL("https://www.paypal.me"); sendLinkAnalytics("paypal"); }
    public void OnKoffiLinkClicked() { soundFX.playLinkButtonDown(); Application.OpenURL("https://ko-fi.com/ggundroidgames"); sendLinkAnalytics("ko-fi"); }
    public void OnLinktreeClicked() { soundFX.playLinkButtonDown(); Application.OpenURL("https://linktr.ee/ggundroidgames"); sendLinkAnalytics("linktree"); }

    void sendLinkAnalytics(string linkButton) 
    {
        Analytics.CustomEvent("Link pressed", new Dictionary<string, object>
            {
                { "link", linkButton},
                { "OS and Version", SystemInfo.operatingSystem}
            });
    }


}
