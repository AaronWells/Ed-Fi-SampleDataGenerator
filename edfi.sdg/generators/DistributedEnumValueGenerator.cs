﻿using edfi.sdg.utility;

namespace edfi.sdg.generators
{
    using System;
    using System.Linq;
    using System.Xml.Serialization;

    using edfi.sdg.interfaces;

    [Serializable]
    public class DistributedEnumValueGenerator<T> : Generator where T : struct, IConvertible
    {
        [XmlAttribute]
        public string Property { get; set; }

        public Distribution Distribution { get; set; }
        
        public Quantity Quantity { get; set; }

        public DistributedEnumValueGenerator()
        {
            Distribution = new RangeDistribution(); // RangeDistribution<T>();
            Quantity = new ConstantQuantity() { Quantity = 1 };
        }

        public override object[] Generate(object input, IConfiguration configuration)
        {
            var type = input.GetType();

            if (Property.FirstSegment() != type.Name) return new[] {input};

            var propertyName = Property.LastSegment();
            var property = type.GetProperty(propertyName);

            if (property != null)
            {
                if (property.PropertyType.IsArray)
                {
                    var array = Distribution.Shuffled<T>().Take(Quantity.Next()).ToArray();
                    property.GetSetMethod().Invoke(input, new object[] { array });
                }
                else
                {
                    var single = Distribution.Next<T>();
                    property.GetSetMethod().Invoke(input, new object[] { single });
                    if (type.GetProperty(propertyName + "Specified") != null)
                    {
                        type.GetProperty(propertyName + "Specified")
                            .GetSetMethod()
                            .Invoke(input, new object[] { true });
                    }
                }
            }
            return new[] { input };
        }
    }
}
