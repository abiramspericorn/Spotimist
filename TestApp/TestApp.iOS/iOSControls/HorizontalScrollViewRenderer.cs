using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using TestApp.Controls;
using TestApp.iOS.iOSControls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HorizontalScrollView), typeof(HorizontalScrollViewRenderer))]
namespace TestApp.iOS.iOSControls
{
    public class HorizontalScrollViewRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var element = e.NewElement as HorizontalScrollView;
            element?.Render();

            // Don't need these.
            ShowsHorizontalScrollIndicator = false;
        }
    }
}