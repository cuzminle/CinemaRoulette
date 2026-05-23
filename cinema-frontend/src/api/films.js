const KINOPOISK_API_URL = 'https://kinopoiskapiunofficial.tech/api'
const KINOPOISK_API_KEY = import.meta.env.VITE_KINOPOISK_KEY

export const getRandomFilm = async ({ genres = [], countries = [], duration, yearFrom, yearTo }) => {
    const params = new URLSearchParams()

    genres.forEach(id => params.append('genres', id))
    countries.forEach(id => params.append('countries', id))

    if (duration) params.append('duration', duration)
    if (yearFrom) params.append('yearFrom', yearFrom)
    if (yearTo) params.append('yearTo', yearTo)
    console.log(params)
    const response = await fetch(`${KINOPOISK_API_URL}/v2.2/films?${params}`, {
        headers: {
            'X-API-KEY': KINOPOISK_API_KEY,
            'Content-Type': 'application/json',
        }
    })
    const data = await response.json()
    const films = data.items ?? []
    return films[Math.floor(Math.random() * films.length)]
}

export const searchFilm = async (keyword) => {
    const response = await fetch(`${KINOPOISK_API_URL}/v2.2/films?keyword=${keyword}`, {
        headers: {
            'X-API-KEY': KINOPOISK_API_KEY,
            'Content-Type': 'application/json',
        }
    });
    return response.json();
};

export const getFilters = async () => {
    const response = await fetch(`${KINOPOISK_API_URL}/v2.2/films/filters`, {
        headers: {
            'X-API-KEY': KINOPOISK_API_KEY,
            'Content-Type': 'application/json',
        }
    });
    return response.json();
};