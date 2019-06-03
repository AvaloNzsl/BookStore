using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Model
{
    public class PageInfo
    {
        public int TotalItems { get; set; }             //summary books
        public int ItemsPerPage { get; set; }           //counter books per page
        public int CurrentPage { get; set; }            //number current page
        public int TotalPages                           //summary pages
        {      
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}
