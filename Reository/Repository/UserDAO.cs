using Library.Entity;
using Library.RepositoryInterface;
using Library.Utils;
using Repository.Repository;
using System.Data;
using System.Data.SqlClient;


namespace Library.Repository
{
    public class UserDAO : BaseRepository, IUserRepository
    {
        public TransactionDB TransactionDB { get; set; }
        public UserDAO(TransactionDB transactionDB)
        {
            TransactionDB = transactionDB;
        }
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.GetConnection();
            }
        }

        public bool Save(User user)
        {
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@parameterName", user.Name));

                ExecuteProcedure(sqlParameters, "SaveUser", TransactionDB);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public User GetUserByName(string Name)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("GetUserByName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@parameterName", Name);
                SqlDataReader dr = cmd.ExecuteReader();

                User user = Mapper.MapperEntity<User>(dr);

                return user;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public static List<User> GetUsers()
        {
            List<User> users = null;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("GetUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                users = Mapper.MapperEntityList<User>(dr);
            }

            return users;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}