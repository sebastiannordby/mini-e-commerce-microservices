using MiniECommerce.Authentication.Services;

namespace MiniECommerce.Testing
{
    internal class TestCurrentUserService : ICurrentUserService
    {
        public string UserEmail => "test@test.com";
    }
}