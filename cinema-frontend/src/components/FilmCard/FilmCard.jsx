import './FilmCard.css'

function FilmCard({ film }) {
    return (
        <div className="film-card">
            <img
                src={film.posterUrl}
                alt={film.nameRu}
                width={200}
            />
            <h2>{film.nameRu || film.nameOriginal}</h2>
            <p>Год: {film.year}</p>
            <p>Рейтинг Кинопоиск: {film.ratingKinopoisk}</p>
        </div>
    )
}

export default FilmCard