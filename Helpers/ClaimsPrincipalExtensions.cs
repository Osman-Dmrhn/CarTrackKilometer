using System.Security.Claims;

namespace CarKilometerTrack.Helpers
{
    public static class ClaimsPrincipalExtensions
    {
        public static int? GetUserId(this ClaimsPrincipal user)
        {
            if(user == null) { return null; }

            var userIdClaim=user.FindFirstValue(ClaimTypes.NameIdentifier);

            if(int.TryParse(userIdClaim, out var UserId)) {  return UserId; }

            return null;
        }
    }
}
