using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Echo.Common;

namespace Echo.Android
{
    [Activity(Label = "Echo.Android"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        private Game1 game;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            game = new Game1(new ContentLoader(Assets), true);
            SetViewFullScreen();

            game.Run();
        }

        public override void OnBackPressed()
        {
            MoveTaskToBack(true);
        }

        private void SetViewFullScreen()
        {
            var view = (View)game.Services.GetService(typeof(View));

            view.SystemUiVisibility = (StatusBarVisibility)
                (SystemUiFlags.LayoutStable
                | SystemUiFlags.LayoutHideNavigation
                | SystemUiFlags.LayoutFullscreen
                | SystemUiFlags.HideNavigation
                | SystemUiFlags.Fullscreen
                | SystemUiFlags.ImmersiveSticky
                );

            if (Build.VERSION.SdkInt >= BuildVersionCodes.P)
                Window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;

            Window.AddFlags(WindowManagerFlags.KeepScreenOn);

            SetContentView(view);
        }

        protected override void OnResume()
        {
            base.OnResume();
            SetViewFullScreen();
            if (!isAppInLockTaskMode())
            {
                //21
                StartLockTask();
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            if (isAppInLockTaskMode())
            {
                StopLockTask();
                MoveTaskToBack(true);
            }
        }


        public bool isAppInLockTaskMode()
        {
            ActivityManager activityManager;

            activityManager = (ActivityManager)
                GetSystemService(Context.ActivityService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                // For SDK version 23 and above.
                return activityManager.LockTaskModeState
                    != LockTaskMode.None;
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                // When SDK version >= 21. This API is deprecated in 23.
#pragma warning disable CS0618 // Type or member is obsolete
                return activityManager.IsInLockTaskMode;
#pragma warning restore CS0618 // Type or member is obsolete
            }

            return false;
        }
        protected override void OnDestroy()
        {
            StopLockTask();
            base.OnDestroy();
        }
    }
}

