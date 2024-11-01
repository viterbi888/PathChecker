namespace PathChecker
{
    public class Program
    {

        public static string FixPath(string commands, (int x, int y) target, List<Tuple<int, int>> obstacles)
        {
            commands ??= string.Empty;
            obstacles ??= new List<Tuple<int, int>>();

            if (CanReachTarget(commands, target, obstacles))
            {
                return commands;
            }

            char[] possibleCommands = { 'L', 'R', 'U', 'D' };
            char[] commandArray = commands.ToCharArray();

            for (int i = 0; i < commandArray.Length; i++)
            {
                // Store the original command
                char originalCommand = commandArray[i];

                // Try each possible command as a replacement
                foreach (char newCommand in possibleCommands)
                {
                    if (newCommand == originalCommand) continue; 
                    commandArray[i] = newCommand;
                    string modifiedCommands = new string(commandArray);
                    if (CanReachTarget(modifiedCommands, target, obstacles))
                    {
                        return modifiedCommands;
                    }
                }

                // Restore the original command before moving to the next position
                commandArray[i] = originalCommand;
            }

            return commands;
        }

        public static bool CanReachTarget(string commands, (int x, int y) target, List<Tuple<int, int>> obstacles)
        {
            int posX = 0;
            int posY = 0;

            var currentDirection = Direction.Right;
            var positionsUpdateForX = new Dictionary<Direction, int> {
                { Direction.Right, 1 } ,
                { Direction.Up, 0 } ,
                { Direction.Left, -1 } ,
                { Direction.Down, 0 } ,
            };
            var positionsUpdateForY = new Dictionary<Direction, int> {
                { Direction.Right, 0 } ,
                { Direction.Up, 1 } ,
                { Direction.Left, 0 } ,
                { Direction.Down, -1 } ,
            };

            foreach (char command in commands)
            {
                if (command == 'L' || command == 'R')
                {
                    currentDirection = UpdateDirection(currentDirection, command);
                }
                else if (command == 'U')
                {
                    // Move forward in the current direction
                    posX += positionsUpdateForX[currentDirection];
                    posY += positionsUpdateForY[currentDirection];
                }
                else if (command == 'D')
                {
                    // Move backward in the current direction
                    posX -= positionsUpdateForX[currentDirection];
                    posY -= positionsUpdateForY[currentDirection];
                }

                if (obstacles.Any(x => x.Item1 == posX && x.Item2 == posY))
                {
                    return false;
                }

                if (Math.Abs(posX) > 20 || Math.Abs(posY) > 20) // out of 40x40 square 
                {
                    return false;
                }
            }           

            return posX == target.x && posY == target.y;
        }

        private static Direction UpdateDirection(Direction direction, char command)
        {
            switch (command)
            {
                case 'L':
                    switch (direction)
                    {
                        case Direction.Right:
                            return Direction.Up;
                        case Direction.Up:
                            return Direction.Left;
                        case Direction.Left:
                            return Direction.Down;
                        case Direction.Down:
                            return Direction.Right;
                    };
                    break;
                case 'R':
                    switch (direction)
                    {
                        case Direction.Right:
                            return Direction.Down;
                        case Direction.Down:
                            return Direction.Left;
                        case Direction.Left:
                            return Direction.Up;
                        case Direction.Up:
                            return Direction.Right;
                    };
                    break;
            }

            return direction;
        }
    }

    public enum Direction
    {
        Up,
        Left,
        Down,
        Right
    }
}