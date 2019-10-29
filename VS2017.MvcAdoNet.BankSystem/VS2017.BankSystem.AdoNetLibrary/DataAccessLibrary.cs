using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace VS2017.BankSystem.AdoNetLibrary
{
    public class DataAccessLibrary : IDataAccessLibrary
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\practice_projects\SamplesExercisesAndPractices\VS2017.MvcAdoNet.BankSystem\VS2017.BankSystem.AdoNetLibrary\Database1.mdf;Integrated Security=True";

        public List<BankAccount> GetAllBankAccounts()
        {
            List<BankAccount> accts = new List<BankAccount>();
            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try
            {                

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
                            Password = rdr["Password"].ToString(),
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
                using (conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetBankAccountByLoginName", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@LoginName", loginName));

                    conn.Open();
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        acct = new BankAccount()
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            LoginName = rdr["LoginName"].ToString(),
                            AccountNumber = rdr["AccountNumber"].ToString(),
                            Password = rdr["Password"].ToString(),
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

        public BankAccount GetBankAccountById(int id)
        {
            BankAccount acct = null;
            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try
            {
                using (conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetBankAccountById", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));

                    conn.Open();
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        acct = new BankAccount()
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            LoginName = rdr["LoginName"].ToString(),
                            AccountNumber = rdr["AccountNumber"].ToString(),
                            Password = rdr["Password"].ToString(),
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

        public void InsertNewBankAccount(BankAccount newAcct, string transactionType)
        {
            SqlConnection conn = null;

            try
            {
                using (conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertNewBankAccount", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@LoginName", newAcct.LoginName));
                    cmd.Parameters.Add(new SqlParameter("@AccountNumber", newAcct.AccountNumber));
                    cmd.Parameters.Add(new SqlParameter("@Password", newAcct.Password));
                    cmd.Parameters.Add(new SqlParameter("@Balance", newAcct.Balance));
                    cmd.Parameters.Add(new SqlParameter("@CreatedDate", newAcct.CreatedDate));
                    cmd.Parameters.Add(new SqlParameter("@TransactionType", transactionType));

                    conn.Open();

                    int recAffected = cmd.ExecuteNonQuery();

                }
            }
            catch (SqlException sqlEx)
            {

            }
            catch (Exception ex)
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
                    cmd.Parameters.Add(new SqlParameter("@AccountNumber", editAcct.AccountNumber));
                    cmd.Parameters.Add(new SqlParameter("@Password", editAcct.Password));
                    cmd.Parameters.Add(new SqlParameter("@Balance", editAcct.Balance));
                    cmd.Parameters.Add(new SqlParameter("@CreatedDate", editAcct.CreatedDate));
                    cmd.Parameters.Add(new SqlParameter("@TransactionType", transactionType));
                    cmd.Parameters.Add(new SqlParameter("@TransactionAmount", transAmount));

                    conn.Open();

                    int recAffected = cmd.ExecuteNonQuery();

                }
            }   
            catch(SqlException sqlEx)
            {

            }
            catch (Exception ex)
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

        public void UpdateBankAccount(BankAccount sourceAcct, BankAccount targetAcct, string transactionType, double transAmount)
        {
            SqlConnection conn = null;

            try
            {
                using (conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_TransferAmountBankAccount", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                 
                    cmd.Parameters.Add(new SqlParameter("@SourceAccountNumber", sourceAcct.AccountNumber));
                    cmd.Parameters.Add(new SqlParameter("@TargetAccountNumber", targetAcct.AccountNumber));                   
                    cmd.Parameters.Add(new SqlParameter("@TransactionType", transactionType));
                    cmd.Parameters.Add(new SqlParameter("@TransactionAmount", transAmount));

                    conn.Open();

                    int recAffected = cmd.ExecuteNonQuery();

                }
            }
            catch (SqlException sqlEx)
            {

            }
            catch(Exception ex)
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

        public List<TransactionHistory> GetTransactionHistoryRecordsByAccountId(int bankAcctId)
        {
            List<TransactionHistory> transactions = new List<TransactionHistory>();

            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try
            {
                using (conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetTransactionHistoryByBankAccountId", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@BankAccountId",bankAcctId));

                    conn.Open();
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        TransactionHistory trans = new TransactionHistory()
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            BankAccountId = Convert.ToInt32(rdr["BankAccountId"]),
                            AccountNumber = rdr["AccountNumber"].ToString(),
                            TransactionType = rdr["TransactionType"].ToString(),
                            TransactionAmount = Convert.ToDouble(rdr["TransactionAmount"]),
                            TransactionDate = Convert.ToDateTime(rdr["TransactionDate"])
                        };

                        transactions.Add(trans);
                    }

                    rdr.Close();
                }
            }
            catch (SqlException sqlEx)
            {

            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

            return transactions;
        }

        public List<TransactionHistory> GetTransactionHistoryRecordsByAccountNumber(string accountNumber)
        {
            List<TransactionHistory> transactions = new List<TransactionHistory>();

            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try
            {
                using (conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetTransactionHistoryByAccountNumber", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@AccountNumber", accountNumber));

                    conn.Open();
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        TransactionHistory trans = new TransactionHistory()
                        {
                            Id = Convert.ToInt32(rdr["Id"]),
                            BankAccountId = Convert.ToInt32(rdr["BankAccountId"]),
                            AccountNumber = rdr["AccountNumber"].ToString(),
                            TransactionType = rdr["TransactionType"].ToString(),
                            TransactionAmount = Convert.ToDouble(rdr["TransactionAmount"]),
                            TransactionDate = Convert.ToDateTime(rdr["TransactionDate"])
                        };

                        transactions.Add(trans);
                    }

                    rdr.Close();
                }
            }            
            catch {
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }

            return transactions;
        }
    }
}
