using System;
using System.Collections.Generic;
using System.Linq;

namespace JuanMartin.Models.Gallery
{
    public class Photography
    {

        public enum PhysicalSource
        {
            negative = 0,
            slide = 1
        };

        public Photography()
        {
            Tags = new List<string>();
        }
        public long Id { get; set; }
        public int UserId { get; set; }
        public PhysicalSource Source { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public int NumberOfViews { get; set; }
        public  List<string> Tags { get; set; }
        public long Rank { get; set; }
        public double AverageRank { get; set; }
        public string Location { get; set; }

        public override string ToString()
        {
            return $"{System.IO.Path.Combine(Path, FileName)},{Rank},{Location}";
        }

        public void ParseTags(string tags)
        {
            if (string.IsNullOrEmpty(tags))
            {
                Tags = new List<string>();
                return;
            }

            Tags = tags.Split(',').Select(t=>t=t.Trim()).ToList();
        }
    }
}
