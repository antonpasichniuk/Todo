using Todo.Application.Services.Interfaces;

namespace Todo.Web.Infrastructure
{
    public class FakeUserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FakeUserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int Id
        {
            get
            {
                var userIdHeader = _httpContextAccessor.HttpContext?.Request.Headers["X-User-Id"].FirstOrDefault();
                if (int.TryParse(userIdHeader, out var userId))
                    return userId;

                throw new UnauthorizedAccessException("Missing or invalid X-User-Id header");
            }
        }
    }
}
