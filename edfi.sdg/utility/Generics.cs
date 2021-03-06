﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EdFi.SampleDataGenerator.Utility
{
    public static class Generics
    {
        public static bool IsSubClassOfGeneric(this Type child, Type parent)
        {
            if (child == parent)
                return false;

            if (child.IsSubclassOf(parent))
                return true;

            var parameters = parent.GetGenericArguments();
            var isParameterLessGeneric = !(parameters.Length > 0 &&
                ((parameters[0].Attributes & TypeAttributes.BeforeFieldInit) == TypeAttributes.BeforeFieldInit));

            while (child != null && child != typeof(object))
            {
                var cur = GetFullTypeDefinition(child);
                if (parent == cur || (isParameterLessGeneric && cur.GetInterfaces().Select(GetFullTypeDefinition).Contains(GetFullTypeDefinition(parent))))
                    return true;
                if (!isParameterLessGeneric)
                    if (GetFullTypeDefinition(parent) == cur && !cur.IsInterface)
                    {
                        if (VerifyGenericArguments(GetFullTypeDefinition(parent), cur))
                            return true;
                    }
                    else
                    {
                        var list = child.GetInterfaces()
                            .Where(i => GetFullTypeDefinition(parent) == GetFullTypeDefinition(i));
                        
                        if (list.Any(item => VerifyGenericArguments(parent, item)))
                        {
                            return true;
                        }
                    }
                child = child.BaseType;
            }

            return false;
        }

        private static Type GetFullTypeDefinition(Type type)
        {
            return type.IsGenericType ? type.GetGenericTypeDefinition() : type;
        }

        private static bool VerifyGenericArguments(Type parent, Type child)
        {
            var childArguments = child.GetGenericArguments();
            var parentArguments = parent.GetGenericArguments();
            if (childArguments.Length == parentArguments.Length) 
                return childArguments.Where((t, i) => 
                        t.Assembly == parentArguments[i].Assembly 
                        && t.Name == parentArguments[i].Name 
                        && t.Namespace == parentArguments[i].Namespace
                    ).Any();

            return false;
        }
    }

    public static class GenericExtensions
    {
        public static bool In<T>(this T value, IEnumerable<T> list)
        {
            return list != null && list.Contains(value);
        }

        public static bool In<T>(this T value, params T[] list)
        {
            return value.In((IEnumerable<T>)list);
        }
    }
}
