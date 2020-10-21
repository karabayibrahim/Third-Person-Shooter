using UnityEngine;
using System.Collections;
using System;
using GoogleMobileAds.Api;
using GoogleMobileAds;

public class ReklamInterstitial : MonoBehaviour
{
	private InterstitialAd reklamObjesi;

	public static ReklamInterstitial instance;

	public static bool showAd = false;


	void Awake(){
		if (instance == null) {

			instance = this;
			DontDestroyOnLoad(gameObject.transform);


		}
		else if(this !=instance){
			Destroy(gameObject);

		}

	}


	void Update(){

		if(showAd){
			ReklamAc(true);
			showAd = false;
		}

	}


	void Start()
	{
        //MobileAds.Initialize("ca-app-pub-2973695865485776~6726039731");
        reklamObjesi = new InterstitialAd("ca-app-pub-4294593317187584/4149574484");
		reklamObjesi.OnAdClosed += YeniReklamAl;
		YeniReklamAl( null, null );
        
    }

	public void ReklamAc (bool goster)
	{
		if(goster) {
			Debug.Log("Çalıştı");
			StartCoroutine( "ReklamiGoster" );
			goster = false;
		}
	}

	IEnumerator ReklamiGoster()
	{
		while( !reklamObjesi.IsLoaded() )
			yield return null;

		reklamObjesi.Show();
	}
	


	public void YeniReklamAl( object sender, EventArgs args )
	{
		AdRequest reklamiAl = new AdRequest.Builder().Build();
		reklamObjesi.LoadAd( reklamiAl );
	}
}

