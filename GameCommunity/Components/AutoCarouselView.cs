using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace GameCommunity.Components
{
    public class AutoCarouselView: CarouselView
    {
        public static readonly BindableProperty IntervalProperty 
            = BindableProperty.Create(nameof(Interval), typeof(int),
                typeof(AutoCarouselView), 0,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: IntervalPropertyChanged);

        public AutoCarouselView() : base()
        {
            
        }

        /// <summary>
        /// 设置图片切换的时间间隔（单位：秒）
        /// </summary>
        public int Interval
        {
            get
            {
                return (int)GetValue(IntervalProperty);
            }
            set
            {
                SetValue(IntervalProperty, value);
            }
        }

        private Timer _timer;

        private void OnTime(int interval)
        {
            if (interval <= 0 && _timer != null)
            {
                _timer.Dispose();
                _timer = null;
                return;
            }

            if (_timer == null)
            {
                _timer = new Timer(Carousel, null, TimeSpan.Zero, TimeSpan.FromSeconds(interval));
            }
            else
            {
                _timer.Change(TimeSpan.Zero, TimeSpan.FromSeconds(interval));
            }
        }

        private static void IntervalPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AutoCarouselView)bindable;
            if (newValue != null)
            {
                control.OnTime((int)newValue);
            }
        }
        private void Carousel(object state)
        {
            var items = this.ItemsSource;
            if (items == null)
            {
                return;
            }

            var lenght = 0;
            foreach (var item in items)
            {
                lenght++;
            }
            var position = (this.Position + 1) % lenght;
            this.ScrollTo(position);
        }
    }
}
