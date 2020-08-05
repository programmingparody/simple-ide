using Xunit;
namespace SIDE.Tests.Usecases {
    public class RunGolangCode {
        private string testImplementation(IProvider Provider, string mainFile) {
            //1. User creates an emtpy project directory from a `Provider` with specified settings *IE: Language = Golang*
            var P = Provider.CreateNewProject(uploadedData, new ProjectSettings{
                Language = "golang"
            });

            //2. Through any concerete interface, user write's a hello world app
            P.SaveFile("main.go", @"
                package main
                import ""fmt""
                func main() {
                    fmt.Print(""hello golang"")
                }
            ");

            //3. User Create an `Environment` for **P** to build and run their `Project`
            var E = Provider.CreateNewEnvironment(P);

            //4. User run's their project and gets results
            var BR = E.Run(P);
            
            //5. User Reviews their results
            var standardOutput = BR.StandardOut;
            return standardOutput;
        }
        [Fact]
        public void HelloWorld() {
            IProvider provider;

            string expected = "hello golang";
            string result = testImplementation(provider, @"
                package main
                import ""fmt""
                func main() {
                    fmt.Print(""hello golang"")
                }
            ");

            Assert.True(expected == result, $"{expected} != ${result}");
        }
    }
}
