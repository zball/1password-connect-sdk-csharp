using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace OpConnectSdk.Lib.Filter
{
    public class FilterBuilder<T> {
        private List<string> _tokens;

        public FilterBuilder()
        {
            _tokens = new List<string>();
        }

        public FilterBuilder<T> Field<V>(Expression<Func<T, V>> fieldSelector)
        {
            Type type = typeof(T);

            MemberExpression member = fieldSelector.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a method, not a property.", fieldSelector.ToString()));
            }

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.", fieldSelector.ToString()));
            }

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a property that is not from type {1}.", fieldSelector.ToString(), type));
            }

            var csharpFieldName = propInfo.Name;
            if (csharpFieldName == null || csharpFieldName.Length == 0)
            {
                throw new ArgumentException(string.Format("Failed to retrieve property name from lambda: '{0}'", fieldSelector.ToString()));
            }

            // make the first character lowercase for the SCIM filter
            var scimFieldName = csharpFieldName.Length > 1 ? char.ToLower(csharpFieldName[0]) + csharpFieldName.Substring(1) : csharpFieldName.ToLower();
            _tokens.Add(scimFieldName);

            return this;
        }

        public FilterBuilder<T> Group()
        {
            _tokens.Add("(");
            return this;
        }

        public FilterBuilder<T> GroupEnd()
        {
            _tokens.Add(")");
            return this;
        }

        public FilterBuilder<T> And()
        {
            _tokens.Add(ScimConstants.AND);
            return this;
        }

        public FilterBuilder<T> Or()
        {
            _tokens.Add(ScimConstants.OR);
            return this;
        }

        public FilterBuilder<T> Eq(string value)
        {
            _tokens.Add(ScimConstants.EQUALS);
            _tokens.Add(value);
            return this;
        }

        public FilterBuilder<T> Eq(long value)
        {
            _tokens.Add(ScimConstants.EQUALS);
            _tokens.Add(value.ToString());
            return this;
        }

        public FilterBuilder<T> Contains(string value)
        {
            _tokens.Add(ScimConstants.CONTAINS);
            _tokens.Add(value);
            return this;
        }

        public FilterBuilder<T> StartsWith(string value)
        {
            _tokens.Add(ScimConstants.STARTS_WITH);
            _tokens.Add(value);
            return this;
        }

        public FilterBuilder<T> IsPresent()
        {
            _tokens.Add(ScimConstants.PRESENT);
            return this;
        }

        public FilterBuilder<T> GreaterThan(string value)
        {
            _tokens.Add(ScimConstants.GREATER_THAN);
            _tokens.Add(value);
            return this;
        }

        public FilterBuilder<T> GreaterThan(long value)
        {
            _tokens.Add(ScimConstants.GREATER_THAN);
            _tokens.Add(value.ToString());
            return this;
        }

        public FilterBuilder<T> GreaterThanOrEqual(string value)
        {
            _tokens.Add(ScimConstants.GREATER_THAN_OR_EQUAL);
            _tokens.Add(value);
            return this;
        }

        public FilterBuilder<T> GreaterThanOrEqual(long value)
        {
            _tokens.Add(ScimConstants.GREATER_THAN_OR_EQUAL);
            _tokens.Add(value.ToString());
            return this;
        }

        public FilterBuilder<T> LessThan(string value)
        {
            _tokens.Add(ScimConstants.LESS_THAN);
            _tokens.Add(value);
            return this;
        }

        public FilterBuilder<T> LessThan(long value)
        {
            _tokens.Add(ScimConstants.LESS_THAN);
            _tokens.Add(value.ToString());
            return this;
        }

        public FilterBuilder<T> LessThanOrEqual(string value)
        {
            _tokens.Add(ScimConstants.LESS_THAN_OR_EQUAL);
            _tokens.Add(value);
            return this;
        }

        public FilterBuilder<T> LessThanOrEqual(long value)
        {
            _tokens.Add(ScimConstants.LESS_THAN_OR_EQUAL);
            _tokens.Add(value.ToString());
            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < _tokens.Count; i++)
            {
                var prevToken = i > 0 ? _tokens[i - 1] : null;
                var token = _tokens[i];

                // Don't add a space between grouping symbols and their contents
                if (prevToken != "(" && token != ")")
                {
                    sb.Append(" ");
                }

                sb.Append(token);
            }

            return sb.ToString().Trim();
        }
    }
}
