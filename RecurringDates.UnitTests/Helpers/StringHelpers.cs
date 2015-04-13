using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using RecurringDates.Helpers;

namespace RecurringDates.UnitTests.Helpers
{
    [TestFixture]
    public class StringHelpersTests
    {
        [Test]
        public void Fmt_OnNullString_ThrowsException()
        {
            Action act = () => ((string) null).Fmt("test");

            act.ShouldThrow<ArgumentNullException>();
        }
        [Test]
        public void Fmt_WithSingleArg_Works()
        {
            var result = "aa{0}bb".Fmt("cc");

            result.Should().Be("aaccbb");
        }

        [Test]
        public void Fmt_WithParams_Works()
        {
            var result = "aa{0}bb{1}dd".Fmt("cc","ee");

            result.Should().Be("aaccbbeedd");
        }
        [Test]
        public void Fmt_WithArrayOfParams_Works()
        {
            var result = "aa{0}bb{1}dd".Fmt(new[]{"cc", "ee"});

            result.Should().Be("aaccbbeedd");
        }

        [Test]
        public void IsNullOrEmpty_OnNull_ReturnsTrue()
        {
            ((string) null).IsNullOrEmpty().Should().BeTrue();
        }
        [Test]
        public void IsNullOrEmpty_OnEmpty_ReturnsTrue()
        {
            string.Empty.IsNullOrEmpty().Should().BeTrue();
        }
        [Test]
        public void IsNullOrWhiteSPace_OnNull_ReturnsTrue()
        {
            ((string)null).IsNullOrEmpty().Should().BeTrue();
        }
        [Test]
        public void IsNullOrWhiteSpace_OnEmpty_ReturnsTrue()
        {
            string.Empty.IsNullOrWhiteSpace().Should().BeTrue();
        }

        [Test]
        public void IsNullOrEmpty_OnWhitespace_ReturnsFalse()
        {
            (" ").IsNullOrEmpty().Should().BeFalse();
        }
        [Test]
        public void IsNullOrWhiteSpace_OnWhitespace_ReturnsTrue()
        {
            (" ").IsNullOrWhiteSpace().Should().BeTrue();
        }
        [Test]
        public void IsNullOrEmpty_OnNormalString_ReturnsFalse()
        {
            "test".IsNullOrEmpty().Should().BeFalse();
        }
        [Test]
        public void IsNullOrWhiteSPace_OnNormalString_ReturnsFalse()
        {
            " test ".IsNullOrWhiteSpace().Should().BeFalse();
        }
    }

    [TestFixture]
    public class StringHelpersTests_StringJoin
    {
        [Test]
        public void WithSingleElement_NoSeparatorReturned()
        {
            new[] {"test"}.StringJoin().Should().Be("test");
        }
        [Test]
        public void WithSeveralElements_NoSeparatorSpecified_CommaIsUsedAsSeparator()
        {
            new[] {"test", "test2", "test3"}.StringJoin()
                .Should().Be("test,test2,test3");
        }
        [Test]
        public void WithSeveralNonStringElements_NoSeparatorSpecified_CommaIsUsedAsSeparator()
        {
            new[] { DayOfWeek.Monday, DayOfWeek.Friday}.StringJoin()
                .Should().Be("Monday,Friday");
        }

        [Test]
        public void WithSeveralElements_WithCustomSeparator_ItIsUsed()
        {
            new[] { "test", "test2", "test3" }.StringJoin(":")
                .Should().Be("test:test2:test3");
        }

        [Test]
        public void WithSingleElement_SpecifyingSeparatorAndLeftPadding_OnlyPaddingIsUsed()
        {
            new[] {"test"}.StringJoin(Environment.NewLine, "> ")
                .Should().Be("> test");
        }

        [Test]
        public void WithSingleElement_SpecifyingSeparatorAndBothPaddings_OnlyPaddingIsUsed()
        {
            new[] { "test" }.StringJoin(Environment.NewLine, "<", ">")
                .Should().Be("<test>");
        }

        [Test]
        public void WithSeveralElements_SpecifyingSeparatorAndLeftPadding_SeparatorAndPaddingAreUsed()
        {
            new[] {"test", "test2"}.StringJoin(Environment.NewLine, "> ")
                .Should().Be(@"> test" + Environment.NewLine + "> test2");
        }

        [Test]
        public void WithSeveralElement_SpecifyingSeparatorAndBothPaddings_SeparatorAndPaddingsAreUsed()
        {
            new[] { "test", "test2" }.StringJoin(",", "<", ">")
                .Should().Be("<test>,<test2>");
        }
    }
}
