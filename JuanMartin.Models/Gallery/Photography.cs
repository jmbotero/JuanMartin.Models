using System;
using System.Collections.Generic;

namespace JuanMartin.Models.Gallery
{
    public class Photography
    {

        public enum PhysicalSource
        {
            negative = 0,
            slide = 1
        };

        public long Id { get; set; }
        public int UserId { get; set; }
        public PhysicalSource Source { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public int NumberOfViews { get; set; }
        public  string[] Keywords { get; set; }
        public double Rank { get; set; }
        public string Location { get; set; }

        public override string ToString()
        {
            return $"{System.IO.Path.Combine(Path, FileName)},{Rank},{Location}";
        }

        public void AddKeywords(string keywords)
        {
            Keywords = keywords.Split(',');
        }
    }
}
