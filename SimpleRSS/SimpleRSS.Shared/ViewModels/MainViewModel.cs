using MvvmHelpers;
using MvvmHelpers.Commands;
using SimpleRSS.Models;
using SimpleRSS.Services;
using SimpleRSS.Shared.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;

namespace SimpleRSS.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private IList<RSSItem> _articles;

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy ,value); }
        }

        public ICommand SelectionChangedCommand { get; set; }

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
            get => _articles; 
            private set => SetProperty(ref _articles,value); 
        }

        private async void Init()
        {
            SelectionChangedCommand = new AsyncCommand(OnSelectionChangedAsync);

            if(DesignMode.DesignMode2Enabled)
            {
                Articles = SampleDataService.GetRss();
            }
            else
            {
                IsBusy = true;
                var channel = await RssService.GetFeedAsync(new Uri("https://kaki104.tistory.com/rss"));
                if (channel == null) return;
                Articles = channel.Items;
                IsBusy = false;
            }
        }

        private async Task OnSelectionChangedAsync()
        {
            IsBusy = true;
            await Task.Delay(500);
            IsBusy = false;
        }
    }
}
