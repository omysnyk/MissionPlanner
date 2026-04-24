using GMap.NET;
using GMap.NET.WindowsForms;
using Ionic.Zip;
using KMZUtils;
using MissionPlanner.Plugin;
using SharpKml.Base;
using SharpKml.Dom;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Point = SharpKml.Dom.Point;
using Style = SharpKml.Dom.Style;

namespace KMZImporter
{
    internal class KmzImporter
    {
        private const string SupportedFileFilter = "All Supported|*.kml;*.kmz|Google Earth KML|*.kml;*.kmz";

        public Plugin Plugin;

        public KmzImporter(Plugin plugin)
        {
            this.Plugin = plugin;
        }


        public void ImportKmz()
        {
            string file;
            using (var fd = new OpenFileDialog())
            {
                fd.Filter = SupportedFileFilter;
                var result = fd.ShowDialog();

                if (result != DialogResult.OK)
                {
                    return;
                }

                file = fd.FileName;
                if (file == "")
                {
                    return;
                }
            }

            string kml;
            if (file.ToLower().EndsWith("kmz"))
            {
                var input = new ZipFile(file);

                var tempDir = Path.GetTempPath() + Path.DirectorySeparatorChar + Path.GetRandomFileName();
                input.ExtractAll(tempDir, ExtractExistingFileAction.OverwriteSilently);

                var kmls = Directory.GetFiles(tempDir, "*.kml");

                //todo: process all files
                if (kmls.Length > 0)
                {
                    file = kmls[0];
                    input.Dispose();
                }
                else
                {
                    input.Dispose();
                    return;
                }
            }

            var sr = new StreamReader(File.OpenRead(file));
            kml = sr.ReadToEnd();
            sr.Close();

            kml = kml.Replace("<Snippet/>", "");

            var parser = new Parser();
            parser.ParseString(kml, false);

            var rootNode = parser.Root as Kml;
            if (rootNode == null)
            {
                Console.Error.WriteLine("Unexpected KMZ content.");
                return;
            }

            var form = new SelectKmzItemsForm(rootNode.Feature);

            var dialogResult = form.ShowDialog();
            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            ClearOverlay();

            foreach (var placemark in form.SelectedPlaceMarks)
            {
                var geometry = placemark.Geometry;
                var styleUrl = placemark.StyleUrl;
                AddGeometry(geometry, styleUrl, form.Document, placemark.Name);
            }
        }

        public void ClearOverlay()
        {
            var kmlOverlay = Plugin.Host.MainForm.FlightPlanner.kmlpolygonsoverlay;
            kmlOverlay.Polygons.Clear();
            kmlOverlay.Routes.Clear();
            kmlOverlay.Markers.Clear();
        }

        private void AddGeometry(Geometry geometry, Uri styleUrl, Document document, string placemarkName)
        {
            var kmlOverlay = Plugin.Host.MainForm.FlightPlanner.kmlpolygonsoverlay;

            switch (geometry)
            {
                case Polygon polygon:
                {
                    var kmlpolygon = new GMapPolygon(new List<PointLatLng>(), placemarkName);

                    var colorWidth = GetKmlLineColor(styleUrl?.OriginalString.TrimStart('#'), document);

                    kmlpolygon.Stroke = new Pen(colorWidth.Item1, colorWidth.Item2);
                    kmlpolygon.Fill = Brushes.Transparent;

                    foreach (var loc in polygon.OuterBoundary.LinearRing.Coordinates)
                    {
                        kmlpolygon.Points.Add(new PointLatLng(loc.Latitude, loc.Longitude));
                    }

                    kmlOverlay.Polygons.Add(kmlpolygon);

                    break;
                }
                case LineString _:
                {
                    var kmlroute = new GMapRoute(new List<PointLatLng>(), placemarkName);

                    //var colorwidth = GetKMLLineColor(styleurl?.OriginalString.TrimStart('#'), root);
                    //kmlroute.Stroke = new Pen(colorwidth.Item1, colorwidth.Item2);

                    // foreach (var loc in ((LineString)Element2).Coordinates) {
                    //     kmlroute.Points.Add(new PointLatLng(loc.Latitude, loc.Longitude));
                    // }

                    //kmlpolygonsoverlay.Routes.Add(kmlroute);
                    break;
                }
                case MultipleGeometry multipleGeometry:
                {
                    foreach (var subGeometry in multipleGeometry.Geometry)
                        AddGeometry(subGeometry, styleUrl, document, placemarkName);
                    break;
                }
                case Point _:
                {
                    // its a label
                    // var text = placemark.Name;
                    // var lookat = placemark.CalculateLookAt();

                    //kmlpolygonsoverlay.Markers.Add(new GMapMarkerKMLLabel(new PointLatLng(lookat.Latitude.Value, lookat.Longitude.Value), text));
                    break;
                }
            }
        }

        private static (Color, int) GetKmlLineColor(string styleUrl, Document root)
        {
            if (string.IsNullOrEmpty(styleUrl)) return (Color.White, 2);

            var referencedStyle = root.Styles.First(a => a.Id == styleUrl.TrimStart('#'));

            Style resolvedStyle;
            switch (referencedStyle)
            {
                case Style style:
                    resolvedStyle = style;
                    break;
                case StyleMapCollection collection:
                {
                    var firstStyle = collection.First().StyleUrl;
                    resolvedStyle = root.Styles.First(a => a.Id == firstStyle.OriginalString.TrimStart('#')) as Style;
                    break;
                }
                default:
                    return (Color.White, 2);
            }

            if (resolvedStyle?.Line == null) return (Color.White, 2);


            int color;
            if (resolvedStyle.Line.Color != null)
            {
                color = resolvedStyle.Line.Color.Value.Abgr;
                color = (int)((color & 0xFF00FF00) | ((color & 0x00FF0000) >> 16) | ((color & 0x000000FF) << 16));
            }
            else
            {
                color = Color.White.ToArgb();
            }

            // convert color from ABGR to ARGB
            return resolvedStyle.Line.Width == null
                ? (Color.FromArgb(color), 2)
                : (Color.FromArgb(color), (int)resolvedStyle.Line.Width.Value);
        }
    }
}