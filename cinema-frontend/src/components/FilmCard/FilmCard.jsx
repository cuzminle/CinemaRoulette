import './FilmCard.css'

function FilmCard({ film }) {
    console.log(film)
    return (
        <div className="film-card flex flex-col items-center text-center w-auto mt-5 mb-5">
            <img
                src={film.posterUrlPreview ?? film.posterUrl}
                alt={film.nameRu}
                width={300}
                className="rounded-xl"
            />
            <h2 className="mt-3 text-lg font-bold">{film.nameRu || film.nameOriginal}</h2>
            <h4 className="mt-1 font-semibold">{film.nameOriginal}</h4>
            <p className="mt-3">Год: {film.year}</p>
            <p>Страны: {film.countries.map(c => c.country).join(', ')}</p>
            <p>Рейтинг Кинопоиск: {film.ratingKinopoisk}</p>
            <p>Рейтинг Imdb: {film.ratingImdb}</p>
        </div>
    )
}

export default FilmCard