using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoldenthreadwithBreeze.Models
{
    public class VoiceViewModel
    {
        public int ID { get; set; }
        public int AuthorID { get; set; }
        public string VoiceStatus { get; set; }
        public string Type { get; set; }
        public string  Video { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public int IDChapter { get; set; }
        public string ChapterTitle { get; set; }
        public string Tag { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string IDGoogle { get; set; }
        public string IDFacebook { get; set; }
        public string IDInstagram { get; set; }
        public string IDTwitter { get; set; }
        public string UserName { get; set; }

    }
}