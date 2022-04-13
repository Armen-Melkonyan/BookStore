using System;
using System.Collections.Generic;

namespace MyTask.BookStore.Models
{
    public class PaginationModel
    {
        public int TotalItem { get; private set; }//total number of recordes
        public int CurrentPage { get; private set; }//Activ page 
        public int PageSize { get; private set; }//Number of recordes in one page

        public int TotalPages { get; private set; }//Number of all pages
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public PaginationModel()
        {

        }
        public PaginationModel(int totalItems, int page, int pageSize = 4)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = page;

            int startPage = currentPage - 2;
            int endPage = currentPage + 1;

            if (startPage <= 0)
            {
                EndPage = endPage - (-startPage-1);
                startPage = 1;
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;

                if (endPage > 4)
                {
                    startPage = endPage - 3;
                }
            }

            TotalPages = totalPages;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalItem = totalItems;
            StartPage = startPage;
            EndPage = endPage;
        }
    }
}