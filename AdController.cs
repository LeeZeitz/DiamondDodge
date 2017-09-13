using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class AdController : MonoBehaviour {

	public static AdController ad;
	public static BannerView bannerView;
	private static float timer;
	private bool firstAd;

	private string adUnitId = "ca-app-pub-9063283071383410/8572973887";
	private string testDeviceLee = "C85B8C44A078D6ED81774803DE19F244";
	private string testDeviceMichael = "9FE57FAA4493794B187631D5C0BC8914";
		

	void Awake()
	{
		if (ad == null) {
			DontDestroyOnLoad (gameObject);
			ad = this;
		}
		else if (ad != this) 
		{
			Destroy (gameObject);
		}
	}


	void Start()
	{
		firstAd = true;
		timer = 0;
	}


	void Update()
	{
		timer += Time.deltaTime;
	}


	public void RequestBannerAd (int position)
	{
		if (firstAd || timer > 60)
		{
			firstAd = false;
			
			// Creates request for ad and add test devices to avoid false impressions
			AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice(testDeviceLee).AddTestDevice(testDeviceMichael).Build();

			// Creates banner to be filled by ad
			if (position == 0)
			{
				bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
			}
			else if (position == 1)
			{
				bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
			}

			// Called when an ad request has successfully loaded.
			bannerView.OnAdLoaded += HandleOnAdLoaded;

			// Called when an ad is clicked.
			bannerView.OnAdOpening += HandleOnAdOpening;

			/*
			// Called when an ad request failed to load.
			bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
			// Called when the user returned from the app after an ad click.
			bannerView.OnAdClosed += HandleOnAdClosed;
			// Called when the ad click caused the user to leave the application.
			bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;
			*/

			// Load request into banner
			bannerView.LoadAd(request);
		}

		else
		{
			bannerView.Show();
		}
	}


	public void HideBannerAd ()
	{
		bannerView.Hide();
	}


	// What to do when an ad loads
	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		print("OnAdLoaded event received.");
		// Handle the ad loaded event.
	}

	// What to do when an ad is clicked
	public void HandleOnAdOpening(object sender, EventArgs args)
	{
		print("OnAdOpened event received.");
	}




}
