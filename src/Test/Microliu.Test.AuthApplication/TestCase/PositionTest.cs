using Microliu.Auth.Application;
using Microliu.Auth.Domain;
using Microliu.Auth.Domain.Entities;
using Microliu.Auth.Domain.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Microliu.Test.AuthApplication
{
    public class PositionTest
    {
        private IAuthService _authApplication;
        private ITestOutputHelper _output;
        public PositionTest(ITestOutputHelper testOutputHelper)
        {
            _authApplication = ApplicationFactory.GetIAuthApplication();
            _output = testOutputHelper;
        }

        [Fact]
        public void CreatePosition()
        {
            var craetePosition = new CreatePositionModel
            {
                Name = Guid.NewGuid().ToString("N").Substring(1, 5),
                Sort = 100,
                IsEnable = 1
            };
            _authApplication.CreatePosition(craetePosition);
        }

        [Fact]
        public void GetPositions()
        {
            var positions = _authApplication.GetPositions() as IQueryable<dynamic>;
            foreach (var prop in positions.FirstOrDefault().GetType().GetProperties())
            {
                _output.WriteLine($"{prop.Name}= {prop.GetValue(positions.FirstOrDefault()).ToString()}");
            }
            //_output.WriteLine(positions.FirstOrDefault().GetType());
        }

        //[Fact]
        [Theory]
        [ClassData(typeof(GetPositionsTestData))]
        // ClassData 传值可参考：https://juejin.im/post/5a541ae6f265da3e4e257c8c
        public void GetPositionsByPage(SearchPositionModel input)
        {
            var positions = _authApplication.GetPositions(input) as IQueryable<Position>;

            foreach (var position in positions)
            {
                foreach (var prop in position.GetType().GetProperties())
                {
                    _output.WriteLine($"{prop.Name}= {prop.GetValue(position).ToString()}");
                }
                _output.WriteLine("");
            }
        }


        private class GetPositionsTestData : IEnumerable<object[]>
        {
            private readonly List<object[]> _data = new List<object[]>
            {
              new object[]{ new SearchPositionModel { PageIndex = 1, PageSize = 10 } },
              new object[]{ new SearchPositionModel { PageIndex = 1, PageSize = 20 } }
            };

            public IEnumerator<object[]> GetEnumerator()
            {
                return _data.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        //[Fact]
        [Theory]
        [InlineData("000091a9e5e24594b355c3c8e6d33f84")] // 01.831 ,01.709
        public void GetPosition(string id)
        {
            var position = _authApplication.GetPosition(id) as Position;
            foreach (var prop in position.GetType().GetProperties())
            {
                _output.WriteLine($"{prop.Name}= {prop.GetValue(position).ToString()}");
            }
        }


        // 测试 yield的使用，与当前测试无关
        [Fact]
        public void PrintNumbers()
        {
            var nums = GetNumber();
            foreach (var item in nums)
            {
                _output.WriteLine(item.ToString());
            }
        }
        private IEnumerable<int>  GetNumber()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i > 4)
                {
                    yield break;
                }
                yield return i;
            }
        }

    }
}
