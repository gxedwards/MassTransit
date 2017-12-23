﻿// Copyright 2007-2016 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.EntityFrameworkCoreIntegration
{
    using GreenPipes.Internals.Extensions;
    using GreenPipes.Internals.Reflection;

    using MassTransit.Saga;

    using Microsoft.EntityFrameworkCore;

    public static class SagaClassMapping
    {
        public static void ConfigureDefaultSagaMap<T>(this ModelBuilder modelBuilder)
            where T : class, ISaga
        {
            ReadWriteProperty<T> property;
            if (!TypeCache<T>.ReadWritePropertyCache.TryGetProperty("CorrelationId", out property))
                throw new ConfigurationException("The CorrelationId property must be read/write for use with Entity Framework. Add a setter to the property.");

            modelBuilder.Entity<T>().HasKey(t => t.CorrelationId);

            modelBuilder.Entity<T>().Property(t => t.CorrelationId)
                .ValueGeneratedNever();
        } 
    }
}