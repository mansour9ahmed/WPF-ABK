using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public static class InvoiceFilterValues
    {
        public static bool IsPaidFilter { get; set; } = false;
        public static bool IsNotPaidFilter { get; set; } = false;
        public static DateTime FromDateFilter { get; set; } = DateTime.Now;
        public static DateTime ToDateFilter { get; set; } = DateTime.Now;
        public static Company CompanyFilter { get; set; } = null;

    }
}
