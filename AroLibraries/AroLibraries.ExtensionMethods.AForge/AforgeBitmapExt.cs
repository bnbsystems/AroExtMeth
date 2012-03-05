using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace AroLibraries.ExtensionMethods.AForge
{
    public static class AforgeBitmapExt
    {
        public static IEnumerable<Bitmap> GetBlobObjects(this Bitmap iBitmap)
        {
            return GetBlobObjects(iBitmap, new Size(5, 5));
        }

        public static IEnumerable<Bitmap> GetBlobObjects(this Bitmap iBitmap, Size iMinSize)
        {
            BlobCounter blobCounter = new BlobCounter(iBitmap);
            blobCounter.FilterBlobs = true;
            blobCounter.MinWidth = iMinSize.Width;
            blobCounter.MinHeight = iMinSize.Height;
            blobCounter.ObjectsOrder = ObjectsOrder.Size;
            blobCounter.ProcessImage(iBitmap);

            Blob[] blobs = blobCounter.GetObjects(iBitmap, false);
            foreach (Blob blob in blobs)
            {
                yield return blob.Image.ToManagedImage();
            }
        }

        public static Bitmap GetBlobObject(this Bitmap iBitmap)
        {
            BlobCounterBase bc = new BlobCounter();
            bc.FilterBlobs = true;
            bc.MinWidth = 1;
            bc.MinHeight = 1;
            bc.ObjectsOrder = ObjectsOrder.Size;
            bc.ProcessImage(iBitmap);
            Blob[] blobs = bc.GetObjectsInformation();
            if (blobs.Length > 0)
            {
                bc.ExtractBlobsImage(iBitmap, blobs[0], false);
            }
            return blobs[0].Image.ToManagedImage();
        }

        public static Bitmap ToGrayscale(this Bitmap iBitmap)
        {
            Grayscale filteasdsadr = new Grayscale(0.2125, 0.7154, 0.0721);
            return filteasdsadr.Apply(iBitmap);
        }
        public static Bitmap ToThreshold(this Bitmap iBitmap, int iThresholdValue)
        {
            Threshold vThreshold = new Threshold(iThresholdValue);
            return vThreshold.Apply(iBitmap.ToGrayscale());
        }
        public static Bitmap ToThreshold(this Bitmap iBitmap)
        {
            return ToThreshold(iBitmap, 128);
        }


        public static IEnumerable<System.Drawing.Point> GetCorners(this Bitmap iBitmap)
        {
            return GetCornersSusan(iBitmap);
        }

        public static IEnumerable<System.Drawing.Point> GetCornersSusan(this Bitmap iBitmap)
        {
            SusanCornersDetector scd = new SusanCornersDetector();
            List<IntPoint> corners = scd.ProcessImage(iBitmap);
            CornersMarker filterCornersMarker = new CornersMarker(scd, Color.Red);
            var result = filterCornersMarker.Apply(iBitmap);
            return corners.Select(x => new System.Drawing.Point(x.X, x.Y));
        }
        public static IEnumerable<System.Drawing.Point> GetCornersMoravec(this Bitmap iBitmap)
        {
            MoravecCornersDetector mcd = new MoravecCornersDetector();
            List<IntPoint> corner2s = mcd.ProcessImage(iBitmap);
            CornersMarker filterCornersMarker = new CornersMarker(mcd, Color.Red);
            var result2 = filterCornersMarker.Apply(iBitmap);
            return corner2s.Select(x => new System.Drawing.Point(x.X, x.Y));
        }

        public static IEnumerable<Bitmap> GetInnerBlobObjects(this Bitmap iBitmap)
        {
            var biggestBlobObject = GetBlobObject(iBitmap);
            Bitmap grayImage = biggestBlobObject.ToGrayscale();
            FiltersSequence sq = new FiltersSequence();
            sq.Add(new BradleyLocalThresholding());
            sq.Add(new Invert());
            Bitmap revorsIMage = sq.Apply(grayImage);
            var innerBlobs = revorsIMage.GetBlobObjects(new Size(2, 2));
            int rCounter = innerBlobs.Count();
            return innerBlobs;
            //Edges filter1 = new Edges();
            //var test3= filter1.Apply(biggestBlobObject);

            //HomogenityEdgeDetector filter2 = new HomogenityEdgeDetector();
            //var tesdasd2 = filter2.Apply(qwe);

            //DifferenceEdgeDetector filter3 = new DifferenceEdgeDetector();
            //var tesdasd322 = filter3.Apply(qwe);

            //SobelEdgeDetector filter4 = new SobelEdgeDetector();
            //var tesdasd3asdas22 = filter4.Apply(qwe);

            //CannyEdgeDetector filter45 = new CannyEdgeDetector( );
            //var tesdasd22 = filter45.Apply(qwe);
        }

        public static Bitmap ToPixellate(this Bitmap iBitmap, int pixelSize)
        {
            Pixellate filter = new Pixellate(pixelSize);
            return filter.Apply(iBitmap);
        }


    }
}