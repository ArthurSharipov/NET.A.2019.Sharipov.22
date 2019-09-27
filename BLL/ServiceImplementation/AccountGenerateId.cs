using BLL.Interface.Interfaces;
using System;

namespace BLL.ServiceImplementation
{
    public class AccountGenerateId : IAccountGenerateIdNumber
    {
        public string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
