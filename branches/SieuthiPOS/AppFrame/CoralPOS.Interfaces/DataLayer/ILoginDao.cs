using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using AppFrame;
using CoralPOS.Interfaces.Model;

namespace CoralPOS.Interfaces.DataLayer
{
    public interface ILoginDao 
    {
        LoginModel getInfo(string Username, string Password);
        LoginModel getUser(string Username);
        LoginModel FindById(object id);
        LoginModel Add(LoginModel data);
        void Update(LoginModel data);
        void Delete(LoginModel data);
        void DeleteById(object id);
        IList FindAll(ObjectCriteria criteria);
    }
}