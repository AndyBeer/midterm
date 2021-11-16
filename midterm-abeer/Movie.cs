using System;
using System.Collections.Generic;
using System.Text;

namespace midterm_abeer
{
    public class Movie
    {
        public string Title { get; set; }
        public string MainActor { get; set; }
        public Genre Category { get; set; }
        public string Director { get; set; }

        public Movie(string Title, string MainActor, Genre Category, string Director)
        {
            this.Title = Title;
            this.MainActor = MainActor;
            this.Category = Category;
            this.Director = Director;
        }
        public Movie()
        { }
        public override string ToString()
        {
            return $"~{Title}\nMain Actor: {MainActor}\nDirector: {Director}\nGenre: {Category}\n";
        }
    }
}
