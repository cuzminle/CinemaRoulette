import { useState } from 'react'
import FilmCard from './components/FilmCard/FilmCard'
import Filters from './components/Filters/Filters'
import logo from './assets/CinemaRouletteBanner.png'

function App() {
  const [film, setFilm] = useState({
      "kinopoiskId": 1339977,
      "imdbId": null,
      "nameRu": "Уровень тревоги: Полночь",
      "nameEn": null,
      "nameOriginal": "Threat Level Midnight: The Movie",
      "countries": [
          {
              "country": "США"
          },
          {
              "country": "Великобритания"
          }
      ],
      "genres": [
          {
              "genre": "боевик"
          },
          {
              "genre": "комедия"
          },
          {
              "genre": "короткометражка"
          }
      ],
      "ratingKinopoisk": 9.3,
      "ratingImdb": 9.6,
      "year": 2011,
      "type": "FILM",
      "posterUrl": "https://kinopoiskapiunofficial.tech/images/posters/kp/1339977.jpg",
      "posterUrlPreview": "https://kinopoiskapiunofficial.tech/images/posters/kp_small/1339977.jpg"
  })
  return (
      <div>
          <div className="flex justify-center">
              <img src={logo} width={1200} alt="Cinema Roulette" />
          </div>

          <div className="flex flex-col md:flex-row md:gap-120 gap-6 items-center">
              <Filters onFilmLoaded={setFilm}/>
              {film && <FilmCard film={film} />}
          </div>
      </div>
  )
}

export default App