using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentValidation;

namespace Riders.Tweakbox.API.Application.Models
{
    public static class Validator
    {
        public static Dictionary<Type, object> _validators = new Dictionary<Type, object>();

        static Validator()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(x => !x.IsAbstract && !x.IsInterface && x.IsAssignableToGenericType(typeof(AbstractValidator<>)));
            foreach (var type in types)
            {
                var validator = Activator.CreateInstance(type);
                _validators[type.BaseType.GenericTypeArguments[0]] = validator;
            }
        }

        /// <summary>
        /// Gets a validator for the given type.
        /// </summary>
        public static AbstractValidator<T> Get<T>() => (AbstractValidator<T>) _validators[typeof(T)];

        private static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type baseType = givenType.BaseType;
            if (baseType == null) 
                return false;

            return IsAssignableToGenericType(baseType, genericType);
        }
    }
}
