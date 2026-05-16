const API_URL = import.meta.env.VITE_API_URL;

export const getRandomFilm = async ({ genres = [], countries = [], duration, yearFrom, yearTo }) => {
    const params = new URLSearchParams()

    genres.forEach(id => params.append('genreId', id))
    countries.forEach(id => params.append('countryId', id))

    if (duration) params.append('duration', duration)
    if (yearFrom) params.append('yearFrom', yearFrom)
    if (yearTo) params.append('yearTo', yearTo)

    const response = await fetch(`${API_URL}/Cinema/GetRandomFilm?${params}`)
    return response.json()
}

export const searchFilm = async (keyword) => {
    const response = await fetch(
        `${API_URL}/Cinema/Search?keyword=${keyword}`
    );
    return response.json();
};

export const getFilters = async () => {
    const response = await fetch(
        `${API_URL}/Filters/GetFilters`
    );
    return response.json();
};