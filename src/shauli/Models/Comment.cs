using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shauli.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public string CommentTitle { get; set; }
        public string CommentAuthorName { get; set; }
        public string CommentAuthorWebAddress { get; set; }
        public string Text { get; set; }
        public virtual Post Post { get; set; }
    }
}
