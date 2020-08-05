using Xunit;

namespace SIDE.Tests.Usecases {
    public class SanityCheck {
        [Fact]
        public void TrueEqualsTrue() {
            //If this fails you might wanna throw your computer in the trash.
            Assert.True(true == true, "This message shouldn't be seen");
            Assert.False(true == false, "This message shouldn't be seen");
            Assert.False(true == false, "This message shouldn't be seen");
        }
    }
}