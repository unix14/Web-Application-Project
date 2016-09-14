using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shauli.Models
{
    public class Post
    {
        public  int PostID { get; set; }
        public string PostTitle { get; set; }
        public string PostAuthorName { get; set; }
        public string PostAuthorWebAddress { get; set; }
        public DateTime PostPublished { get; set; }
        public string Text { get; set; }
        public string PicturePath { get; set; }
        public string VideoPath { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        
    }
}
