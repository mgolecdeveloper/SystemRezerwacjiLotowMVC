﻿namespace SystemRezerwacjiLotow.Domain
{
    /// <summary>
    /// Model posiadający obiektwy wykorzystywane podczas wyszukiwania oraz filtrowania danych
    /// </summary>
    public class BaseSearchModel<T>
    {
        // Wyszukiwarka  
        public string q { get; set; }
        public string SearchOption { get; set; }
        public string SortowanieOption { get; set; }


        // Paginator
        public Paginator<T> Paginator { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 500;
        public int IlePokazac { get; set; } = 0;
        public int Start { get; set; } = 0;
        public int End { get; set; } = 0;
    }
}
