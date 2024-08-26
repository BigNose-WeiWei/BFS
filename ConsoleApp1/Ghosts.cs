using System;
using System.Runtime;

namespace ConsoleApp1;

public class Ghosts
{
    public World World { get; set; }
    public List<Ghost> Members { get; set; }

    public static ConsoleColor[] Colors = 
        [
            ConsoleColor.Red,
            ConsoleColor.Green,
            ConsoleColor.Blue,
            ConsoleColor.Cyan,
            ConsoleColor.Black,
        ];

    public Ghosts(World world, int ghostCount)
    {
        World = world;
        Members = new List<Ghost>();
        for (int i = 0; i < ghostCount; i++)
        {
            Members.Add(new Ghost(i));
        }

    }

    public void Reborn(Ghost ghost)
        => Members[ghost.Index] = new Ghost(ghost.Index);

    public void Move(Pacman pacman)
    {
        foreach (var ghost in Members)
        {
            World.showGhost(ghost);

            if (!StepMovable(ghost)) continue;

            if (ghost.Weak) continue;

            //var direction = World.GetRandomDirection(ghost.Position);
            var direction = World.GhostsAlgorithmDirection(ghost, pacman);

            var newPosition = World.GetPosition(ghost.Position, direction);
            World.ClearGhost(ghost);
            ghost.Position = newPosition;
            World.showGhost(ghost);
        }
    }

    private bool StepMovable(Ghost ghost)
    {
        ghost.StepFrame = ++ghost.StepFrame % ghost.MaxStepFrame;
        if (ghost.StepFrame == 0) return true;
        return false;
    }
}
public class Ghost(int index)
{
    public int Index = index;
    public Position Position = World.GhostStartPositions[index];
    public Direction Direction = Direction.None;
    public ConsoleColor Color = Ghosts.Colors[index];
    public bool Weak { get;  set; }
    public int StepFrame;
    public int MaxStepFrame = 4;
    public Position? FirstDestination = World.GhostFirstDestinations[index];
}