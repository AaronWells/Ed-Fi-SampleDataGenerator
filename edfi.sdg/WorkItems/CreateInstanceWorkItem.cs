﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml.Serialization;
using EdFi.SampleDataGenerator.Configurations;
using EdFi.SampleDataGenerator.Models;
using EdFi.SampleDataGenerator.Quantity;
using EdFi.SampleDataGenerator.Utility;

namespace EdFi.SampleDataGenerator.WorkItems
{
    /// <summary>
    /// Create a number of objects of type T and put them on the work queue
    /// </summary>
    [Serializable]
    public class CreateInstanceWorkItem : WorkItem
    {
        /// <summary>
        /// Number of objects to create
        /// </summary>
        public QuantityBase QuantitySpecifier { get; set; }

        private Type _assignedType = typeof(ComplexObjectType);

        /// <summary>
        /// The full name of the type to be generated by the work item
        /// </summary>
        [XmlAttribute]
        public string CreatedType
        {
            get { return _assignedType.FullName; }
            set
            {
                var type = Type.GetType(value);

                if (type == null) // try to get it from other available assemblies
                {
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                    foreach (var assembly in assemblies)
                    {
                        type = assembly.GetType(value);
                        if (type != null) break;
                    }
                }

                if (type == null)
                    throw new ConfigurationErrorsException(string.Format("Cannot find CreatedType: '{0}'", value));

                _assignedType = type;

                // todo: check if the type has the default constructor
                //                var constructor = type.GetConstructor(Type.EmptyTypes);
            }
        }

        /// <summary>
        /// Create a number of objects and place them on the queue, 
        /// or if there are too many, split the task in two and put those tasks back on the queue.
        /// Initialize the Id property.
        /// </summary>
        /// <param name="input">ignored</param>
        /// <param name="configuration">uses MaxQueueWrites property</param>
        /// <returns>an array of work items</returns>
        protected override object[] DoWorkImplementation(object input, IConfiguration configuration)
        {
            var results = new List<object>();
            var qty = QuantitySpecifier.Next();
            if (qty > configuration.MaxQueueWrites)
            {
                results = new object[]
                {
                    new CreateInstanceWorkItem {Id = Id, CreatedType = CreatedType, QuantitySpecifier = new ConstantQuantity {Quantity = qty/2}},
                    new CreateInstanceWorkItem {Id = Id, CreatedType = CreatedType, QuantitySpecifier = new ConstantQuantity {Quantity = qty/2 + qty%2}}
                }.ToList();
            }
            else
            {
                // input object should be always returned to the quote for further updates
                if (input != null) results.Add(input);

                for (var i = 0; i < qty; i++)
                {
                    var instance = CreateInstanceWithId(_assignedType);
                       
                    results.Add(instance);

                    if (input != null && input.IsAssociation())
                    {
                        var referencePropertyName = instance.GetType().Name + "Reference";
                        var referenceObject = CreateReferenceToObject(instance);
                        input.SetPropertyValue(referencePropertyName, referenceObject);
                    }

                    if (input != null && _assignedType.IsAssociation() )
                    {
                        var referencePropertyName = input.GetType().Name + "Reference";
                        var referenceObject = CreateReferenceToObject(input);
                        instance.SetPropertyValue(referencePropertyName, referenceObject);
                    }
                }
            }
            return results.ToArray();
        }

        private object CreateInstanceWithId(Type assignedType)
        {
            var instance = Activator.CreateInstance(assignedType);

            if(instance.GetType().GetProperties().Any(p => p.Name == "id"))
                instance.SetPropertyValue("id", IdentifierGenerator.Create());

            return instance;
        }

        private static object CreateReferenceToObject(object obj)
        {
            var id = obj.GetPropertyValue("id");
            var referencePropertyTypeName = obj.GetType().FullName + "ReferenceType";
            var referencePropertyType = Type.GetType(referencePropertyTypeName);
            
            if (referencePropertyType == null) // try other assemblies
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (var assembly in assemblies)
                {
                    referencePropertyType = assembly.GetType(referencePropertyTypeName);
                    if (referencePropertyType != null) break;
                }
            }

            if (referencePropertyType == null) return null; // or maybe an error condition?

            var referenceInstance = Activator.CreateInstance(referencePropertyType);
            referenceInstance.SetPropertyValue("id", id);
            return referenceInstance;
        }
    }
}
