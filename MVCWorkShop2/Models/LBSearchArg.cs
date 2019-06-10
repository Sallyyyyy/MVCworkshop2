using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVCWorkShop2.Models
{
    public class LBSearchArg
    {
 
        [DisplayName("書名")]
        public string BookId { get; set; }
        [DisplayName("書籍類別")]
        public string BookClassName { get; set; }
        [DisplayName("借閱人")]
        public string BookKeeper { get; set; }
        [DisplayName("借閱狀態")]
        public string BookStatus { get; set; }
    }
}