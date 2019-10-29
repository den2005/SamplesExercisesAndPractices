using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace VS2017.BankSystem.AdoNetClsLib
{
    public class DataAccessLayer : IDataAccessLayer
    {
        //private readonly string _connectionString = @"Data Source=DESKTOP-GB4QU6O\SQLEXPRESS2016;Initial Catalog=PracticeTestDb;Integrated Security=True";
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\practice_projects\SamplesExercisesAndPractices\VS2017.BankSystem.AdoNetClsLib\VS2017.BankSystem.AdoNetClsLib\Database1.mdf;Integrated Security=True";

        public List<BankAccount> GetAllBankAccounts()
        {
            List<BankAccount> accts = new List<BankAccount>();
            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try
            {
                //string commandText = "sp_GetAllBankAccounts";

                using (conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetAllBankAccounts", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        BankAccount acct = new BankAccount()
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            LoginName = rdr["LoginName"].ToString(),
                            AccountNumber = rdr["AccountNumber"].ToString(),
                            Password = rdr["AccountNumber"].ToString(),
                            Balance = Convert.ToDouble(rdr["Balance"]),
                            CreatedDate = Convert.ToDateTime(rdr["CreatedDate"])
                        };

                        accts.Add(acct);
                    }

                    rdr.Close();
                }
            }
            catch
            {

            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

            return accts;
        }

        public BankAccount GetBankAccountByAccountNumber(string accountNumber)
        {
            BankAccount acct = null;
            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try
            {
                using (conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetBankAccountByAccountNumber", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@AccountNumber", accountNumber));

                    conn.Open();
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        acct = new BankAccount()
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            LoginName = rdr["LoginName"].ToString(),
                            AccountNumber = rdr["AccountNumber"].ToString(),
                            Password = rdr["AccountNumber"].ToString(),
                            Balance = Convert.ToDouble(rdr["Balance"]),
                            CreatedDate = Convert.ToDateTime(rdr["CreatedDate"])
                        };

                    }

                    rdr.Close();

                }

            }
            catch
            {

            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            return acct;
        }

        public BankAccount GetBankAccountByLoginName(string loginName)
        {
            BankAccount acct = null;
            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try
            {
                using(conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetBankAccountByLoginName", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@LoginName",loginName));

                    conn.Open();
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        acct = new BankAccount()
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            LoginName = rdr["LoginName"].ToString(),
                            AccountNumber = rdr["AccountNumber"].ToString(),
                            Password = rdr["AccountNumber"].ToString(),
                            Balance = Convert.ToDouble(rdr["Balance"]),
                            CreatedDate = Convert.ToDateTime(rdr["CreatedDate"])
                        };

                    }

                    rdr.Close();

                }

            }
            catch
            {

            }
            finally {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            return acct;
        }

        public void InsertNewBankAccount(BankAccount newAcct)
        {
            SqlConnection conn = null;

            try
            {
                using(conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertNewBankAccount", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@LoginName",newAcct.LoginName));
                    cmd.Parameters.Add(new SqlParameter("@AccountName", newAcct.AccountNumber));
                    cmd.Parameters.Add(new SqlParameter("@Password", newAcct.Password));
                    cmd.Parameters.Add(new SqlParameter("@Balance", newAcct.Balance));
                    cmd.Parameters.Add(new SqlParameter("@CreatedDate", newAcct.CreatedDate));

                    conn.Open();

                    int recAffected = cmd.ExecuteNonQuery();

                }
            }
            catch
            {

            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        public void UpdateBankAccount(BankAccount editAcct, string transactionType, double transAmount)
        {
            SqlConnection conn = null;

            try
            {
                using (conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateBankAccount", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Id", editAcct.Id));
                    cmd.Parameters.Add(new SqlParameter("@LoginName", editAcct.LoginName));
                    cmd.Parameters.Add(new SqlParameter("@AccountName", editAcct.AccountNumber));
                    cmd.Parameters.Add(new SqlParameter("@Password", editAcct.Password));
                    cmd.Parameters.Add(new SqlParameter("@Balance", editAcct.Balance));
                    cmd.Parameters.Add(new SqlParameter("@CreatedDate", editAcct.CreatedDate));
                    cmd.Parameters.Add(new SqlParameter("@TransactionType", transactionType));
                    cmd.Parameters.Add(new SqlParameter("@TransactionAmount", transAmount));

                    conn.Open();

                    int recAffected = cmd.ExecuteNonQuery();

                }
            }
            catch
            {

            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

    }
}
