using System;
using UIKit;
using Foundation;
using CoreLocation;

namespace BeaconDemo
{
	public class HomeViewController : UIViewController
	{
		CLLocationManager locationManager;

		UITextView textView;

		public HomeViewController ()
		{

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.textView = new UITextView (this.View.Bounds);
			this.locationManager = new CLLocationManager ();

			var uuid = new NSUuid ("B9407F30-F5F8-466E-AFF9-25556B57FE6D");
			var region = new CLBeaconRegion (uuid, "myIBeacon");

			region.NotifyEntryStateOnDisplay = true;
			region.NotifyOnEntry = true;
			region.NotifyOnExit = true;

			locationManager.RequestWhenInUseAuthorization ();

			locationManager.RegionEntered += (object sender, CLRegionEventArgs e) => {
				// Do something when user's device enters range of the beacon.
				// (i.e, show a notification, send data to //a server, etc.)
				this.textView.Text += String.Format("[{0:T}] Beacon Region Entered\n", DateTime.Now);

			};

			locationManager.RegionLeft += (object sender, CLRegionEventArgs e) => {
				// Do something when user's device exits range of the beacon.
				// (i.e, show a notification, send data to //a server, etc.)
				this.textView.Text += String.Format("[{0:T}] Beacon Region Left\n", DateTime.Now);
			};

			locationManager.DidRangeBeacons += (object sender, CLRegionBeaconsRangedEventArgs e) => {
				foreach (var b in e.Beacons) {
					this.textView.Text += String.Format("[{0:T}] Beacon Ranged: Major = {1}, Minor = {2}\n", DateTime.Now,  b.Major, b.Minor);
				}
			};
		}
	}
}

