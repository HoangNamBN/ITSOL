using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace ProductManagement.Customers
{
    public class Customer : AuditedAggregateRoot<Guid>
    {
        [NotNull]
        public string FristName { get; private set; }

        [NotNull]
        public string LastName { get; private set; }

        [NotNull]
        public string Email { get; private set; }

        private Customer() { }

        internal Customer(Guid id, [NotNull] string fristName, [NotNull] string lastName, [NotNull] string email)
        {
            Id = id;
            SetFristName(Check.NotNullOrWhiteSpace(fristName, nameof(fristName)));
            SetLastName(Check.NotNullOrWhiteSpace(lastName, nameof(lastName)));
            SetEmail(Check.NotNullOrWhiteSpace(email, nameof(email)));
        }

        public Customer SetFristName([NotNull] string fristName)
        {
            Check.NotNullOrWhiteSpace(fristName, nameof(fristName));
            if (fristName.Length >= CustomerConsts.MaxFristName)
            {
                throw new ArgumentException($"Customer FristName can not be longer than {CustomerConsts.MaxFristName}");
            }
            FristName = fristName;
            return this;
        }


        public Customer SetLastName([NotNull] string lastName)
        {
            Check.NotNullOrWhiteSpace(lastName, nameof(lastName));
            if (lastName.Length >= CustomerConsts.MaxLastName)
            {
                throw new ArgumentException($"Customer lastName can not be longer than {CustomerConsts.MaxLastName}");
            }
            LastName = lastName;
            return this;
        }

        public Customer SetEmail([NotNull] string email)
        {
            Check.NotNullOrWhiteSpace(email, nameof(email));
            if (email.Length >= CustomerConsts.MaxEmail)
            {
                throw new ArgumentException($"Customer email can not be longer than {CustomerConsts.MaxEmail}");
            }
            Email = email;
            return this;
        }
    }
}