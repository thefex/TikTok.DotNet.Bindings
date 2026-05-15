using TikTokBusinessSDK;

namespace TikTokSample;

[Register("AppDelegate")]
public class AppDelegate : UIApplicationDelegate
{
    public override bool FinishedLaunching(UIApplication application, NSDictionary? launchOptions)
    {
        InitializeTikTokSDK();
        return true;
    }

    static void InitializeTikTokSDK()
    {
        // Replace these placeholder values with your real credentials from
        // https://ads.tiktok.com/marketing_api/docs?id=1739584855420929
        const string accessToken = "YOUR_ACCESS_TOKEN";
        const string appId       = "YOUR_APP_ID";
        const string tiktokAppId = "YOUR_TIKTOK_APP_ID";

        var config = TikTokConfig.ConfigWithAccessToken(accessToken, appId, tiktokAppId);
        if (config is null)
            return;

        config.TrackingEnabled          = true;
        config.AutomaticTrackingEnabled = true;

        TikTokBusiness.InitializeSdk(config, (success, error) =>
        {
            if (!success || error is not null)
            {
                Console.WriteLine($"[TikTok] SDK init failed: {error?.LocalizedDescription}");
                return;
            }

            Console.WriteLine($"[TikTok] SDK {TikTokBusiness.GetSDKVersion()} initialized.");

            TrackViewContent();
            TrackAddToCart();
            TrackCheckout();
            TrackPurchase();
            TrackCustomEvent();
        });
    }

    // Track a product view — fires when a user views a product detail page.
    static void TrackViewContent()
    {
        var content = new TikTokContentParams
        {
            ContentId       = "product_001",
            ContentName     = "Running Shoes",
            ContentCategory = "Footwear",
            Price           = NSNumber.FromDouble(99.99),
            Quantity        = 1
        };

        var @event = new TikTokViewContentEvent("vc_001");
        @event.SetContents([content]);
        @event.SetCurrency(Constants.TTCurrencyUSD);
        @event.SetValue("99.99");

        TikTokBusiness.TrackTTEvent(@event);
        Console.WriteLine("[TikTok] ViewContent tracked.");
    }

    // Track add-to-cart — fires when a user adds an item to their cart.
    static void TrackAddToCart()
    {
        var content = new TikTokContentParams
        {
            ContentId       = "product_001",
            ContentName     = "Running Shoes",
            ContentCategory = "Footwear",
            Price           = NSNumber.FromDouble(99.99),
            Quantity        = 2
        };

        var @event = new TikTokAddToCartEvent("atc_001");
        @event.SetContents([content]);
        @event.SetCurrency(Constants.TTCurrencyUSD);
        @event.SetValue("199.98");

        TikTokBusiness.TrackTTEvent(@event);
        Console.WriteLine("[TikTok] AddToCart tracked.");
    }

    // Track checkout initiation.
    static void TrackCheckout()
    {
        var content = new TikTokContentParams
        {
            ContentId   = "product_001",
            ContentName = "Running Shoes",
            Price       = NSNumber.FromDouble(99.99),
            Quantity    = 2
        };

        var @event = new TikTokCheckoutEvent("co_001");
        @event.SetContents([content]);
        @event.SetCurrency(Constants.TTCurrencyUSD);
        @event.SetValue("199.98");

        TikTokBusiness.TrackTTEvent(@event);
        Console.WriteLine("[TikTok] Checkout tracked.");
    }

    // Track a completed purchase — the most important conversion event.
    static void TrackPurchase()
    {
        var content = new TikTokContentParams
        {
            ContentId       = "product_001",
            ContentName     = "Running Shoes",
            ContentCategory = "Footwear",
            Brand           = "AcmeSport",
            Price           = NSNumber.FromDouble(99.99),
            Quantity        = 2
        };

        var @event = new TikTokPurchaseEvent("pur_001");
        @event.SetContents([content]);
        @event.SetCurrency(Constants.TTCurrencyUSD);
        @event.SetValue("199.98");

        TikTokBusiness.TrackTTEvent(@event);
        Console.WriteLine("[TikTok] Purchase tracked.");
    }

    // Track a fully custom event using TikTokBaseEvent.
    static void TrackCustomEvent()
    {
        var @event = TikTokBaseEvent.EventWithName("Tutorial_Complete");
        @event.AddPropertyWithKey("level",    new NSString("3"));
        @event.AddPropertyWithKey("duration", new NSString("120s"));

        TikTokBusiness.TrackTTEvent(@event);
        Console.WriteLine("[TikTok] Custom event Tutorial_Complete tracked.");
    }

    public override UISceneConfiguration GetConfiguration(
        UIApplication application,
        UISceneSession connectingSceneSession,
        UISceneConnectionOptions options)
    {
        return new UISceneConfiguration("Default Configuration", connectingSceneSession.Role);
    }

    public override void DidDiscardSceneSessions(
        UIApplication application,
        NSSet<UISceneSession> sceneSessions) { }
}
