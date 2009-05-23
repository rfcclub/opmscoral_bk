using System;
using System.Collections.Generic;

namespace AppFrame.Utility.Mapper
{
    public class MapperRepository
    {
        private Dictionary<string, object> mapperList = new Dictionary<string, object>();
        private static MapperRepository repository = null;

        // register new mapper
        
        public virtual void RegisterMappers()
        {
            //Register(new BaseUserMapper());
            //Register(new RoleMapper());
        }
        public static MapperRepository Instance()
        {
            if (repository == null)
            {
                repository = new MapperRepository();
                repository.RegisterMappers();
            }
            return repository;
        }
        /// <summary>
        ///     
        /// </summary>
        /// <param name="mapper" type="Mapper">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        public void Register<U,V>(BaseMapper<U,V> mapper)
        {
            string key = typeof(V).FullName;
            mapperList.Add(key, mapper);
        }
        public void Remove(Type type)
        {            
            mapperList.Remove(type.FullName);
        }
        public object Get(string name)
        {
            return mapperList[name];
        }
    }
}
