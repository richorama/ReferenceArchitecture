using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Two10.TaskList.Tests
{


    /// <summary>
    /// http://stackoverflow.com/questions/3463017/unit-testing-with-ef4-code-first-and-repository
    /// </summary>
    public class InMemoryDbSet<T> : IDbSet<T> where T : class
    {

        private int GetKey(T t)
        {
            return (int)this.KeyField.GetValue(t, null);
        }

        private void SetKey(T t, int value)
        {
            this.KeyField.SetValue(t, value, null);
        }


        private int GetNextKey()
        {
            if (this.data.Count == 0)
            {
                return 1;
            }
            return this.data.Max(d => GetKey(d)) + 1;
        }

        public void SetPrimaryKeyField(Expression<Func<T, int>> propertyLambda)
        {
            Type type = typeof(T);

            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null) throw new ArgumentException(string.Format("Expression '{0}' refers to a method, not a property.", propertyLambda.ToString()));

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null) throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.", propertyLambda.ToString()));

            this.KeyField = propInfo;
        }

        public PropertyInfo KeyField { get; private set; }
        readonly HashSet<T> data;
        readonly IQueryable query;

        public InMemoryDbSet()
        {
            data = new HashSet<T>();
            query = data.AsQueryable();
        }

        public T Add(T entity)
        {
            int key = this.GetNextKey();
            SetKey(entity, key);
            data.Add(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            data.Add(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public virtual T Find(params object[] keyValues)
        {
            if (keyValues.Length != 1) throw new ArgumentException("Must contain one key", "keyValues");
            int key = (int)keyValues[0];
            return this.data.Where(d => GetKey(d) == key).FirstOrDefault();
        }

        public System.Collections.ObjectModel.ObservableCollection<T> Local
        {
            get { return new System.Collections.ObjectModel.ObservableCollection<T>(data); }
        }

        public T Remove(T entity)
        {
            data.Remove(entity);
            return entity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        public Type ElementType
        {
            get { return query.ElementType; }
        }

        public Expression Expression
        {
            get { return query.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return query.Provider; }
        }
    }
}
