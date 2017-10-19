using System;
using System.IO;
using System.Linq;
using Sokoban.Enums;
using System.Collections.Generic;
using System.Dynamic;

namespace Sokoban.Models
{
    public class Game
    {
        private Maze Maze;

        private Forklift Forklift;

        private List<Crate> Crates;

        public bool IsPlaying { get; private set; }
        public bool IsGameOver { get; private set; }

        public Game()
        {
            IsPlaying = true;
            IsGameOver = false;
        }

        private void CheckEndGame()
        {
            if (Maze.AllCratesOnDestination())
            {
                IsPlaying = false;
            }
        }

        public bool LoadMaze(int maze)
        {
            var lines = new List<string>();
            try
            {
                lines = File.ReadAllLines(string.Format("C://Doolhof/doolhof{0}.txt", maze)).ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("Maps/doolhof{0}.txt niet gevonden", maze);
                Console.ReadKey();

                return false;
            }

            ParseMaze(lines);

            return true;
        }

        private void ParseMaze(IReadOnlyCollection<string> lines)
        {
            Maze = new Maze();
            Crates = new List<Crate>();

            var y = lines.Count;
            foreach (var line in lines)
            {
                var x = 1;
                foreach (var character in line.ToCharArray())
                {
                    var coordinate = new Location(x, y);
                    switch (character)
                    {
                        case '#'://Wall
                            Maze.Map.Add(coordinate.ToString(), new Wall(coordinate));
                            break;
                        case '.'://Empty floor
                            Maze.Map.Add(coordinate.ToString(), new Floor(coordinate));
                            break;
                        case 'o'://Crate
                            Maze.Map.Add(coordinate.ToString(), new Floor(coordinate) {HasCrate = true});
                            Crates.Add(new Crate(coordinate));
                            break;
                        case '@'://Forklift
                            Maze.Map.Add(coordinate.ToString(), new Floor(coordinate) {HasForklift = true});
                            Forklift = new Forklift(coordinate);
                            break;
                        case 'x'://Destination
                            Maze.Map.Add(coordinate.ToString(), new Destination(coordinate));
                            break;
                        case ' '://Space
                            Maze.Map.Add(coordinate.ToString(), new Space(coordinate));
                            break;
                        case '~'://Pitfall
                            Maze.Map.Add(coordinate.ToString(), new Pitfall(coordinate) {HasPitfall = true});
                            break;
                        default:
                            throw new IndexOutOfRangeException();
                    }
                    x++;
                }
                y--;
            }
        }

        public void MoveForklift(ConsoleKey input)
        {
            var direction = ConvertInputToDirection(input);
            var coordinate = new Location(Forklift.Coordinate.X, Forklift.Coordinate.Y);

            var nextField = Maze.GetNextField(coordinate, direction);
            if (nextField.Type == FieldType.Wall) return;

            var canMove = nextField.CanWalkOn;

            if (nextField.Type != FieldType.PitFall)
            {
                var nextFloor = (Floor) nextField;
                if (nextFloor.HasCrate)
                {
                    canMove = MoveCrate(nextFloor.Coordinate, direction);
                }

                if (canMove)
                {
                    if (Maze.Map[Forklift.Coordinate.ToString()].Type == FieldType.PitFall)
                    {
                        ((Pitfall) Maze.Map[Forklift.Coordinate.ToString()]).HasForklift = false;
                    }
                    else
                    {
                        ((Floor)Maze.Map[Forklift.Coordinate.ToString()]).HasForklift = false;
                    }
                    Forklift.Walk(direction);
                    nextFloor.HasForklift = true;
                }
            }
            else
            {
                var nextFloor = (Pitfall) nextField;
                if (nextFloor.HasCrate)
                {
                    canMove = MoveCrate(nextFloor.Coordinate, direction);
                }
                if (canMove)
                {
                    if (Maze.Map[Forklift.Coordinate.ToString()].Type == FieldType.PitFall)
                    {
                        ((Pitfall)Maze.Map[Forklift.Coordinate.ToString()]).HasForklift = false;
                    }
                    else
                    {
                        ((Floor)Maze.Map[Forklift.Coordinate.ToString()]).HasForklift = false;
                    }
                    if (((Pitfall) Maze.Map[nextField.Coordinate.ToString()]).GetLives() == 0)
                    {
                        IsPlaying = false;
                        IsGameOver = true;
                    }
                    Forklift.Walk(direction);
                    nextFloor.HasForklift = true;
                }
            }
        }

        private bool MoveCrate(Location coordinate, DirectionType direction)
        {
            var crate = Crates.First(b => b.Coordinate.ToString() == coordinate.ToString());
            var nextField = Maze.GetNextField(coordinate, direction);

            if (nextField.Type == FieldType.Wall) return false;

            if (nextField.Type == FieldType.Floor)
            {
                var nextFloor = (Floor) nextField;
                if (!nextFloor.HasCrate)
                {
                    if (Maze.Map[coordinate.ToString()].Type == FieldType.PitFall)
                    {
                        ((Pitfall) Maze.Map[coordinate.ToString()]).HasCrate = false;
                    }
                    else
                    {
                        ((Floor) Maze.Map[coordinate.ToString()]).HasCrate = false;
                    }
                    crate.Move(direction);
                    nextFloor.HasCrate = true;

                    if (nextFloor.Type == FieldType.Box && Maze.AllCratesOnDestination())
                    {
                        IsPlaying = false;
                    }
                    if (nextFloor.Type == FieldType.PitFall){
                        if (((Pitfall) Maze.Map[nextField.Coordinate.ToString()]).GetLives() == 0)
                        {
                            IsPlaying = false;
                            IsGameOver = true;
                        }
                    }

                    return true;
                }
            }
            else if (nextField.Type == FieldType.PitFall)
            {
                var nextFloor = (Pitfall) nextField;
                if (!nextFloor.HasCrate)
                {
                    ((Floor) Maze.Map[coordinate.ToString()]).HasCrate = false;
                    crate.Move(direction);
                    nextFloor.HasCrate = true;
                    if (((Pitfall)Maze.Map[nextField.Coordinate.ToString()]).GetLives() == 0)
                    {
                        IsPlaying = false;
                        IsGameOver = true;
                    }

                    return true;
                }

            }

            return false;
        }

        public string GetMap()
        {
            var map = "";

            Maze.Map.GroupBy(item => item.Value.Coordinate.Y).ToList().ForEach(row =>
            {
                row.ToList().ForEach(field =>
                {
                    map += field.Value.GetIdentifier();
                });
                map += "\n";
            });

            return map;
        }

        public static Location CalculateNewCoordinate(Location coordinate, DirectionType direction)
        {
            var coord = new Location(coordinate.X, coordinate.Y);
            switch (direction)
            {
                case DirectionType.Up:
                    coord.Y++;
                    break;
                case DirectionType.Down:
                    coord.Y--;
                    break;
                case DirectionType.Left:
                    coord.X--;
                    break;
                case DirectionType.Right:
                    coord.X++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }

            return coord;
        }

        public static DirectionType ConvertInputToDirection(ConsoleKey input)
        {
            if (new[] { ConsoleKey.UpArrow, ConsoleKey.W }.Contains(input))
            {
                return DirectionType.Up;
            }

            if (new[] { ConsoleKey.DownArrow, ConsoleKey.S }.Contains(input))
            {
                return DirectionType.Down;
            }

            if (new[] { ConsoleKey.LeftArrow, ConsoleKey.A }.Contains(input))
            {
                return DirectionType.Left;
            }

            if (new[] { ConsoleKey.RightArrow, ConsoleKey.D }.Contains(input))
            {
                return DirectionType.Right;
            }

            throw new ArgumentOutOfRangeException("input", input, null);
        }

    }
}

