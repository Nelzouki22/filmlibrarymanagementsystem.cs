using System;
using System.Collections.Generic;

class Film
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public int Year { get; set; }
    public bool IsAvailable { get; set; }
}

class Member
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Film> FilmsBorrowed { get; set; } = new List<Film>();
}

class FilmLibrary
{
    public List<Film> Films { get; set; } = new List<Film>();
    public List<Member> Members { get; set; } = new List<Member>();
}

class Program
{
    static void Main(string[] args)
    {
        FilmLibrary filmLibrary = new FilmLibrary();

        while (true)
        {
            Console.WriteLine("Film Library Management System");
            Console.WriteLine("1. Add Film");
            Console.WriteLine("2. Add Member");
            Console.WriteLine("3. Borrow Film");
            Console.WriteLine("4. Return Film");
            Console.WriteLine("5. List Films");
            Console.WriteLine("6. List Members");
            Console.WriteLine("7. Exit");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddFilm(filmLibrary);
                    break;
                case 2:
                    AddMember(filmLibrary);
                    break;
                case 3:
                    BorrowFilm(filmLibrary);
                    break;
                case 4:
                    ReturnFilm(filmLibrary);
                    break;
                case 5:
                    ListFilms(filmLibrary);
                    break;
                case 6:
                    ListMembers(filmLibrary);
                    break;
                case 7:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void AddFilm(FilmLibrary filmLibrary)
    {
        Film film = new Film
        {
            Id = filmLibrary.Films.Count + 1,
            Title = Console.ReadLine(),
            Director = Console.ReadLine(),
            Year = int.Parse(Console.ReadLine()),
            IsAvailable = true,
        };

        filmLibrary.Films.Add(film);
        Console.WriteLine("Film added successfully.");
    }

    static void AddMember(FilmLibrary filmLibrary)
    {
        Member member = new Member
        {
            Id = filmLibrary.Members.Count + 1,
            Name = Console.ReadLine(),
        };

        filmLibrary.Members.Add(member);
        Console.WriteLine("Member added successfully.");
    }

    static void BorrowFilm(FilmLibrary filmLibrary)
    {
        Console.Write("Enter Member ID: ");
        int memberId = int.Parse(Console.ReadLine());

        Member member = filmLibrary.Members.Find(m => m.Id == memberId);
        if (member == null)
        {
            Console.WriteLine("Member not found.");
            return;
        }

        Console.Write("Enter Film ID to borrow: ");
        int filmId = int.Parse(Console.ReadLine());

        Film film = filmLibrary.Films.Find(f => f.Id == filmId);
        if (film == null)
        {
            Console.WriteLine("Film not found.");
            return;
        }

        if (!film.IsAvailable)
        {
            Console.WriteLine("Film is already borrowed.");
            return;
        }

        member.FilmsBorrowed.Add(film);
        film.IsAvailable = false;
        Console.WriteLine("Film borrowed successfully.");
    }

    static void ReturnFilm(FilmLibrary filmLibrary)
    {
        Console.Write("Enter Member ID: ");
        int memberId = int.Parse(Console.ReadLine());

        Member member = filmLibrary.Members.Find(m => m.Id == memberId);
        if (member == null)
        {
            Console.WriteLine("Member not found.");
            return;
        }

        Console.Write("Enter Film ID to return: ");
        int filmId = int.Parse(Console.ReadLine());

        Film film = filmLibrary.Films.Find(f => f.Id == filmId);
        if (film == null)
        {
            Console.WriteLine("Film not found.");
            return;
        }

        if (film.IsAvailable)
        {
            Console.WriteLine("This film is not borrowed.");
            return;
        }

        member.FilmsBorrowed.Remove(film);
        film.IsAvailable = true;
        Console.WriteLine("Film returned successfully.");
    }

    static void ListFilms(FilmLibrary filmLibrary)
    {
        Console.WriteLine("List of Films:");
        foreach (var film in filmLibrary.Films)
        {
            Console.WriteLine($"ID: {film.Id}, Title: {film.Title}, Director: {film.Director}, Year: {film.Year}, Available: {film.IsAvailable}");
        }
    }

    static void ListMembers(FilmLibrary filmLibrary)
    {
        Console.WriteLine("List of Members:");
        foreach (var member in filmLibrary.Members)
        {
            Console.WriteLine($"ID: {member.Id}, Name: {member.Name}");
            Console.WriteLine("Films Borrowed:");
            foreach (var film in member.FilmsBorrowed)
            {
                Console.WriteLine($"  Title: {film.Title}, Director: {film.Director}, Year: {film.Year}");
            }
        }
    }
}
