using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proje.Models
{
    public class Channel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string field1 { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int last_entry_id { get; set; }
    }

    public class Feed
    {
        public DateTime created_at { get; set; }
        public int entry_id { get; set; }
        public string field1 { get; set; }
    }

    public class ResponseApi
    {
        public Channel channel { get; set; }
        public List<Feed> feeds { get; set; }
    }
}