using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q2.DTOs;
using System.Text.Json;

namespace Q2.Pages.Movies
{
    public class Director_MovieModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public Director_MovieModel(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public List<Director> Directors { get; set; } = new();
        public List<MovieResponse> Movies { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int? DirectorId { get; set; }

        public async Task OnGetAsync()
        {
            string baseUrl = _configuration["GivenAPIBaseUrl"]!;
            var client = _httpClientFactory.CreateClient();

            var directorsResponse = await client.GetAsync($"{baseUrl}/api/Directors/GetDirectors");
            if (directorsResponse.IsSuccessStatusCode)
            {
                var json = await directorsResponse.Content.ReadAsStringAsync();
                Directors = JsonSerializer.Deserialize<List<Director>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new();
            }

            HttpResponseMessage moviesResponse;

            if (DirectorId.HasValue)
            {
                moviesResponse = await client.GetAsync($"{baseUrl}/api/Movies/GetMoviesByDirectorId/{DirectorId}");
            }
            else
            {
                moviesResponse = await client.GetAsync($"{baseUrl}/api/Movies/GetMovies");
            }

            if (moviesResponse.IsSuccessStatusCode)
            {
                var json = await moviesResponse.Content.ReadAsStringAsync();
                var movieData = JsonSerializer.Deserialize<List<MovieResponse>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new();

                Movies = movieData;
            }
        }
    }
}
