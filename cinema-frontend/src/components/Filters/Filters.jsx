import './Filters.css'
import {useState} from "react";
import {getRandomFilm} from "../../api/films.js";
function Filters() {
    const [loading, setLoading] = useState(false)
    const [rangeValue, setRangeValue] = useState(120)
    const handleGetFilm = async () => {
        setLoading(true)
        try {
            const data = await getRandomFilm()
            setFilm(data)
        } catch (error) {
            console.error('Ошибка:', error)
        } finally {
            setLoading(false)
        }
    }

    return (
        <div className="flex text-lg mt-10 ml-50">
            <div className="flex flex-col">
                <label className="block mb-2.5 ml-1 font-bold text-heading">Жанр</label>
                <div className="relative mb-4.5">
                    <select className="appearance-none w-full px-3 py-2.5 pr-8 bg-neutral-secondary-medium border border-default-medium text-heading rounded-xl">
                        <option>Выбери жанр</option>
                        <option>Боевик</option>
                    </select>
                    {/* Своя стрелка */}
                    <div className="pointer-events-none absolute inset-y-0 right-3 flex items-center">
                        <svg className="w-4 h-4 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 9l-7 7-7-7" />
                        </svg>
                    </div>
                </div>
                <label htmlFor="steps-range"
                       className="block mb-2.5 ml-1 font-bold text-heading">Длительность - <span className="text-amber-400">{rangeValue}</span></label>
                <input id="steps-range" type="range" min="80" max="240" value={rangeValue} step="10"
                       onChange={(e) => setRangeValue(Number(e.target.value))}
                       className="w-full h-2 mb-4.5"/>
                <div className="w-md">
                    <label className="block mb-2.5 ml-1 font-bold text-heading">Годы выхода</label>
                    <div className="flex items-center gap-2">
                        <input type="text"
                               className="bg-neutral-secondary-medium border border-default-medium text-heading rounded-xl focus:ring-brand focus:border-brand w-full px-3 py-2.5 shadow-xs placeholder:text-body"
                               placeholder="С какого года" required/>
                        <span className="text-gray-400">—</span>
                        <input type="text"
                               className="bg-neutral-secondary-medium border border-default-medium text-heading rounded-xl focus:ring-brand focus:border-brand w-full px-3 py-2.5 shadow-xs placeholder:text-body"
                               placeholder="До какого года" required/>
                    </div>
                </div>
                <button
                    className="bg-transparent mt-6 hover:bg-amber-400 text-red-600 border border-amber-400 font-semibold py-2 px-4 rounded-xl"
                    onClick={handleGetFilm} disabled={loading}>
                    {loading ? 'Загрузка...' : 'Выбрать фильм'}
                </button>
            </div>
        </div>
    )
}

export default Filters