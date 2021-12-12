using System.Data.SqlClient;
using System.Reflection;


namespace Repository.Repository
{
    public class Mapper
    {
        public static T MapperEntity<T>(SqlDataReader dataReader) where T : class
        {
            Type entity = typeof(T);

            PropertyInfo[] properties = entity.GetProperties(BindingFlags.Public | BindingFlags.NonPublic);

            T newEntity = null;

            if (dataReader.Read())
            {
                newEntity = (T)Activator.CreateInstance(entity);

                foreach (PropertyInfo property in properties)
                {
                    string columName = property.Name;

                    if (property.Name == "Id")
                    {
                        columName = entity.Name + property.Name;
                    }

                    property.SetValue(newEntity, dataReader[columName], null);
                }
            }

            return newEntity;
        }

        public static List<T> MapperEntityList<T>(SqlDataReader dataReader)
        {
            List<T> listEntity = new List<T>();

            while (dataReader.Read())
            {
                Type entity = typeof(T);

                PropertyInfo[] properties = entity.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

                T newEntity = (T)Activator.CreateInstance(typeof(T));

                foreach (PropertyInfo property in properties)
                {
                    string columName = property.Name;
                    if (property.Name == "Id")
                    {
                        columName = entity.Name + property.Name;
                    }

                    property.SetValue(newEntity, dataReader[columName], null);
                }

                listEntity.Add(newEntity);
            };

            return listEntity;
        }
    }
}
