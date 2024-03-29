﻿using GameCommunity.ViewModels;
using GameCommunity.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GameCommunity
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(HomeStayDetailPage), typeof(HomeStayDetailPage));
        }

    }
}
