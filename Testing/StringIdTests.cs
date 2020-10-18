using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace StringIdLibrary
{
    [TestClass]
    public class StringIdTests
    {
        [TestMethod]
        public void RandomIdsAreUnique()
        {
            HashSet<string> test = new HashSet<string>();

            const int iterations = 1_000_000;
            for (int i = 0; i < iterations; i++) test.Add(StringId.New(9));

            // if the generated Ids are unique enough, then the hash set count will equal the iteration count
            Assert.IsTrue(test.Count == iterations);
            Assert.IsTrue(test.All(id => id.Length == 9));

            // We're assuming that a million 9-character Ids should be unique.
            // We'd likely have to tune this assumption for different Id lengths and number of iterations
        }        

        [TestMethod]
        public void ChainResults()
        {
            string result = new StringIdBuilder()
                .Add(4, StringIdRanges.Upper)
                .Add("-")
                .Add(4, StringIdRanges.Upper)
                .Add("-")
                .Add(4, StringIdRanges.Upper)
                .Build();

            Assert.IsTrue(result.Length == 14);
        }

        [TestMethod]
        public void CreatePasswords()
        {
            var pwds = new HashSet<string>();
            const int iterations = 10;
            for (int i = 0; i < iterations; i++) pwds.Add(StringId.NewPassword());

            Assert.IsTrue(pwds.Count == iterations);
            Assert.IsTrue(pwds.All(id => id.Length == 16));
        }
    }
}
