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

[assembly: ExportRenderer(typeof(PaddedScrollView), typeof(PaddedScrollViewRenderer))]
namespace TestApp.iOS.iOSControls
{
    public class PaddedScrollViewRenderer : ScrollViewRenderer
    {
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            // An inset at the bottom because else we will have too much space there.
            ContentInset = new UIEdgeInsets(0, 0, -60, 0);
        }
    }
}