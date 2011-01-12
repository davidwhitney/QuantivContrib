using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text;
using NHibernate.Engine;
using NHibernate.Exceptions;
using NHibernate.Id;
using NHibernate.Type;
using IConfigurable = NHibernate.Id.IConfigurable;

namespace QuantivContrib.NHibernateCompatability
{
    public class QuantivIdGenerator: IIdentifierGenerator, IConfigurable
    {
        private string _counterRef;

        private long _next;
        private string _sql;
        private Type _returnClass;

        public void Configure(IType type, IDictionary<string, string> parms, NHibernate.Dialect.Dialect dialect)
        {
            if(!parms.ContainsKey("counterRef"))
            {
                throw new InvalidOperationException("You must specify a counterRef param that exists in QTV_Counters for this generator to work.");
            }

            _counterRef = parms["counterRef"];
            _returnClass = type.ReturnedClass;

            var buffer = new StringBuilder ("DECLARE @nextCounterValue int  ")
                                .AppendLine("SET @nextCounterValue = (select countervalue+1 as nextValue from qtv_counter where counterref='" +_counterRef + "') ")
                                .AppendLine("UPDATE qtv_counter set countervalue = @nextCounterValue where counterref='" + _counterRef +"' ")
                                .AppendLine("SELECT @nextCounterValue ");
            
            _sql = buffer.ToString();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public object Generate(ISessionImplementor session, object obj)
        {
            if (_sql != null)
            {
                GetNext(session);
            }
            return IdentifierGeneratorFactory.CreateNumber(_next, _returnClass);
        }

        private void GetNext(ISessionImplementor session)
        {
            try
            {
                var connection = session.Factory.ConnectionProvider.GetConnection();
                var dbCommand = connection.CreateCommand();
                dbCommand.CommandText = _sql;
                dbCommand.CommandType = CommandType.Text;
                try
                {
                    var dataReader = dbCommand.ExecuteReader();
                    try
                    {
                        if (dataReader.Read())
                        {
                            _next = !dataReader.IsDBNull(0) ? Convert.ToInt64(dataReader.GetValue(0)) : 1L;
                        }
                        else
                        {
                            _next = 1L;
                        }
                        _sql = null;
                    }
                    finally
                    {
                        dataReader.Close();
                    }
                }
                finally
                {
                    session.Factory.ConnectionProvider.CloseConnection(connection);
                }
            }
            catch (DbException sqle)
            {
                throw ADOExceptionHelper.Convert(session.Factory.SQLExceptionConverter, sqle, "could not fetch initial value for increment generator");
            }
        }
    }
}
