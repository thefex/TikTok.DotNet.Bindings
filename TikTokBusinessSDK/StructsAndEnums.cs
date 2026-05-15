using ObjCRuntime;

namespace TikTokBusinessSDK {

	public enum TikTokLogLevel : uint {
		Verbose = 1,
		Debug = 2,
		Info = 3,
		Warn = 4,
		Error = 5,
		Assert = 6,
		Suppress = 7
	}

	[Native]
	public enum TikTokPaymentTrackStatus : long {
		Default = 0,
		Enabled = 1,
		Disabled = 2
	}

}
