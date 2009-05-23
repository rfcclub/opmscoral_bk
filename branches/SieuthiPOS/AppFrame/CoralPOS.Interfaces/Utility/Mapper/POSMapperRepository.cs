using System;
using System.Collections.Generic;
using System.Text;
using AppFrame.Common;
using AppFrame.Utility.Mapper;

namespace CoralPOS.Interfaces.Utility.Mapper
{
    public class POSMapperRepository  : MapperRepository
    {
        private POSMapperRepository()
        {
            
        }

        public override void RegisterMappers()
        {
            base.RegisterMappers();
            Register(new BaseUserMapper());
            Register(new RoleMapper());
        }

        // register new mapper
    }
}