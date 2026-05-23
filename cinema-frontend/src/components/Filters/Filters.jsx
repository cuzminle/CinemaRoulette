import './Filters.css'
import {useEffect, useState, useRef} from "react";
import {getRandomFilm, getFilters} from "../../api/films.js";

function Filters({ onFilmLoaded }) {
    const [loading, setLoading] = useState(false)
    const [rangeValue, setRangeValue] = useState(120)
    const [filters, setFilters] = useState({ genres: [], countries: [] })
    const [selectedGenres, setSelectedGenres] = useState([])
    const [selectedCountries, setSelectedCountries] = useState([])
    const [isOpenGenres, setIsOpenGenres] = useState(false)
    const [isOpenCountries, setIsOpenCountries] = useState(false)
    const [yearFrom, setYearFrom] = useState('')
    const [yearTo, setYearTo] = useState('')

    const genresRef = useRef(null)
    const countriesRef = useRef(null)

    useEffect(() => {
        const loadFilters = async () => {
            const data = await getFilters()
            setFilters(data)
        }
        loadFilters()
    }, [])

    useEffect(() => {
        const handler = (e) => {
            if (genresRef.current && !genresRef.current.contains(e.target)) setIsOpenGenres(false)
            if (countriesRef.current && !countriesRef.current.contains(e.target)) setIsOpenCountries(false)
        }
        document.addEventListener('mousedown', handler)
        return () => document.removeEventListener('mousedown', handler)
    }, [])

    const handleGetFilm = async () => {
        setLoading(true)
        try {
            const data = await getRandomFilm({
                genres: selectedGenres,
                countries: selectedCountries,
                duration: rangeValue,
                yearFrom,
                yearTo
            })
            onFilmLoaded(data) // <-- вместо setFilm(data)
        } catch (error) {
            console.error('Ошибка:', error)
        } finally {
            setLoading(false)
        }
    }
    const toggle = (setter) => (id) => {
        setter(prev => prev.includes(id) ? prev.filter(x => x !== id) : [...prev, id])
    }

    const labelGenres = selectedGenres.length === 0
        ? "Выбери жанр"
        : filters.genres.filter(g => selectedGenres.includes(g.id)).map(g => g.genre).join(", ")

    const labelCountries = selectedCountries.length === 0
        ? "Выбери страну"
        : filters.countries.filter(c => selectedCountries.includes(c.id)).map(c => c.country).join(", ")

    const Dropdown = ({ label, isOpen, setIsOpen, items, selected, onToggle, dropRef }) => (
        <div className="relative mb-4.5" ref={dropRef}>
            <button
                onClick={() => setIsOpen(!isOpen)}
                className="appearance-none w-full px-3 py-2.5 pr-8 bg-neutral-secondary-medium border border-default-medium text-heading rounded-xl text-left"
            >
                <span className="truncate block">{label}</span>
                <div className="pointer-events-none absolute inset-y-0 right-3 flex items-center">
                    <svg className="w-4 h-4 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 9l-7 7-7-7" />
                    </svg>
                </div>
            </button>
            {isOpen && (
                <div className="absolute z-10 w-full mt-1 border border-default-medium rounded-xl max-h-60 overflow-y-auto" style={{ background: '#000' }}>
                    {items.filter(i => i.genre || i.country).map(item => (
                        <label key={item.id} className="flex items-center gap-2 px-3 py-2 cursor-pointer" style={{ background: '#000' }}>
                            <input
                                type="checkbox"
                                checked={selected.includes(item.id)}
                                onChange={() => onToggle(item.id)}
                            />
                            {item.genre ?? item.country}
                        </label>
                    ))}
                </div>
            )}
        </div>
    )

    return (
        <div className="flex text-lg mt-10 m-auto md:ml-50">
            <div className="flex flex-col">
                <label className="block mb-2.5 ml-1 font-bold text-heading">Жанр</label>
                <Dropdown
                    label={labelGenres}
                    isOpen={isOpenGenres}
                    setIsOpen={setIsOpenGenres}
                    items={filters.genres}
                    selected={selectedGenres}
                    onToggle={toggle(setSelectedGenres)}
                    dropRef={genresRef}
                />

                <label className="block mb-2.5 ml-1 font-bold text-heading">Страна</label>
                <Dropdown
                    label={labelCountries}
                    isOpen={isOpenCountries}
                    setIsOpen={setIsOpenCountries}
                    items={filters.countries}
                    selected={selectedCountries}
                    onToggle={toggle(setSelectedCountries)}
                    dropRef={countriesRef}
                />

                <label className="block mb-2.5 ml-1 font-bold text-heading">
                    Длительность ~ <span className="text-amber-400">{rangeValue} минут</span>
                </label>
                <input id="steps-range" type="range" min="80" max="180" value={rangeValue} step="10"
                       onChange={(e) => setRangeValue(Number(e.target.value))}
                       className="w-full h-2 mb-4.5"
                />

                <div className="w-md">
                    <label className="block mb-2.5 ml-1 font-bold text-heading">Годы выхода</label>
                    <div className="flex items-center gap-2">
                        <input type="text"
                               className="bg-neutral-secondary-medium border border-default-medium text-heading rounded-xl focus:ring-brand focus:border-brand w-full px-3 py-2.5 shadow-xs placeholder:text-body"
                               placeholder="С какого года" required
                               onChange={e => setYearFrom(e.target.value)}
                        />
                        <span className="text-gray-400">—</span>
                        <input type="text"
                               className="bg-neutral-secondary-medium border border-default-medium text-heading rounded-xl focus:ring-brand focus:border-brand w-full px-3 py-2.5 shadow-xs placeholder:text-body"
                               placeholder="До какого года" required
                               onChange={e => setYearTo(e.target.value)}
                        />
                    </div>
                </div>

                <button
                    className="film-button bg-transparent mt-6 hover:bg-amber-400 text-red-600 font-semibold py-2 px-4 rounded-xl"
                    onClick={handleGetFilm} disabled={loading}
                >
                    {loading ? 'Загрузка...' : 'Выбрать фильм'}
                </button>
            </div>
        </div>
    )
}

export default Filters