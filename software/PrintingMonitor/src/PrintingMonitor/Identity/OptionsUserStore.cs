using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace PrintingMonitor.Identity
{
    public class OptionsUserStore : IUserPasswordStore<ApplicationUser>
    {
        private readonly IOptions<SecurityOptions> _options;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public OptionsUserStore(IOptions<SecurityOptions> options, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _options = options;
            _passwordHasher = passwordHasher;
        }

        public void Dispose()
        {
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            
            return Task.FromResult(user.UserName);
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var identityError = new IdentityError
            {
                Code = "NotSupported",
                Description = $"This operation not supported by {nameof(OptionsUserStore)}"
            };

            return Task.FromResult(IdentityResult.Failed(identityError));
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var identityError = new IdentityError
            {
                Code = "NotSupported",
                Description = $"This operation not supported by {nameof(OptionsUserStore)}"
            };

            return Task.FromResult(IdentityResult.Failed(identityError));
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var identityError = new IdentityError
            {
                Code = "NotSupported",
                Description = $"This operation not supported by {nameof(OptionsUserStore)}"
            };

            return Task.FromResult(IdentityResult.Failed(identityError));
        }

        public Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException(nameof(userId));

            if (InvariantNormalizedUsernameCompare(userId))
            {
                return Task.FromResult(GetUserFromOptions());
            }

            return Task.FromResult((ApplicationUser)null);
        }

        public Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (string.IsNullOrWhiteSpace(normalizedUserName)) throw new ArgumentException(nameof(normalizedUserName));

            if (InvariantNormalizedUsernameCompare(normalizedUserName))
            {
                return Task.FromResult(GetUserFromOptions());
            }

            return Task.FromResult((ApplicationUser)null);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            if (InvariantNormalizedUsernameCompare(user.UserName))
            {
                var adminUser = GetUserFromOptions();

                var passwordHash = _passwordHasher.HashPassword(adminUser, adminUser.Password);

                return Task.FromResult(passwordHash);
            }

            return Task.FromResult((string)null);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(InvariantNormalizedUsernameCompare(user.UserName));
        }

        private ApplicationUser GetUserFromOptions()
        {
            return new ApplicationUser
            {
                UserName = _options.Value.UserName,
                Password = _options.Value.Password,
            };
        }

        private bool InvariantNormalizedUsernameCompare(string normalizedUserName)
        {
            var normalizedOptionUserName = _options.Value.UserName.ToUpperInvariant();

            return string.Compare(
               normalizedOptionUserName, 
               normalizedUserName.ToUpperInvariant(), 
               StringComparison.InvariantCulture) == 0;
        }
    }
}
