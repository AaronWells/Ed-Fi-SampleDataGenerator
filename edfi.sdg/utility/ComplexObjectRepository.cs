﻿using System;
using System.Data.SqlClient;
using EdFi.SampleDataGenerator.Data;
using EdFi.SampleDataGenerator.Models;

namespace EdFi.SampleDataGenerator.Utility
{
    public class ComplexObjectRepository
    {
        public void Save(ComplexObjectType obj)
        {
            using (var model = new DataModel())
            {
                model.Database.ExecuteSqlCommand("dbo.upsertComplexObject @identifier, @className, @xml",
                    new object[]
                        {
                            new SqlParameter("@identifier", obj.id),
                            new SqlParameter("@className", obj.GetType().ToString()),
                            new SqlParameter("@xml", obj.ToXml())
                        });
            }
        }

        public ComplexObjectType GetById(string identifier)
        {
/*
            using (var model = new DataModel())
            {
                var result = model.ComplexObjects.FirstOrDefault(x => x.Id == identifier);
                return result != null ? ComplexObjectType.FromXml(result.Xml) : null;
            }
*/
            throw new NotImplementedException();
        }

        public ComplexObjectType[] GetByExample(dynamic obj)
        {
            throw new NotImplementedException();
        }
    }
}
