﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Webkit;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace ComciganViewer.Activities
{
    [Activity(MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private DrawerLayout drawerLayout;
        private NavigationView navigationView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = GetString(Resource.String.Timetable);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            var drawerToggle = new ActionBarDrawerToggle
                (this, drawerLayout, toolbar, Resource.String.nav_open, Resource.String.nav_close);
            drawerLayout.AddDrawerListener(drawerToggle);
            drawerToggle.SyncState();
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

            WebView web_view = FindViewById<WebView>(Resource.Id.main_webview);
            web_view.SetWebViewClient(new ComciganWebViewClient());
            web_view.LoadUrl("http://112.186.146.96:4080/");
            web_view.Settings.JavaScriptEnabled = true;
            web_view.SetScrollContainer(true);
            web_view.Settings.DomStorageEnabled = true;
        }

        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            var menuItem = e.MenuItem;
            //menuItem.SetChecked(!menuItem.IsChecked);
            menuItem.SetCheckable(false);
            Intent intent;
            switch (menuItem.ItemId)
            {
                case Resource.Id.nav_aboutMe:
                    intent = new Intent(this, typeof(AboutMeActivity));
                    StartActivity(intent);
                    break;
            }
            drawerLayout.CloseDrawers();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            navigationView.InflateMenu(Resource.Menu.nav_menu);
            return true;
        }
    }
}