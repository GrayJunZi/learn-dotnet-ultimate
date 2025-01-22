using StocksApp.Models;

namespace StocksApp.ServiceContracts;

public interface IPostService
{
    Task<IEnumerable<Post>> GetPosts();
}