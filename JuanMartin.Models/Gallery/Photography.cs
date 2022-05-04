using System.Collections.Generic;

namespace JuanMartin.Models.Gallery
{
    public class PhotoGraphy
    {

        public enum PhysicalSource
        {
            negative = 0,
            slide = 1
        };

        public long Id { get; set; }
        public PhysicalSource Source { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public int NumberOfViews { get; set; }
        public  List<string> Keywords { get; set; }
        public double Rank { get; private set; }

    }
}
