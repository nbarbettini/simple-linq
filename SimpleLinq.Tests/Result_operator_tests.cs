using System;
using System.Linq;
using Shouldly;
using Xunit;

namespace SimpleLinq1.Tests
{
    public class Result_operator_tests
    {
        [Fact]
        public void Any_operator()
        {
            var model = Build.ResultQuery(x => x.Any());

            model.ResultOperator.ShouldBe(ResultOperator.Any);
        }

        [Fact]
        public void Count_operator()
        {
            var model = Build.ResultQuery(x => x.Count());

            model.ResultOperator.ShouldBe(ResultOperator.Count);
        }

        [Fact]
        public void First_operator()
        {
            var model = Build.ResultQuery(x => x.First());

            model.ResultOperator.ShouldBe(ResultOperator.First);
            model.DefaultIfEmpty.ShouldBeFalse();
        }

        [Fact]
        public void FirstOrDefault_operator()
        {
            var model = Build.ResultQuery(x => x.FirstOrDefault());

            model.ResultOperator.ShouldBe(ResultOperator.First);
            model.DefaultIfEmpty.ShouldBeTrue();
        }

        [Fact]
        public void Single_operator()
        {
            var model = Build.ResultQuery(x => x.Single());

            model.ResultOperator.ShouldBe(ResultOperator.Single);
            model.DefaultIfEmpty.ShouldBeFalse();
        }

        [Fact]
        public void SingleOrDefault_operator()
        {
            var model = Build.ResultQuery(x => x.SingleOrDefault());

            model.ResultOperator.ShouldBe(ResultOperator.Single);
            model.DefaultIfEmpty.ShouldBeTrue();
        }
    }
}