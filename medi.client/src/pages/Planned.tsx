
import image1 from '../assets/image1.png';
import image2 from '../assets/image2.png';

import '../styles/Planned.css';

const Przyklad: React.FC = () => (
    <>

        <div className="main-flex-container">

            <div className="left-section">
                <div className="form-box">
                    <h2>Moje zaplanowane wizyty</h2>

                    <div className="visit-card">
                        <strong>15 sierpnia 2024, 10:00</strong>
                        <div className="status-zaplanowana">Zaplanowana</div>
                        <p>dr n. med. Jan Kowalski<br/>Kardiolog</p>
                        <a href="#">Specjalista</a>
                        <a href="#">Przychodnia "Biegunowa"</a>
                        <a href="#">Lokalizacja</a>
                    </div>

                    <div className="visit-card">
                        <strong>18 lipca 2024, 08:00</strong>
                        <div className="status-odwolana">Odwołana</div>
                        <p>dr n. med. Iwona Bierko<br/>Dermatolog</p>
                        <a href="#">Specjalista</a>
                        <a href="#">Przychodnia "Biegunowa"</a>
                        <a href="#">Lokalizacja</a>
                    </div>

                    <div className="visit-card">
                        <strong>05 lipca 2024, 18:40</strong>
                        <div className="status-zrealizowana">Zrealizowana</div>
                        <p>dr n. med. Joanna Przybysz<br/>Internista</p>
                        <a href="#">Specjalista</a>
                        <a href="#">Przychodnia "Miedź"</a>
                        <a href="#">Lokalizacja</a>
                    </div>

                </div>
            </div>

            <div className="right-section">

                <img src={image1} alt="Lekarze" className="img-fluid custom-img" />
                <img src={image2} alt="Lekarze" className="img-fluid custom-img" />

               </div>

            </div>


    </>
)

export default Przyklad;
