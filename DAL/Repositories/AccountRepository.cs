using DAL.Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DAL.Repositories
{
    public class AccountRepository : IRepository
    {
        private readonly string _path;
        private readonly List<DalAccount> _accounts = new List<DalAccount>();
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public AccountRepository(string path)
        {
            _path = path;
        }

        public void CreateAccount(DalAccount account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            string sqlExpression = "INSERT INTO dbo.Users (Id, [First Name], [Last Name], [Account Balance], [Account Bonus], [Account Type]) " +
                "VALUES (@Id, @FirstName, @LastName, @AccountBalance, @AccountBonus, @AccountType)";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand(sqlExpression, connection);

                command.Parameters.Add("@Id", SqlDbType.NVarChar).Value = account.Id;
                command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = account.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = account.LastName;
                command.Parameters.Add("@AccountBalance", SqlDbType.Real).Value = account.AccountBalance;
                command.Parameters.Add("@AccountBonus", SqlDbType.Int).Value = account.AccountBonus;
                command.Parameters.Add("@AccountType", SqlDbType.NVarChar).Value = account.AccountType;

                command.ExecuteNonQuery();
            }

            _accounts.Add(account);
            AppendAccountToFile(account);
        }

        public void RemoveAccount(DalAccount account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            string sqlExpression = "DELETE  FROM dbo.Users WHERE Id=@Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@Id", System.Data.SqlDbType.NVarChar).Value = account.Id;

                command.ExecuteNonQuery();
            }

            _accounts.Remove(_accounts.Find(acc => acc.Id == account.Id));
            AppendAccountsToFile(_accounts);
        }

        public void UpdateAccount(DalAccount account)
        {
            if (account == null)
            {
                throw new ArgumentException(nameof(account));
            }

            string sqlExpression = "DELETE  FROM dbo.Users WHERE Id=@Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand(sqlExpression, connection);
                command.Parameters.Add("@Id", System.Data.SqlDbType.NVarChar).Value = account.Id;

                command.ExecuteNonQuery();
            }

            sqlExpression = "INSERT INTO dbo.Users (Id, [First Name], [Last Name], [Account Balance], [Account Bonus], [Account Type]) " +
                "VALUES (@Id, @FirstName, @LastName, @AccountBalance, @AccountBonus, @AccountType)";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand(sqlExpression, connection);

                command.Parameters.Add("@Id", SqlDbType.NVarChar).Value = account.Id;
                command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = account.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = account.LastName;
                command.Parameters.Add("@AccountBalance", SqlDbType.Real).Value = account.AccountBalance;
                command.Parameters.Add("@AccountBonus", SqlDbType.Int).Value = account.AccountBonus;
                command.Parameters.Add("@AccountType", SqlDbType.NVarChar).Value = account.AccountType;

                command.ExecuteNonQuery();
            }

            _accounts.Remove(_accounts.Find(acc => acc.Id == account.Id));
            _accounts.Add(account);
            AppendAccountsToFile(_accounts);
        }

        public void Purge()
        {
            string sqlExpression = "TRUNCATE TABLE dbo.Users";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand(sqlExpression, connection);

                command.ExecuteNonQuery();
            }

            _accounts.Clear();
        }

        private void AppendAccountToFile(DalAccount account)
        {
            if (string.IsNullOrEmpty(_path))
            {
                throw new ArgumentException(_path);
            }

            using (var binaryWriter = new BinaryWriter(File.Open(_path, FileMode.Append,
                FileAccess.Write, FileShare.None)))
            {
                Writer(binaryWriter, account);
            }
        }

        public void AppendAccountsToFile(List<DalAccount> accounts)
        {
            if (string.IsNullOrEmpty(_path))
            {
                throw new ArgumentException(_path);
            }

            using (var binaryWriter = new BinaryWriter(File.Open(_path, FileMode.Create,
                FileAccess.Write, FileShare.None)))
            {
                foreach (var account in accounts)
                    Writer(binaryWriter, account);
            }
        }

        private static void Writer(BinaryWriter binaryWriter, DalAccount accounts)
        {
            binaryWriter.Write(accounts.Id);
            binaryWriter.Write(accounts.FirstName);
            binaryWriter.Write(accounts.LastName);
            binaryWriter.Write(accounts.AccountBalance);
            binaryWriter.Write(accounts.AccountBonus);
            binaryWriter.Write(accounts.AccountType);
        }

        public IEnumerable<DalAccount> GetAccounts()
        {
            if (string.IsNullOrEmpty(_path))
            {
                throw new ArgumentException(_path);
            }

            var accounts = new List<DalAccount>();
            using (var binaryReader = new BinaryReader(File.Open(_path, FileMode.OpenOrCreate,
                FileAccess.Read, FileShare.Read)))
            {
                while (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
                {
                    var bankAccount = Reader(binaryReader);
                    accounts.Add(bankAccount);
                }
            }

            return accounts;
        }

        private static DalAccount Reader(BinaryReader binaryReader)
        {
            var id = binaryReader.ReadString();
            var firstName = binaryReader.ReadString();
            var lastName = binaryReader.ReadString();
            var accountBalance = binaryReader.ReadDouble();
            var accountBonus = binaryReader.ReadInt32();
            var accountType = binaryReader.ReadString();

            return new DalAccount()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                AccountBalance = accountBalance,
                AccountBonus = accountBonus,
                AccountType = accountType,
            };
        }
    }
}