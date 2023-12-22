using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO.Pipes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;
using Data;
using EntryToEntity.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    enum MenuItem
    {
        Додавання,
        Редагування,
        Видалення,
        Вихід,
    }

    enum Properties
    {
        Команда1,
        Команда2,
        Голи1,
        Голи2,
        Дата,
    }

    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        using (DataContex dc = new DataContex())
        {
            Player player1 = new Player { Name = "Родріго", Country = "Бразилія", Position = "Форвард", Number = 11 };
            Player player2 = new Player { Name = "Даніель Карвахаль", Country = "Іспанія", Position = "Захисник", Number = 2 };
            Player player3 = new Player { Name = "Джуд Беллінгем", Country = "Велика Британія", Position = "Півзахисник", Number = 5 };
            Player player4 = new Player { Name = "Артем Довбик", Country = "Україна", Position = "Форвард", Number = 9 };
            Player player5 = new Player { Name = "Савіо", Country = "Бразилія", Position = "Форвард", Number = 16 };
            Player player6 = new Player { Name = "Крістіан Стуані", Country = "Уругвай", Position = "Форвард", Number = 7 };
            Player player7 = new Player { Name = "Роберт Лєвандовскі", Country = "Польща", Position = "Форвард", Number = 9 };
            Player player8 = new Player { Name = "Ферран Торрес", Country = "Іспанія", Position = "Форвард", Number = 11 };
            Player player9 = new Player { Name = "Жоау Феліш", Country = "Португалія", Position = "Форвард", Number = 23 };
            Player player10 = new Player { Name = "Антуан Грізманн", Country = "Франція", Position = "Форвард", Number = 7 };
            Player player11 = new Player { Name = "Анхель Корреа", Country = "Аргентина", Position = "Форвард", Number = 10 };
            Player player12 = new Player { Name = "Альваро Мората", Country = "Іспанія", Position = "Форвард", Number = 19 };
            Player player13 = new Player { Name = "Іньякі Вільямс", Country = "Іспанія", Position = "Форвард", Number = 9 };
            Player player14 = new Player { Name = "Горка Гурусета", Country = "Іспанія", Position = "Форвард", Number = 12 };
            Player player15 = new Player { Name = "Ойхан Сансет", Country = "Іспанія", Position = "Півзахисник", Number = 8 };

            Team realMadrid = new Team { Name = "Real Madrid", City = "Мадрид", Wins = 14, Defeats = 1, Draws = 3, ScoredGoals = 39, MissedGoals = 11, Players = { player1, player2, player3 } };
            Team girona = new Team { Name = "Girona", City = "Жірона", Wins = 14, Defeats = 1, Draws = 3, ScoredGoals = 42, MissedGoals = 21, Players = { player4, player5, player6 } };
            Team barcelona = new Team { Name = "Barcelona", City = "Барселона", Wins = 11, Defeats = 2, Draws = 5, ScoredGoals = 34, MissedGoals = 21, Players = { player7, player8, player9 } };
            Team atleticoMadrid = new Team { Name = "Atletico de Madrid", City = "Мадрид", Wins = 11, Defeats = 4, Draws = 2, ScoredGoals = 35, MissedGoals = 18, Players = { player10, player11, player12 } };
            Team atleticoBilbao = new Team { Name = "Athletic Bilbao", City = "Більбао", Wins = 10, Defeats = 3, Draws = 5, ScoredGoals = 34, MissedGoals = 19, Players = { player13, player14, player15 } };

            Model.Match brm = new Model.Match { Teams = { realMadrid, barcelona }, CountScoredTeam1 = 2, CountScoredTeam2 = 1, Date = new DateOnly(2023, 10, 28) };
            Model.Match grm = new Model.Match { Teams = { girona, realMadrid }, CountScoredTeam1 = 0, CountScoredTeam2 = 3, Date = new DateOnly(2023, 9, 30) };
            Model.Match abrm = new Model.Match { Teams = { atleticoBilbao, realMadrid }, CountScoredTeam1 = 0, CountScoredTeam2 = 2, Date = new DateOnly(2023, 8, 12) };
            Model.Match amrm = new Model.Match { Teams = { atleticoMadrid, realMadrid }, CountScoredTeam1 = 3, CountScoredTeam2 = 1, Date = new DateOnly(2023, 9, 24) };
            Model.Match amab = new Model.Match { Teams = { atleticoMadrid, atleticoBilbao }, CountScoredTeam1 = 2, CountScoredTeam2 = 0, Date = new DateOnly(2023, 12, 16) };
            Model.Match bg = new Model.Match { Teams = { barcelona, girona }, CountScoredTeam1 = 2, CountScoredTeam2 = 4, Date = new DateOnly(2023, 12, 10) };
            Model.Match bam = new Model.Match { Teams = { barcelona, atleticoMadrid }, CountScoredTeam1 = 1, CountScoredTeam2 = 0, Date = new DateOnly(2023, 12, 3) };
            Model.Match gab = new Model.Match { Teams = { girona, atleticoBilbao }, CountScoredTeam1 = 1, CountScoredTeam2 = 1, Date = new DateOnly(2023, 11, 27) };

            Goal goal1 = new Goal { Player = player4, Match = bg, Minute = 12 };
            Goal goal2 = new Goal { Player = player7, Match = bg, Minute = 19 };
            Goal goal3 = new Goal { Player = player3, Match = grm, Minute = 71 };
            Goal goal4 = new Goal { Player = player3, Match = abrm, Minute = 36 };
            Goal goal5 = new Goal { Player = player1, Match = abrm, Minute = 28 };
            Goal goal6 = new Goal { Player = player3, Match = brm, Minute = 68 };
            Goal goal7 = new Goal { Player = player3, Match = brm, Minute = 92 };
            Goal goal8 = new Goal { Player = player12, Match = amrm, Minute = 4 };
            Goal goal9 = new Goal { Player = player10, Match = amrm, Minute = 18 };
            Goal goal10 = new Goal { Player = player12, Match = amrm, Minute = 46 };
            Goal goal11 = new Goal { Player = player13, Match = gab, Minute = 67 };
            Goal goal12 = new Goal { Player = player13, Match = amab, Minute = 64 };

            dc.Matches.AddRange(brm, grm, abrm, amrm, amab, bg, bam, gab);
            dc.Players.AddRange(player1, player2, player3, player4, player5, player6, player7, player8, player9, player10, player11, player12, player13, player14, player15);
            dc.Teams.AddRange(realMadrid, girona, barcelona, atleticoMadrid, atleticoBilbao);
            dc.Goals.AddRange(goal1, goal2, goal3, goal4, goal5, goal6, goal7, goal8, goal9, goal10, goal11, goal12);
            dc.SaveChanges();

            PrintTeams(dc);
            PrintDifference(dc);
            PrintMatches(dc);
            FindByDate(dc);
            FindByName(dc);
            FindGoalsByDate(dc);

            //------------------------------------------------------------------------------------------------------------------------------------------------------------
            Console.WriteLine("*управління за допомогою стрілочок*");
            while (true)
            {
                int input = Menu.MultipleChoice(true, new MenuItem());
                switch ((MenuItem)input)
                {
                    case MenuItem.Додавання:
                        MenuAdd(dc);
                        break;

                    case MenuItem.Редагування:
                        MenuChange(dc);
                        break;

                    case MenuItem.Видалення:
                        MenuDelete(dc);
                        break;

                    case MenuItem.Вихід:
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }

                PrintMatches(dc);
            }
        }
    }

    private static void PrintTeams(DataContex dc)
    {
        var teams = dc.Teams.ToList();
        Console.WriteLine("Турнірна талиця: ");
        Console.WriteLine();
        Console.WriteLine("{0,-25} {1,-20} {2,-10} {3,-10} {4,-10} {5,-10} {6,-10}", "Назва команди", "Назва міста", "W", "L", "D", "SG", "MG");
        Console.WriteLine();
        foreach (var team in teams)
        {
            Console.WriteLine("{0,-25} {1,-20} {2,-10} {3,-10} {4,-10} {5,-10} {6,-10}", team.Name, team.City, team.Wins, team.Defeats, team.Draws, team.ScoredGoals, team.MissedGoals);
        }

        Console.ReadKey();
        Console.Clear();
    }

    private static void PrintDifference(DataContex dc)
    {
        var teams = dc.Teams.ToList();
        Console.WriteLine("Різниця забитих та пропущених голів:");
        Console.WriteLine();
        Console.WriteLine("{0,-25} {1,-10} {2,-10} {3, -10}", "Назва команди", "Забиті", "Пропущені", "Різниця");
        Console.WriteLine();
        foreach (var team in teams)
        {
            Console.WriteLine("{0,-25} {1,-10} {2,-10} {3, -10}", team.Name, team.ScoredGoals, team.MissedGoals, team.ScoredGoals - team.MissedGoals);
        }

        Console.ReadKey();
        Console.Clear();
    }

    private static void PrintMatches(DataContex dc)
    {
        var matches = dc.Matches.ToList();
        Console.WriteLine("Інформація про матчі:");
        Console.WriteLine();
        Console.WriteLine("{0,-25} {1,-10} {2,-25} {3,-10} {4,-10} ", "Назва команди 1", "Голи", "Назва команди 2", "Голи", "Дата");
        Console.WriteLine();
        foreach (var match in matches)
        {
            Console.WriteLine("{0,-25} {1,-10} {2,-25} {3,-10} {4,-10} ", match.Teams[0].Name, match.CountScoredTeam1, match.Teams[1].Name, match.CountScoredTeam2, match.Date);
        }

        Console.ReadKey();
        Console.Clear();
    }

    private static void FindByDate(DataContex dc)
    {
        Console.WriteLine("Пошук матчу за датою");
        DateOnly dateMatch;
        while (true)
        {
            Console.WriteLine("Введіть дату матчу (у форматі yyyy-MM-dd): ");
            string dateInput = Console.ReadLine();

            if (DateOnly.TryParse(dateInput, out dateMatch))
            {
                break;
            }
            else
            {
                Console.WriteLine("Некоректний формат дати!");
            }
        }

        var determinedDateMatches = dc.Matches.Where(m => m.Date == dateMatch).ToList();
        if (determinedDateMatches.Count != 0)
        {
            Console.WriteLine();
            Console.WriteLine("{0,-25} {1,-10} {2,-25} {3,-10} {4,-10} ", "Назва команди 1", "Голи", "Назва команди 2", "Голи", "Дата");
            Console.WriteLine();
            foreach (var match in determinedDateMatches)
            {
                Console.WriteLine("{0,-25} {1,-10} {2,-25} {3,-10} {4,-10} ", match.Teams[0].Name, match.CountScoredTeam1, match.Teams[1].Name, match.CountScoredTeam2, match.Date);
            }
        }
        else
        {
            Console.WriteLine("Матч не знайдено.");
        }

        Console.ReadKey();
        Console.Clear();
    }

    private static void FindByName(DataContex dc)
    {
        Console.WriteLine("Пошук матчу за назвою команди");
        Console.WriteLine("Введіть назву команди: ");
        string teamInput = Console.ReadLine();
        var determinedTeamMatches = dc.Matches.Where(m => m.Teams.Any(t => t.Name == teamInput)).ToList();

        if (determinedTeamMatches.Count != 0)
        {
            Console.WriteLine();
            Console.WriteLine("{0,-25} {1,-10} {2,-25} {3,-10} {4,-10} ", "Назва команди 1", "Голи", "Назва команди 2", "Голи", "Дата");
            Console.WriteLine();
            foreach (var match in determinedTeamMatches)
            {
                Console.WriteLine("{0,-25} {1,-10} {2,-25} {3,-10} {4,-10} ", match.Teams[0].Name, match.CountScoredTeam1, match.Teams[1].Name, match.CountScoredTeam2, match.Date);
            }
        }
        else
        {
            Console.WriteLine("Матч не знайдено.");
        }

        Console.ReadKey();
        Console.Clear();
    }

    private static void FindGoalsByDate(DataContex dc)
    {
        Console.WriteLine("Пошук гравців, які забили гол за датою");
        DateOnly dateMatch;
        while (true)
        {
            Console.WriteLine("Введіть дату матчу (у форматі yyyy-MM-dd): ");
            string dateInput = Console.ReadLine();

            if (DateOnly.TryParse(dateInput, out dateMatch))
            {
                break;
            }
            else
            {
                Console.WriteLine("Некоректний формат дати!");
            }
        }

        var determinedDatePlayers = dc.Players
            .Include(p => p.Team)
            .Where(p => p.Goals.Any(g => g.Match.Date == dateMatch))
            .ToList();

        if (determinedDatePlayers.Count != 0)
        {
            Console.WriteLine();
            Console.WriteLine("{0,-25} {1,-10} {2,-10} {3,-7} {4,-35} {5,-5}", "ПІБ", "Країна", "Позиція", "Номер", "Матч", "Хвилина");
            Console.WriteLine();

            foreach (var player in determinedDatePlayers)
            {
                var playerGoals = player.Goals.Where(g => g.Match.Date == dateMatch).ToList();

                if (playerGoals.Any())
                {
                    var teamNames = playerGoals.Select(g => g.Match.Teams[0].Name + " vs " + g.Match.Teams[1].Name).Distinct().ToList();
                    var minutePerTeam = playerGoals
                        .GroupBy(g => g.Match.Teams[0].Name + " vs " + g.Match.Teams[1].Name)
                        .ToDictionary(g => g.Key, g => string.Join(", ", g.Select(go => go.Minute)));

                    foreach (var teamName in teamNames)
                    {
                        Console.WriteLine("{0,-25} {1,-10} {2,-10} {3,-7} {4,-35} {5,-5}", player.Name, player.Country, player.Position, player.Number, teamName, minutePerTeam[teamName]);
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Матчі не знайдено.");
        }

        Console.ReadKey();
        Console.Clear();
    }

    private static void MenuAdd(DataContex dc)
    {
        Console.Clear();
        Console.WriteLine("Введіть id першої команди: ");
        int team1 = Convert.ToInt32(Console.ReadLine());
        Console.Clear();

        Console.WriteLine("Введіть id другої команди: ");
        int team2 = Convert.ToInt32(Console.ReadLine());
        Console.Clear();

        Console.WriteLine("Введіть к-сть забитих голів першої команди: ");
        int sgteam1 = Convert.ToInt32(Console.ReadLine());
        Console.Clear();

        Console.WriteLine("Введіть к-сть забитих голів другої команди: ");
        int sgteam2 = Convert.ToInt32(Console.ReadLine());
        Console.Clear();

        DateOnly date;
        while (true)
        {
            Console.WriteLine("Введіть дату гри (у форматі yyyy-MM-dd): ");
            string dateInput = Console.ReadLine();

            if (DateOnly.TryParse(dateInput, out date))
            {
                break;
            }
            else
            {
                Console.WriteLine("Некоректний формат дати!");
            }
        }

        Console.Clear();

        var matchesWithTeam1 = dc.Matches
            .Any(m => m.Teams.Any(t => t.Id == team1));

        var matchesWithTeam2 = dc.Matches
            .Any(m => m.Teams.Any(t => t.Id == team2));

        var matchesWithDate = dc.Matches
            .Any(m => m.Date == date);

        bool matchExists = matchesWithTeam1 && matchesWithTeam2 && matchesWithDate;

        if (matchExists)
        {
            Console.WriteLine("Такий матч вже існує в базі даних.");
        }
        else
        {
            if (team1 == team2)
            {
                Console.WriteLine("Не може id першої команди дорівнювати id другої");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                if (dc.Teams.Find(team1) != null && dc.Teams.Find(team2) != null)
                {
                    var newMatch = new Model.Match
                    {
                        Teams = { dc.Teams.Find(team1), dc.Teams.Find(team2) },
                        CountScoredTeam1 = sgteam1,
                        CountScoredTeam2 = sgteam2,
                        Date = date,
                    };

                    dc.Matches.Add(newMatch);
                    dc.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Одна або обидві команди не знайдені у базі даних.");
                }
            }
        }
    }

    public static void MenuChange(DataContex dc)
    {
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Введіть id матчу, дані якого хочете змінити: ");
            int idMatch = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            if (dc.Matches.FirstOrDefault(m => m.Id == idMatch) != null)
            {
                int inputProperty = Menu.MultipleChoice(true, new Properties());

                switch ((Properties)inputProperty)
                {
                    case Properties.Команда1:
                        Console.Clear();
                        Console.WriteLine("Введіть id нової команди: ");
                        int newid1 = Convert.ToInt32(Console.ReadLine());
                        if (dc.Matches.FirstOrDefault(m => m.Id == idMatch).Teams[1].Id == newid1)
                        {
                            Console.WriteLine("Не може id першої команди дорівнювати id другої");
                        }
                        else
                        {
                            dc.Matches.FirstOrDefault(m => m.Id == idMatch).Teams[0] = dc.Teams.Find(newid1);
                        }

                        break;

                    case Properties.Команда2:
                        Console.Clear();
                        Console.WriteLine("Введіть id нової команди: ");
                        int newid2 = Convert.ToInt32(Console.ReadLine());
                        if (dc.Matches.FirstOrDefault(m => m.Id == idMatch).Teams[0].Id == newid2)
                        {
                            Console.WriteLine("Не може id першої команди дорівнювати id другої");
                        }
                        else
                        {
                            dc.Matches.FirstOrDefault(m => m.Id == idMatch).Teams[1] = dc.Teams.Find(newid2);
                        }

                        break;

                    case Properties.Голи1:
                        Console.Clear();
                        Console.WriteLine("Введіть нову кількість голів першої команди: ");
                        int newsg1 = Convert.ToInt32(Console.ReadLine());
                        dc.Matches.FirstOrDefault(m => m.Id == idMatch).CountScoredTeam1 = newsg1;
                        break;

                    case Properties.Голи2:
                        Console.Clear();
                        Console.WriteLine("Введіть нову кількість голів другої команди: ");
                        int newsg2 = Convert.ToInt32(Console.ReadLine());
                        dc.Matches.FirstOrDefault(m => m.Id == idMatch).CountScoredTeam2 = newsg2;
                        break;

                    case Properties.Дата:
                        Console.Clear();
                        DateOnly newdate;
                        while (true)
                        {
                            Console.WriteLine("Введіть нову дату матчу (у форматі yyyy-MM-dd): ");
                            string dateInput = Console.ReadLine();

                            if (DateOnly.TryParse(dateInput, out newdate))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Некоректний формат дати!");
                            }

                        }

                        dc.Matches.FirstOrDefault(m => m.Id == idMatch).Date = newdate;
                        break;
                }

                dc.SaveChanges();
            }
            else
            {
                Console.WriteLine("Матч не знайдено.");
                Console.ReadKey();
                Console.Clear();
            }

            Console.Clear();
            break;
        }
    }

    public static void MenuDelete(DataContex dc)
    {
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Введіть id матчу, якипй хочете видалити: ");
            int idMatch = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            if (dc.Matches.FirstOrDefault(m => m.Id == idMatch) != null)
            {
                dc.Matches.Remove(dc.Matches.FirstOrDefault(m => m.Id == idMatch));
                dc.SaveChanges();
                break;
            }
            else
            {
                Console.WriteLine("Матч не знайдено.");
                Console.ReadKey();
                Console.Clear();
            }

            break;
        }

        Console.Clear();
    }
}