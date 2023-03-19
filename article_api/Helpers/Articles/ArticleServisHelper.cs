using dll.Models;

namespace article_api.Helpers.Articles
{
    public class ArticleServisHelper
    {
        public static (bool isValid, string? errorMessage) ValidateRequest(Article request)
        {
            var errorMessages = new List<string>();

            if (request == null)
                errorMessages.Add("Request is null.");
            else
            {
                if (string.IsNullOrWhiteSpace(request.ArticleNumber))
                    errorMessages.Add("Article number is invalid.");

                if (string.IsNullOrWhiteSpace(request.Name))
                    errorMessages.Add("Article name is invalid.");

                if (request.Price <= 0)
                    errorMessages.Add("Article price is invalid.");

                if (request.CreatedUTC == default)
                    errorMessages.Add("Article createdUTC is invalid.");
            }

            return (!errorMessages.Any(), string.Join("\n", errorMessages));
        }
    }
}
