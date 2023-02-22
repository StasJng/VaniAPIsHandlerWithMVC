using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web;

namespace MP.Lib.LibUtility
{
	public class MultimediaUtility
	{
		private static bool ThumbnailCallback()
		{
			return false;
		}
        public static Image Crop(Image p, int width, int height)
        {
            Bitmap b = new Bitmap(p);
            Bitmap imgCropped = new Bitmap(width, height);
            Graphics objGraphic = Graphics.FromImage(imgCropped);
            int intStartTop = 0;
            int intStartLeft = 0;
            objGraphic.DrawImage(b, intStartLeft, intStartTop);
            b.Dispose();
            objGraphic.Dispose();
            return imgCropped;
        }
       
		public static bool SetThumbnail(string filePath, string newPath, int iThumbWidth, int iThumbHeight)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			if (!fileInfo.Exists) return false;
			try
			{
				if (!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);

				Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
				Bitmap myBitmap = new Bitmap(fileInfo.FullName);

				if ((iThumbHeight == 0) && (iThumbWidth == 0)) return false;
				else if ((iThumbHeight != 0) && (iThumbWidth == 0))
					iThumbWidth = (int) (iThumbHeight*myBitmap.Width)/myBitmap.Height;
				else if ((iThumbHeight == 0) && (iThumbWidth != 0))
					iThumbHeight = (int) (iThumbWidth*myBitmap.Height)/myBitmap.Width;

				Image myThumbnail = myBitmap.GetThumbnailImage(iThumbWidth, iThumbHeight, myCallback, IntPtr.Zero);
				myThumbnail.Save(newPath + fileInfo.Name, ImageFormat.Jpeg);
			}
			catch
			{
				return false;
			}
			return true;
		}

		public static bool SetAvatarThumbnail(string filePath, int iThumbWidth, int iThumbHeight)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			if (!fileInfo.Exists) return false;
			return SetThumbnail(filePath, fileInfo.Directory + "\\Avatar\\", iThumbWidth, iThumbHeight);
		}

        public static string GetAvatarThumnail(string avatar, int width, int height)
        {
            int splitIndex = avatar.LastIndexOf("/");
            if (splitIndex >= 0)
                return AppEnv.GetSetting("datasite") + avatar.Substring(0, splitIndex) + "/" + "WebAvatar" + width + "_" + height + "/" + avatar.Substring(splitIndex, avatar.Length - splitIndex).Replace("/", "");
            else return string.Empty;

        }
      
     
		public static string GetAvatar(string avatar)
		{
			int splitIndex = avatar.LastIndexOf("/");
			if (splitIndex != 0)
				return avatar.Substring(0, splitIndex) + "/Avatar" + avatar.Substring(splitIndex, avatar.Length - splitIndex);
			else return string.Empty;

		}
		public static string strInitImage(string image, int width, int height)
		{
			string retVal = "<img src=\"" + image + "\"";
			if (width > 0) retVal += " width=\"" + width + "px\" ";
			if (height > 0) retVal += " height=\"" + height + "px\" ";
			retVal += ">";
			return retVal;
		}
        public static string strInitFlashWithActiveContent(string fileName, int width, int height, string _align)
        {
            string retVal = "<script type=\"text/javascript\">";
            retVal += "AC_FL_RunContent( 'codebase','http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0','width','" + width + "','height','" + height + "','id','" + DateTime.Now.Ticks + "','align','" + _align + "','src','" + fileName + "','quality','high','bgcolor','','name','" + fileName + "','allowscriptaccess','always','pluginspage','http://www.macromedia.com/go/getflashplayer','movie','" + fileName + "' );";
            retVal += "</script>";
            return retVal;
        }
        public static string strInitFlash(string flashURL, int width, int height)
        {
            string retVal = "<EMBED align=baseline src='" + flashURL + "'";
            if (width != 0) retVal += " width=" + width;
            if (height != 0) retVal += " height=" + height;

            retVal += " type=audio/x-pn-realaudio-plugin autostart=\"true\" controls=\"ControlPanel\" console=\"Clip1\" border=\"0\">";
            return retVal;
        }
		public static string strInitMultimedia(string mediaPath, int width, int height)
		{
			string retVal = "<EMBED pluginspage='http://www.microsoft.com/Windows/Downloads/Contents/Products/MediaPlayer/' ";
			if (width != 0) retVal += " width=" + width;
			if (height != 0) retVal += " height=" + height;

			retVal += " src='" + mediaPath + "' type='application/x-mplayer2' ShowStatusBar='1' AutoStart='true' ShowControls='1'></embed>";
			return retVal;
		}

        public static string GetTimeDistance(DateTime time)
        {
            var result = new StringBuilder(string.Empty);
            var timeDistance = DateTime.Now - time;
            var days = timeDistance.Days;
            var hours = timeDistance.Hours;
            var minutes = timeDistance.Minutes;
            if (days > 0)
            {
                var day = timeDistance.Days;
                if (day > 365)
                {
                    result.Append((int)day / 365 + " năm " + day % 365 + " ngày trước");
                }
                else
                {
                    result.Append(day + " ngày trước");
                }
                return result.ToString();
            }
            if (hours > 0)
            {
                result.Append(hours + " giờ trước");
                return result.ToString();
            }
            if (minutes > 0)
            {
                result.Append(minutes + " phút trước");
                return result.ToString();
            }
            return result.ToString();
        }
	}
}