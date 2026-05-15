using System;
using Foundation;
using ObjCRuntime;

namespace TikTokBusinessSDK {

	// @interface TikTokLogger : NSObject
	[BaseType (typeof (NSObject))]
	interface TikTokLogger {
		[Export ("setLogLevel:")]
		void SetLogLevel (TikTokLogLevel logLevel);

		[Export ("lockLogLevel")]
		void LockLogLevel ();

		[Internal]
		[Export ("verbose:", IsVariadic = true)]
		void Verbose (string message, IntPtr varArgs);

		[Internal]
		[Export ("debug:", IsVariadic = true)]
		void Debug (string message, IntPtr varArgs);

		[Internal]
		[Export ("info:", IsVariadic = true)]
		void Info (string message, IntPtr varArgs);

		[Internal]
		[Export ("warn:", IsVariadic = true)]
		void Warn (string message, IntPtr varArgs);

		[Internal]
		[Export ("warnInProduction:", IsVariadic = true)]
		void WarnInProduction (string message, IntPtr varArgs);

		[Internal]
		[Export ("error:", IsVariadic = true)]
		void Error (string message, IntPtr varArgs);

		[Internal]
		[Export ("assert:", IsVariadic = true)]
		void Assert (string message, IntPtr varArgs);
	}

	// @interface TikTokConfig : NSObject
	[BaseType (typeof (NSObject))]
	interface TikTokConfig {
		[Export ("accessToken")]
		string AccessToken { get; }

		[Export ("appId")]
		string AppId { get; }

		[Export ("tiktokAppId")]
		string TiktokAppId { get; }

		[Export ("trackingEnabled")]
		bool TrackingEnabled { get; set; }

		[Export ("automaticTrackingEnabled")]
		bool AutomaticTrackingEnabled { get; set; }

		[Export ("installTrackingEnabled")]
		bool InstallTrackingEnabled { get; set; }

		[Export ("launchTrackingEnabled")]
		bool LaunchTrackingEnabled { get; set; }

		[Export ("retentionTrackingEnabled")]
		bool RetentionTrackingEnabled { get; set; }

		[Export ("paymentTrackingStatus", ArgumentSemantic.Assign)]
		TikTokPaymentTrackStatus PaymentTrackingStatus { get; set; }

		[Export ("appTrackingDialogSuppressed")]
		bool AppTrackingDialogSuppressed { get; set; }

		[Export ("SKAdNetworkSupportEnabled")]
		bool SKAdNetworkSupportEnabled { get; set; }

		[Export ("debugModeEnabled")]
		bool DebugModeEnabled { get; set; }

		[Export ("LDUModeEnabled")]
		bool LDUModeEnabled { get; set; }

		[Export ("autoEDPEventEnabled")]
		bool AutoEDPEventEnabled { get; set; }

		[Export ("isLowPerf")]
		bool IsLowPerf { get; set; }

		[Export ("initialFlushDelay")]
		nint InitialFlushDelay { get; set; }

		[Static]
		[Export ("configWithAccessToken:appId:tiktokAppId:")]
		[return: NullAllowed]
		TikTokConfig ConfigWithAccessToken (string accessToken, string appId, string tiktokAppId);

		[Export ("disableTracking")]
		void DisableTracking ();

		[Export ("disableAutomaticTracking")]
		void DisableAutomaticTracking ();

		[Export ("disableInstallTracking")]
		void DisableInstallTracking ();

		[Export ("disableLaunchTracking")]
		void DisableLaunchTracking ();

		[Export ("disableRetentionTracking")]
		void DisableRetentionTracking ();

		[Export ("disablePaymentTracking")]
		void DisablePaymentTracking ();

		[Export ("enablePaymentTracking")]
		void EnablePaymentTracking ();

		[Export ("disableAppTrackingDialog")]
		void DisableAppTrackingDialog ();

		[Export ("disableSKAdNetworkSupport")]
		void DisableSKAdNetworkSupport ();

		[Export ("disableAutoEnhancedDataPostbackEvent")]
		void DisableAutoEnhancedDataPostbackEvent ();

		[Export ("setCustomUserAgent:")]
		void SetCustomUserAgent (string customUserAgent);

		[Export ("setLogLevel:")]
		void SetLogLevel (TikTokLogLevel logLevel);

		[Export ("setDelayForATTUserAuthorizationInSeconds:")]
		void SetDelayForATTUserAuthorizationInSeconds (nint seconds);

		[Export ("enableDebugMode")]
		void EnableDebugMode ();

		[Export ("enableLDUMode")]
		void EnableLDUMode ();

		[Export ("setIsLowPerformanceDevice:")]
		void SetIsLowPerformanceDevice (bool isLow);

		[Export ("initWithAppId:tiktokAppId:")]
		NativeHandle Constructor (string appId, string tiktokAppId);
	}

	// @interface TikTokBaseEvent : NSObject
	[BaseType (typeof (NSObject))]
	interface TikTokBaseEvent {
		[Export ("properties", ArgumentSemantic.Strong)]
		NSDictionary Properties { get; set; }

		[Export ("eventName", ArgumentSemantic.Strong)]
		string EventName { get; set; }

		[Export ("eventId", ArgumentSemantic.Strong)]
		string EventId { get; set; }

		[Export ("initWithEventName:")]
		NativeHandle Constructor (string eventName);

		[Export ("initWithEventName:eventId:")]
		NativeHandle Constructor (string eventName, [NullAllowed] string eventId);

		[Export ("initWithEventName:properties:eventId:")]
		NativeHandle Constructor (string eventName, NSDictionary properties, [NullAllowed] string eventId);

		[Static]
		[Export ("eventWithName:")]
		TikTokBaseEvent EventWithName (string eventName);

		[Export ("addPropertyWithKey:value:")]
		TikTokBaseEvent AddPropertyWithKey (string key, [NullAllowed] NSObject value);
	}

	[Static]
	partial interface Constants {
		[Field ("TTCurrencyAED", "__Internal")]
		NSString TTCurrencyAED { get; }

		[Field ("TTCurrencyARS", "__Internal")]
		NSString TTCurrencyARS { get; }

		[Field ("TTCurrencyAUD", "__Internal")]
		NSString TTCurrencyAUD { get; }

		[Field ("TTCurrencyBDT", "__Internal")]
		NSString TTCurrencyBDT { get; }

		[Field ("TTCurrencyBGN", "__Internal")]
		NSString TTCurrencyBGN { get; }

		[Field ("TTCurrencyBHD", "__Internal")]
		NSString TTCurrencyBHD { get; }

		[Field ("TTCurrencyBIF", "__Internal")]
		NSString TTCurrencyBIF { get; }

		[Field ("TTCurrencyBOB", "__Internal")]
		NSString TTCurrencyBOB { get; }

		[Field ("TTCurrencyBRL", "__Internal")]
		NSString TTCurrencyBRL { get; }

		[Field ("TTCurrencyCAD", "__Internal")]
		NSString TTCurrencyCAD { get; }

		[Field ("TTCurrencyCHF", "__Internal")]
		NSString TTCurrencyCHF { get; }

		[Field ("TTCurrencyCLP", "__Internal")]
		NSString TTCurrencyCLP { get; }

		[Field ("TTCurrencyCNY", "__Internal")]
		NSString TTCurrencyCNY { get; }

		[Field ("TTCurrencyCOP", "__Internal")]
		NSString TTCurrencyCOP { get; }

		[Field ("TTCurrencyCRC", "__Internal")]
		NSString TTCurrencyCRC { get; }

		[Field ("TTCurrencyCZK", "__Internal")]
		NSString TTCurrencyCZK { get; }

		[Field ("TTCurrencyDKK", "__Internal")]
		NSString TTCurrencyDKK { get; }

		[Field ("TTCurrencyDZD", "__Internal")]
		NSString TTCurrencyDZD { get; }

		[Field ("TTCurrencyEGP", "__Internal")]
		NSString TTCurrencyEGP { get; }

		[Field ("TTCurrencyEUR", "__Internal")]
		NSString TTCurrencyEUR { get; }

		[Field ("TTCurrencyGBP", "__Internal")]
		NSString TTCurrencyGBP { get; }

		[Field ("TTCurrencyGTQ", "__Internal")]
		NSString TTCurrencyGTQ { get; }

		[Field ("TTCurrencyHKD", "__Internal")]
		NSString TTCurrencyHKD { get; }

		[Field ("TTCurrencyHNL", "__Internal")]
		NSString TTCurrencyHNL { get; }

		[Field ("TTCurrencyHUF", "__Internal")]
		NSString TTCurrencyHUF { get; }

		[Field ("TTCurrencyIDR", "__Internal")]
		NSString TTCurrencyIDR { get; }

		[Field ("TTCurrencyILS", "__Internal")]
		NSString TTCurrencyILS { get; }

		[Field ("TTCurrencyINR", "__Internal")]
		NSString TTCurrencyINR { get; }

		[Field ("TTCurrencyIQD", "__Internal")]
		NSString TTCurrencyIQD { get; }

		[Field ("TTCurrencyISK", "__Internal")]
		NSString TTCurrencyISK { get; }

		[Field ("TTCurrencyJOD", "__Internal")]
		NSString TTCurrencyJOD { get; }

		[Field ("TTCurrencyJPY", "__Internal")]
		NSString TTCurrencyJPY { get; }

		[Field ("TTCurrencyKES", "__Internal")]
		NSString TTCurrencyKES { get; }

		[Field ("TTCurrencyKHR", "__Internal")]
		NSString TTCurrencyKHR { get; }

		[Field ("TTCurrencyKRW", "__Internal")]
		NSString TTCurrencyKRW { get; }

		[Field ("TTCurrencyKWD", "__Internal")]
		NSString TTCurrencyKWD { get; }

		[Field ("TTCurrencyKZT", "__Internal")]
		NSString TTCurrencyKZT { get; }

		[Field ("TTCurrencyLBP", "__Internal")]
		NSString TTCurrencyLBP { get; }

		[Field ("TTCurrencyMAD", "__Internal")]
		NSString TTCurrencyMAD { get; }

		[Field ("TTCurrencyMOP", "__Internal")]
		NSString TTCurrencyMOP { get; }

		[Field ("TTCurrencyMXN", "__Internal")]
		NSString TTCurrencyMXN { get; }

		[Field ("TTCurrencyMYR", "__Internal")]
		NSString TTCurrencyMYR { get; }

		[Field ("TTCurrencyNGN", "__Internal")]
		NSString TTCurrencyNGN { get; }

		[Field ("TTCurrencyNIO", "__Internal")]
		NSString TTCurrencyNIO { get; }

		[Field ("TTCurrencyNOK", "__Internal")]
		NSString TTCurrencyNOK { get; }

		[Field ("TTCurrencyNZD", "__Internal")]
		NSString TTCurrencyNZD { get; }

		[Field ("TTCurrencyOMR", "__Internal")]
		NSString TTCurrencyOMR { get; }

		[Field ("TTCurrencyPEN", "__Internal")]
		NSString TTCurrencyPEN { get; }

		[Field ("TTCurrencyPHP", "__Internal")]
		NSString TTCurrencyPHP { get; }

		[Field ("TTCurrencyPKR", "__Internal")]
		NSString TTCurrencyPKR { get; }

		[Field ("TTCurrencyPLN", "__Internal")]
		NSString TTCurrencyPLN { get; }

		[Field ("TTCurrencyPYG", "__Internal")]
		NSString TTCurrencyPYG { get; }

		[Field ("TTCurrencyQAR", "__Internal")]
		NSString TTCurrencyQAR { get; }

		[Field ("TTCurrencyRON", "__Internal")]
		NSString TTCurrencyRON { get; }

		[Field ("TTCurrencyRUB", "__Internal")]
		NSString TTCurrencyRUB { get; }

		[Field ("TTCurrencySAR", "__Internal")]
		NSString TTCurrencySAR { get; }

		[Field ("TTCurrencySEK", "__Internal")]
		NSString TTCurrencySEK { get; }

		[Field ("TTCurrencySGD", "__Internal")]
		NSString TTCurrencySGD { get; }

		[Field ("TTCurrencyTHB", "__Internal")]
		NSString TTCurrencyTHB { get; }

		[Field ("TTCurrencyTRY", "__Internal")]
		NSString TTCurrencyTRY { get; }

		[Field ("TTCurrencyTWD", "__Internal")]
		NSString TTCurrencyTWD { get; }

		[Field ("TTCurrencyTZS", "__Internal")]
		NSString TTCurrencyTZS { get; }

		[Field ("TTCurrencyUAH", "__Internal")]
		NSString TTCurrencyUAH { get; }

		[Field ("TTCurrencyUSD", "__Internal")]
		NSString TTCurrencyUSD { get; }

		[Field ("TTCurrencyVES", "__Internal")]
		NSString TTCurrencyVES { get; }

		[Field ("TTCurrencyVND", "__Internal")]
		NSString TTCurrencyVND { get; }

		[Field ("TTCurrencyZAR", "__Internal")]
		NSString TTCurrencyZAR { get; }

		[Field ("TTEventNameAchieveLevel", "__Internal")]
		NSString TTEventNameAchieveLevel { get; }

		[Field ("TTEventNameAddPaymentInfo", "__Internal")]
		NSString TTEventNameAddPaymentInfo { get; }

		[Field ("TTEventNameCompleteTutorial", "__Internal")]
		NSString TTEventNameCompleteTutorial { get; }

		[Field ("TTEventNameCreateGroup", "__Internal")]
		NSString TTEventNameCreateGroup { get; }

		[Field ("TTEventNameCreateRole", "__Internal")]
		NSString TTEventNameCreateRole { get; }

		[Field ("TTEventNameGenerateLead", "__Internal")]
		NSString TTEventNameGenerateLead { get; }

		[Field ("TTEventNameImpressionLevelAdRevenue", "__Internal")]
		NSString TTEventNameImpressionLevelAdRevenue { get; }

		[Field ("TTEventNameInAppADClick", "__Internal")]
		NSString TTEventNameInAppADClick { get; }

		[Field ("TTEventNameInAppADImpr", "__Internal")]
		NSString TTEventNameInAppADImpr { get; }

		[Field ("TTEventNameInstallApp", "__Internal")]
		NSString TTEventNameInstallApp { get; }

		[Field ("TTEventNameJoinGroup", "__Internal")]
		NSString TTEventNameJoinGroup { get; }

		[Field ("TTEventNameLaunchAPP", "__Internal")]
		NSString TTEventNameLaunchAPP { get; }

		[Field ("TTEventNameLoanApplication", "__Internal")]
		NSString TTEventNameLoanApplication { get; }

		[Field ("TTEventNameLoanApproval", "__Internal")]
		NSString TTEventNameLoanApproval { get; }

		[Field ("TTEventNameLoanDisbursal", "__Internal")]
		NSString TTEventNameLoanDisbursal { get; }

		[Field ("TTEventNameLogin", "__Internal")]
		NSString TTEventNameLogin { get; }

		[Field ("TTEventNameRate", "__Internal")]
		NSString TTEventNameRate { get; }

		[Field ("TTEventNameRegistration", "__Internal")]
		NSString TTEventNameRegistration { get; }

		[Field ("TTEventNameSearch", "__Internal")]
		NSString TTEventNameSearch { get; }

		[Field ("TTEventNameSpendCredits", "__Internal")]
		NSString TTEventNameSpendCredits { get; }

		[Field ("TTEventNameStartTrial", "__Internal")]
		NSString TTEventNameStartTrial { get; }

		[Field ("TTEventNameSubscribe", "__Internal")]
		NSString TTEventNameSubscribe { get; }

		[Field ("TTEventNameUnlockAchievement", "__Internal")]
		NSString TTEventNameUnlockAchievement { get; }

		[Field ("TTAccumulatedSKANValuesKey", "__Internal")]
		NSString TTAccumulatedSKANValuesKey { get; }

		[Field ("TTLatestFineValueKey", "__Internal")]
		NSString TTLatestFineValueKey { get; }

		[Field ("TTLatestCoarseValueKey", "__Internal")]
		NSString TTLatestCoarseValueKey { get; }

		[Field ("TTSKANTimeWindowKey", "__Internal")]
		NSString TTSKANTimeWindowKey { get; }
	}

	// @interface TikTokConstants : NSObject
	[BaseType (typeof (NSObject))]
	interface TikTokConstants {
	}

	// @interface TikTokBusiness : NSObject
	[BaseType (typeof (NSObject))]
	interface TikTokBusiness {
		[Export ("userTrackingEnabled")]
		bool UserTrackingEnabled { get; set; }

		[Export ("isRemoteSwitchOn")]
		bool IsRemoteSwitchOn { get; set; }

		[Export ("isGlobalConfigFetched")]
		bool IsGlobalConfigFetched { get; set; }

		[Export ("accessToken")]
		string AccessToken { get; set; }

		[Export ("anonymousID")]
		string AnonymousID { get; set; }

		[Export ("isDebugMode")]
		bool IsDebugMode { get; }

		[Static]
		[Export ("initializeSdk:")]
		void InitializeSdk ([NullAllowed] TikTokConfig tiktokConfig);

		[Static]
		[Export ("initializeSdk:completionHandler:")]
		void InitializeSdk ([NullAllowed] TikTokConfig tiktokConfig, Action<bool, NSError> completionHandler);

		[Static]
		[Export ("trackEvent:")]
		void TrackEvent (string eventName);

		[Static]
		[Export ("trackEvent:withProperties:")]
		void TrackEvent (string eventName, NSDictionary properties);

		[Static]
		[Export ("trackEvent:withType:")]
		void TrackEventWithType (string eventName, string type);

		[Static]
		[Export ("trackEvent:withId:")]
		void TrackEventWithId (string eventName, string eventId);

		[Static]
		[Export ("trackTTEvent:")]
		void TrackTTEvent (TikTokBaseEvent @event);

		[Static]
		[Export ("setTrackingEnabled:")]
		void SetTrackingEnabled (bool enabled);

		[Static]
		[Export ("setCustomUserAgent:")]
		void SetCustomUserAgent (string customUserAgent);

		[Static]
		[Export ("identifyWithExternalID:externalUserName:phoneNumber:email:")]
		void IdentifyWithExternalID ([NullAllowed] string externalID, [NullAllowed] string externalUserName, [NullAllowed] string phoneNumber, [NullAllowed] string email);

		[Static]
		[Export ("logout")]
		void Logout ();

		[Static]
		[Export ("explicitlyFlush")]
		void ExplicitlyFlush ();

		[Static]
		[Export ("updateAccessToken:")]
		void UpdateAccessToken (string accessToken);

		[Static]
		[Export ("isTrackingEnabled")]
		bool IsTrackingEnabled ();

		[Static]
		[Export ("isUserTrackingEnabled")]
		bool IsUserTrackingEnabled ();

		[Static]
		[Export ("idfa")]
		[return: NullAllowed]
		string Idfa ();

		[Static]
		[Export ("appInForeground")]
		bool AppInForeground ();

		[Static]
		[Export ("appInBackground")]
		bool AppInBackground ();

		[Static]
		[Export ("appIsInactive")]
		bool AppIsInactive ();

		[Static]
		[Export ("requestTrackingAuthorizationWithCompletionHandler:")]
		void RequestTrackingAuthorizationWithCompletionHandler ([NullAllowed] Action<nuint> completion);

		[Static]
		[Export ("isDebugMode")]
		bool IsDebugModeEnabled ();

		[Static]
		[Export ("isLDUMode")]
		bool IsLDUMode ();

		[Static]
		[Export ("isInitialized")]
		bool IsInitialized ();

		[Static]
		[Export ("getInstance")]
		TikTokBusiness GetInstance ();

		[Static]
		[Export ("resetInstance")]
		void ResetInstance ();

		[Static]
		[Export ("getTestEventCode")]
		string GetTestEventCode ();

		[Static]
		[Export ("produceFatalError")]
		void ProduceFatalError ();

		[Static]
		[Export ("getSDKVersion")]
		string GetSDKVersion ();

		[Static]
		[Export ("fetchDeferredDeeplinkWithCompletion:")]
		void FetchDeferredDeeplinkWithCompletion (Action<NSUrl, NSError> completion);

		[Static]
		[Export ("paramForApmConfig:")]
		void ParamForApmConfig (NSNotification noti);
	}

	// @interface TikTokContentParams : NSObject
	[BaseType (typeof (NSObject))]
	interface TikTokContentParams {
		[Export ("price", ArgumentSemantic.Strong)]
		NSNumber Price { get; set; }

		[Export ("quantity")]
		nint Quantity { get; set; }

		[NullAllowed, Export ("contentId", ArgumentSemantic.Strong)]
		string ContentId { get; set; }

		[NullAllowed, Export ("contentCategory", ArgumentSemantic.Strong)]
		string ContentCategory { get; set; }

		[NullAllowed, Export ("contentName", ArgumentSemantic.Strong)]
		string ContentName { get; set; }

		[NullAllowed, Export ("brand", ArgumentSemantic.Strong)]
		string Brand { get; set; }

		[Export ("dictionaryValue")]
		NSDictionary GetDictionaryValue ();
	}

	// @interface TikTokContentsEvent : TikTokBaseEvent
	[BaseType (typeof (TikTokBaseEvent))]
	interface TikTokContentsEvent {
		[Export ("setDescription:")]
		void SetDescription (string description);

		[Export ("setCurrency:")]
		void SetCurrency (string currency);

		[Export ("setValue:")]
		void SetValue (string value);

		[Export ("setContentType:")]
		void SetContentType (string contentType);

		[Export ("setContentId:")]
		void SetContentId (string contentId);

		[Export ("setContents:")]
		void SetContents (TikTokContentParams [] contents);
	}

	// @interface TikTokAddToCartEvent : TikTokContentsEvent
	[BaseType (typeof (TikTokContentsEvent))]
	interface TikTokAddToCartEvent {
		[Export ("initWithEventId:")]
		NativeHandle Constructor (string eventId);
	}

	// @interface TikTokAddToWishlistEvent : TikTokContentsEvent
	[BaseType (typeof (TikTokContentsEvent))]
	interface TikTokAddToWishlistEvent {
		[Export ("initWithEventId:")]
		NativeHandle Constructor (string eventId);
	}

	// @interface TikTokCheckoutEvent : TikTokContentsEvent
	[BaseType (typeof (TikTokContentsEvent))]
	interface TikTokCheckoutEvent {
		[Export ("initWithEventId:")]
		NativeHandle Constructor (string eventId);
	}

	// @interface TikTokPurchaseEvent : TikTokContentsEvent
	[BaseType (typeof (TikTokContentsEvent))]
	interface TikTokPurchaseEvent {
		[Export ("initWithEventId:")]
		NativeHandle Constructor (string eventId);
	}

	// @interface TikTokViewContentEvent : TikTokContentsEvent
	[BaseType (typeof (TikTokContentsEvent))]
	interface TikTokViewContentEvent {
		[Export ("initWithEventId:")]
		NativeHandle Constructor (string eventId);
	}

	// @interface TikTokAdRevenueEvent : TikTokBaseEvent
	[BaseType (typeof (TikTokBaseEvent))]
	interface TikTokAdRevenueEvent {
		[Export ("initWithAdRevenue:eventId:")]
		NativeHandle Constructor (NSDictionary adRevenue, string eventId);
	}

	// @interface TikTokDeviceInfo : NSObject
	[BaseType (typeof (NSObject))]
	interface TikTokDeviceInfo {
		[Export ("appId")]
		string AppId { get; set; }

		[Export ("appName")]
		string AppName { get; set; }

		[Export ("appNamespace")]
		string AppNamespace { get; set; }

		[Export ("appVersion")]
		string AppVersion { get; set; }

		[Export ("appBuild")]
		string AppBuild { get; set; }

		[Export ("devicePlatform")]
		string DevicePlatform { get; set; }

		[Export ("deviceIdForAdvertisers")]
		string DeviceIdForAdvertisers { get; set; }

		[Export ("deviceVendorId")]
		string DeviceVendorId { get; set; }

		[Export ("localeInfo")]
		string LocaleInfo { get; set; }

		[Export ("ipInfo")]
		string IpInfo { get; set; }

		[Export ("trackingEnabled")]
		bool TrackingEnabled { get; set; }

		[Export ("clientSdk")]
		string ClientSdk { get; set; }

		[Export ("deviceName")]
		string DeviceName { get; set; }

		[Export ("systemVersion")]
		string SystemVersion { get; set; }

		[Static]
		[Export ("deviceInfo")]
		TikTokDeviceInfo GetDeviceInfo ();

		[Export ("updateIdentifier")]
		void UpdateIdentifier ();

		[Export ("getUserAgent")]
		string GetUserAgent ();

		[Export ("fallbackUserAgent")]
		string GetFallbackUserAgent ();
	}

	// @interface TikTokBusinessSDKAddress : NSObject
	[BaseType (typeof (NSObject))]
	interface TikTokBusinessSDKAddress {
		[Static]
		[Export ("beginAddress")]
		long BeginAddress ();

		[Static]
		[Export ("endAddress")]
		long EndAddress ();
	}

}
