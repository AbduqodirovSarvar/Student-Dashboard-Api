﻿namespace Student_Dashboard_Api.Models
{
    public class GetFilterModel
    {
        public string? SearchText { get; set; } = null;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
