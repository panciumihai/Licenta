using CampusManagement.Business.Article.Models;
using CampusManagement.Business.Generics;

namespace CampusManagement.Business.Article
{
    public interface IArticleService :
        IDetailsService<ArticleDetailsModel>,
        ICreateService<ArticleCreateModel>
    {
    }
}
