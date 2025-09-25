using DataDictionary.Main.Properties;
using System.Drawing.Drawing2D;

namespace DataDictionary.Main.Enumerations
{
    static class ImageHelper
    {
        /// <summary>
        /// Used to merge images into a composite image.
        /// </summary>
        /// <param name="baseImage">Image to use as the base/background image.</param>
        /// <param name="overlayImage">Image to overlay the base image with.</param>
        /// <param name="alignment">Alignment of the overlay image. Default: Top-Left</param>
        /// <param name="overlay">The overlay behavior of the overlay.</param>
        /// <returns></returns>
        public static Image MergeImage(
            this Image baseImage,
            Image overlayImage,
            ContentAlignment alignment = ContentAlignment.TopLeft,
            CompositingMode overlay = CompositingMode.SourceCopy)
        {
            using (Graphics graphics = Graphics.FromImage(baseImage))
            {
                Int32 xPosition = 0;
                Int32 yPosition = 0;

                // Calculate X coordinate based on horizontal alignment
                switch (alignment)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.BottomLeft:
                        xPosition = 0; // Align left
                        break;
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.MiddleCenter:
                    case ContentAlignment.BottomCenter:
                        xPosition = (baseImage.Width - overlayImage.Width) / 2; // Align center
                        break;
                    case ContentAlignment.TopRight:
                    case ContentAlignment.MiddleRight:
                    case ContentAlignment.BottomRight:
                        xPosition = baseImage.Width - overlayImage.Width; // Align right
                        break;
                }

                // Calculate Y coordinate based on vertical alignment
                switch (alignment)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.TopRight:
                        yPosition = 0; // Align top
                        break;
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.MiddleCenter:
                    case ContentAlignment.MiddleRight:
                        yPosition = (baseImage.Height - overlayImage.Height) / 2; // Align middle
                        break;
                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.BottomCenter:
                    case ContentAlignment.BottomRight:
                        yPosition = baseImage.Height - overlayImage.Height; // Align bottom
                        break;
                }

                graphics.CompositingMode = overlay;
                graphics.DrawImage(overlayImage, xPosition, yPosition, overlayImage.Width, overlayImage.Height);
            }

            return baseImage;
        }

        /// <summary>
        /// Used to merge images into a composite image.
        /// </summary>
        /// <param name="baseIcon">Icon (16x16) to use as the base/background image.</param>
        /// <param name="overlayImage">Image to overlay the base image with.</param>
        /// <param name="alignment">Alignment of the overlay image. Default: Top-Left</param>
        /// <param name="overlay">The overlay behavior of the overlay.</param>
        /// <returns></returns>
        public static Image MergeImage(
            this Icon baseIcon,
            Image overlayImage,
            ContentAlignment alignment = ContentAlignment.TopLeft,
            CompositingMode overlay = CompositingMode.SourceCopy)
        {
            Image result;

            using (Icon smallIcon = new Icon(baseIcon, 16, 16))
            { result = MergeImage(smallIcon.ToBitmap(), overlayImage, alignment, overlay); }

            return result;
        }


    }
}
