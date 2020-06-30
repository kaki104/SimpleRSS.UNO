using MvvmHelpers;
using SimpleRSS.Models;
using SimpleRSS.Services;
using SimpleRSS.Shared.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel;

namespace SimpleRSS.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private IList<RSSItem> articles;

        /// <summary>
        /// 생성자
        /// </summary>
        public MainViewModel()
        {
            Init();
        }

        /// <summary>
        /// Articles
        /// </summary>
        public IList<RSSItem> Articles 
        { 
            get => articles; 
            private set => SetProperty(ref articles,value); 
        }

        private async void Init()
        {
            if(DesignMode.DesignMode2Enabled)
            {
                Articles = SampleDataService.GetRss();
            }
            else
            {
                var channel = await RssService.GetFeedAsync(new Uri("https://kaki104.tistory.com/rss"));
                if (channel == null) return;
                Articles = channel.Items;
            }
        }
    }
}
