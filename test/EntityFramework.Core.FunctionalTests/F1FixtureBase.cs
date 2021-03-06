// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.Entity.FunctionalTests.TestModels.ConcurrencyModel;

namespace Microsoft.Data.Entity.FunctionalTests
{
    public abstract class F1FixtureBase<TTestStore>
        where TTestStore : TestStore
    {
        public abstract TTestStore CreateTestStore();

        public abstract F1Context CreateContext(TTestStore testStore);

        protected virtual void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Uncomment when complex types are supported
            //builder.ComplexType<Location>();
            modelBuilder.Entity<Chassis>(b =>
                {
                    b.Key(c => c.TeamId);
                    b.Property(e => e.Version)
                        .StoreComputed()
                        .ConcurrencyToken();
                });

            modelBuilder.Entity<Driver>(b =>
                {
                    b.Property(e => e.Version)
                        .StoreComputed()
                        .ConcurrencyToken();
                });

            modelBuilder.Entity<Engine>(b =>
                {
                    b.Property(e => e.EngineSupplierId).ConcurrencyToken();
                    b.Property(e => e.Name).ConcurrencyToken();
                });

            // TODO: Complex type
            // .Property(c => c.StorageLocation);
            modelBuilder.Ignore<Location>();

            modelBuilder.Entity<EngineSupplier>();

            modelBuilder.Entity<Gearbox>();

            // TODO: Complex type
            //builder
            //    .ComplexType<Location>()
            //    .Properties(ps =>
            //        {
            //            // TODO: Use lambda expression
            //            ps.Property<double>("Latitude", concurrencyToken: true);
            //            // TODO: Use lambda expression
            //            ps.Property<double>("Longitude", concurrencyToken: true);
            //        });

            modelBuilder.Entity<Sponsor>(b =>
                {
                    b.Property(e => e.Version)
                        .StoreComputed()
                        .ConcurrencyToken();
                });

            // TODO: Complex type
            //builder
            //    .ComplexType<SponsorDetails>()
            //    .Properties(ps =>
            //        {
            //            ps.Property(s => s.Days);
            //            ps.Property(s => s.Space);
            //        });
            modelBuilder.Ignore<SponsorDetails>();

            modelBuilder.Entity<Team>(b =>
                {
                    b.Property(t => t.Version)
                        .StoreComputed()
                        .ConcurrencyToken();

                    b.Reference(e => e.Gearbox).InverseReference().ForeignKey<Team>(e => e.GearboxId);
                    b.Reference(e => e.Chassis).InverseReference(e => e.Team).ForeignKey<Chassis>(e => e.TeamId);
                });

            modelBuilder.Entity<TestDriver>();

            modelBuilder.Entity<TitleSponsor>();
            // TODO: Complex type
            // .Property(t => t.Details);

            // TODO: Sponsor * <-> * Team
        }
    }
}
