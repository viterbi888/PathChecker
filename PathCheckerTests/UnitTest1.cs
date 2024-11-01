namespace PathCheckerTests
{
    public class Tests
    {
        [Test]
        public void Should_return_true_in_CanReachTarget_for_valid_path_and_target()
        {
            //arrange 
            var commands = "UUURDDLLRU";

            //act 
            var reached = PathChecker.Program.CanReachTarget(commands, (4, 2), new List<Tuple<int, int>>());

            //assert
            Assert.IsTrue(reached);
        }

        [Test]
        public void Should_return_false_in_CanReachTarget_for_valid_path_and_target()
        {
            //arrange 
            var commands = "UUULDDLLRU";

            //act 
            var reached = PathChecker.Program.CanReachTarget(commands, (4, 2), new List<Tuple<int, int>>());

            //assert
            Assert.IsFalse(reached);
        }

        [Test]
        public void Should_return_false_in_CanReachTarget_if_out_of_bounds()
        {
            //arrange 
            var commands = "UUUUUUUUUUUUUUUUUUUUUUUUUUURRUUUUUUUUUUUUULLRU";

            //act 
            var reached = PathChecker.Program.CanReachTarget(commands, (14, -1), new List<Tuple<int, int>>());

            //assert
            Assert.IsFalse(reached);
        }

        [Test]
        public void Should_return_false_in_CanReachTarget_if_obstacle_in_cell()
        {
            //arrange 
            var commands = "UUURDDLLRU";

            //act 
            var reached = PathChecker.Program.CanReachTarget(commands, (4, 2), new List<Tuple<int, int>> { new Tuple<int, int>(3, 1) });

            //assert
            Assert.IsFalse(reached);
        }

        [Test]
        public void Should_return_corrected_command_in_FixPath()
        {
            //arrange 
            var commands = "UDURDDLLRU";

            //act 
            var fixedCommands = PathChecker.Program.FixPath(commands, (4, 2), new List<Tuple<int, int>>());

            //assert
            Assert.AreEqual("UUURDDLLRU", fixedCommands);
        }
    }
}