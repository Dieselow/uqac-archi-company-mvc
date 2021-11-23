using System;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace archi_company_mvc.Models
{
    public static class SelectListUtils
    {
        public static List<SelectListItem> CreatePropertiesSelectListForType(string type, string selected) {
            PropertyInfo[] typeProperties  = Type.GetType("archi_company_mvc.Models." + type).GetProperties();
            List<SelectListItem> final = new List<SelectListItem>();
            List<PropertyInfo> typePropertyList = typeProperties.ToList();

            //Creation de la liste
            foreach (PropertyInfo p in typePropertyList) {
                if (Attribute.IsDefined(p, typeof(SearchableAttribute))) {
                    SelectListItem newItem = new SelectListItem();
                    //On est dans un attribut searchable, on va voir si dans ses champs il y'a le default:
                    newItem.Value = GetDefaultSearchAttribute(p);

                    if (Attribute.IsDefined(p, typeof(DisplayAttribute))) {
                        string displayName = p.GetCustomAttributes(typeof(DisplayAttribute)).Cast<DisplayAttribute>().Single().Name;
                        newItem.Text = displayName;
                    } else {
                        newItem.Text = p.Name;
                    }

                    if (newItem.Value.Equals(selected)) {
                        newItem.Selected = true;
                    }
                    final.Add(newItem);
                }
            }
            return final;
        }

        private static string GetDefaultSearchAttribute(PropertyInfo p){
            foreach (PropertyInfo pp in p.PropertyType.GetProperties()) {
                    //Si un attribut enfant a le default search
                    var attr = pp.GetCustomAttributes(typeof(SearchableAttribute), true);
                    if (attr.Length > 0) {
                        if (((SearchableAttribute)attr[0]).DefaultSearch) {
                        return p.Name + "." + GetDefaultSearchAttribute(pp) ;
                    }
                } 
            }
            return p.Name;
        }

        public static IQueryable<T> DynamicWhere<T>(IQueryable<T> src, string propertyName, string value)
        {
            var pe = Expression.Parameter(typeof(T), "t");
            var left = GetExpressionPropertyNested(pe, typeof(T), propertyName);

            Type leftType = ((PropertyInfo)((MemberExpression)left).Member).PropertyType;
            Expression<Func<T, bool>> predicate;
            if (leftType.Equals(typeof(string))) {
                var right = Expression.Constant(value);
                MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var condition = Expression.Call(left, method, right);
                predicate = Expression.Lambda<Func<T, bool>>(condition, pe);
                return src.Where(predicate);
                
            } else if (leftType.Equals(typeof(DateTime))){
                DateTime formattedDate;
                if (DateTime.TryParseExact(value, "dd/MM/yyyy",System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out formattedDate)) {
                    var right = Expression.Constant(formattedDate );
                    var condition = Expression.Equal(left,right);
                    predicate = Expression.Lambda<Func<T, bool>>(condition, pe);
                    return src.Where(predicate);
                }
                
               
            } 
            return src;
        }
        

        public static Expression GetExpressionPropertyNested(Expression previous, Type src, string propName)
        {
            if (src == null) throw new ArgumentException("Value cannot be null.", "src");
            if (propName == null) throw new ArgumentException("Value cannot be null.", "propName");

            if(propName.Contains("."))//complex type nested
            {
                var temp = propName.Split(new char[] { '.' }, 2);
                return GetExpressionPropertyNested(GetExpressionPropertyNested(previous, src, temp[0]), src.GetProperty(temp[0]).PropertyType, temp[1]);
            }
            else
            {
                var prop = Expression.Property(previous, src.GetProperty(propName));
                return prop;
            }
        }

    }
}