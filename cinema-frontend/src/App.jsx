import { useState } from 'react'
import FilmCard from './components/FilmCard/FilmCard'
import Filters from './components/Filters/Filters'
import logo from './assets/CinemaRouletteBanner.png'

function App() {
  const [film, setFilm] = useState(null)
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