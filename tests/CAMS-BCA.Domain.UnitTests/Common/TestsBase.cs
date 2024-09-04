using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace CAMS_BCA.Domain.UnitTests.Common
{
    public abstract class TestsBase
    {
        protected IFixture Fixture { get; }

        protected TestsBase()
        {
            this.Fixture = new Fixture();

            this.Fixture.Customize(new AutoNSubstituteCustomization());

            // this.Fixture.Customize<DbConnection>(
            //    composer => composer.FromFactory(() =>
            //        new MockDbConnection
            //        {
            //            HasValidSqlServerCommandText = true
            //        }));
        }
    }
}
