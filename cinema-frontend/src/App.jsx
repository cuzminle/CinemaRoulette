import { useState } from 'react'
import { getRandomFilm } from './api/films'
import FilmCard from './components/FilmCard/FilmCard'
import Filters from './components/Filters/Filters'
import logo from './assets/CinemaRouletteBanner.png'

function App() {
  const [film, setFilm] = useState(null)
  const [loading, setLoading] = useState(false)

  return (
      <div>
          <div className="flex justify-center">
              <img src={logo} width={1200} alt="Cinema Roulette" />
          </div>

         <Filters/>
        {film && <FilmCard film={film} />}
      </div>
  )
}

export default App