const API_URL = import.meta.env.VITE_API_URL;

export const getRandomFilm = async (genreId = 3, yearFrom = 2000, yearTo = 2024) => {
    const response = await fetch(
        `${API_URL}/Cinema/GetRandomFilm?genreId=${genreId}&yearFrom=${yearFrom}&yearTo=${yearTo}`
    );
    return response.json();
};

export const searchFilm = async (keyword) => {
    const response = await fetch(
        `${API_URL}/Cinema/Search?keyword=${keyword}`
    );
    return response.json();
};