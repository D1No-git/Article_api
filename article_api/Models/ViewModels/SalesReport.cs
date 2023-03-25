namespace article_api.Models.ViewModels
{
    public class SalesReport
    {
        public DateTime Date { get; set; }
        public int TotalArticlesSold { get; set; }
        public int TotalUniqueArticlesSold { get; set; }
    }
}
